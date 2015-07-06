using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// 
/// 
/// 
/// 
/// </summary>

public class Pusher : MonoBehaviour
{
    private enum MyState
    {
        None,

        Push_Phsyics,
        Pull_Phsyics,

        Push_Normal,
        Pull_Normal,
    }

    private MyState m_ePrevState;
    private MyState m_eCurrentState = MyState.None;

    private Transform m_pTransform;
    private Rigidbody m_pRigidbody;

    private Vector3 m_vecOriginPos;
    private Vector3 m_vecDirection;

    private Vector3 m_vecPrevVelocity;
    private float m_fPower;
    private float m_fPushTime;
    private float m_fProximityDist = 0.5f;

    //----------------------------------------------------------------

    void Awake()
    {
        m_pTransform = transform;
        m_pRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        switch (m_eCurrentState)
        {
            case MyState.Pull_Phsyics :
            if(CheckIsProximityToOriginPos())
            {
                m_pRigidbody.velocity = Vector3.zero;
                m_pTransform.position = m_vecOriginPos;
                m_eCurrentState = MyState.None;
            }
            break;

            case MyState.Push_Normal :
            transform.Translate(m_vecDirection * m_fPushTime);
            break;

            case MyState.Pull_Normal:
            transform.Translate(m_vecDirection * m_fPushTime * -1f);
            if (CheckIsProximityToOriginPos())
            {
                m_pTransform.position = m_vecOriginPos;
                m_eCurrentState = MyState.None;
            }
            break;
        }
    }

    //----------------------------------------------------------------

    public void DoPushToDirection(Vector3 vecDirection, float fPower, bool bUsePhysics = true)
    {
        m_vecDirection = vecDirection;
        m_fPower = fPower;

        if (bUsePhysics)
            ProcChangeMyState(MyState.Push_Phsyics);
        else
            ProcChangeMyState(MyState.Push_Normal);
    }

    public void DoPushAndPull(Vector3 vecDirection, float fPower, float fPushTime, bool bUsePhysics = true)
    {
        if (m_eCurrentState != MyState.None)
            return;

        m_vecDirection = vecDirection;
        m_fPower = fPower;
        m_fPushTime = fPushTime;
        m_vecOriginPos = m_pTransform.position;

        if (bUsePhysics)
            StartCoroutine("PushAndPullPhysics");
        else
            StartCoroutine("PushAndPullNormal");
    }

    public void DoStop()
    {
        m_ePrevState = m_eCurrentState;
        m_eCurrentState = MyState.None;

        m_vecPrevVelocity = m_pRigidbody.velocity;
        m_pRigidbody.velocity = Vector3.zero;
    }

    public void DoPlay()
    {
        m_eCurrentState = m_ePrevState;
        m_pRigidbody.velocity = m_vecPrevVelocity;
    }

    //----------------------------------------------------------------

    private void ProcChangeMyState(MyState eState)
    {
        m_eCurrentState = eState;

        switch (eState)
        {
            case MyState.Push_Phsyics :
                m_pRigidbody.AddForce(m_vecDirection * m_fPower, ForceMode.Force);
                break;

            case MyState.Pull_Phsyics :
                m_pRigidbody.AddForce(m_vecDirection * m_fPower * -2f, ForceMode.Force);
                break;
        }
    }

    private bool CheckIsProximityToOriginPos()
    {
        float fDist = Vector3.Distance(m_pTransform.position, m_vecOriginPos);

        if (fDist < m_fProximityDist)
            return true;
        else
            return false;
    }

    //----------------------------------------------------------------

    IEnumerator PushAndPullPhysics()
    {
        ProcChangeMyState(MyState.Push_Phsyics);

        yield return new WaitForSeconds(m_fPushTime);

        ProcChangeMyState(MyState.Pull_Phsyics);
    }

    IEnumerator PushAndPullNormal()
    {
        ProcChangeMyState(MyState.Push_Normal);

        yield return new WaitForSeconds(m_fPushTime);

        ProcChangeMyState(MyState.Pull_Normal);
    }
}

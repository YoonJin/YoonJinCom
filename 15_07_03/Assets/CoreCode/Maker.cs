using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

/// <summary>
/// 클래스 역할 : 무언가를 만드는 클래스
/// 작성자      : 이양진
/// 
/// 수정자      : 이양진
/// 수정일      : 15-07-03
/// 내용        : ㅁㅁㅁ 추가
/// 
/// 수정한 내용이 있으면 이어서 쓰시오..
/// 
/// 수정자      : 홍길동
/// 수정일      : 15-07-03
/// 내용        : 알고리즘 개선
/// </summary>

public class Maker : MonoBehaviour{
   
    [SerializeField]
    private int m_iMakeCount = 1;
    [SerializeField]
    private Vector3 m_vecMakePos = Vector3.zero;
    [SerializeField]
    private Vector3 m_vecMakeRotation = Vector3.zero;
    [SerializeField]
    private float m_fMakeTime = 0.5f;
    [SerializeField]
    private string[] m_arrMakeObjectName;

    //------------------------------------------------------------------------------

    void Awake()
    {
        if (m_vecMakePos == Vector3.zero)
        {
            m_vecMakePos = this.transform.position;
        }
    }

    //------------------------------------------------------------------------------

    public void SetMakePosAndRotation(Vector3 vecMakePos, Vector3 vecMakeRotation)
    {
        m_vecMakePos = vecMakePos;
        m_vecMakeRotation = vecMakeRotation;
    }

    public void SetMakeCount(int iMakeCount) { m_iMakeCount = iMakeCount; }

    public void SetMakeObjectName(params string[] arrMakeObjectName) { m_arrMakeObjectName = arrMakeObjectName; }

    public void SetMakeTime(float fMakeTime) { m_fMakeTime = fMakeTime; } 

    public void DoStartMake()
    {
        StartCoroutine("AutoGeneration", m_fMakeTime);
    }

    public void DoStopMake()
    {
        StopCoroutine("AutoGeneration");
    }

    //------------------------------------------------------------------------------

    private IEnumerator AutoGeneration()
    {
        int SelectNum;
        while (true)
        {
            SelectNum = Random.Range(0, m_arrMakeObjectName.Length);
            GameObject newObject = Resources.Load(m_arrMakeObjectName[SelectNum]) as GameObject;
            for (int i = 0; i < m_iMakeCount; ++i)
                Instantiate(newObject, m_vecMakePos, Quaternion.Euler(m_vecMakeRotation));

            yield return new WaitForSeconds(m_fMakeTime);
        }
    }
}

using UnityEngine;
using System.Collections;

public class SpringControl : MonoBehaviour {

    private Rigidbody springBody;

//     private Pusher m_pPusher;
// 
//     private Transform m_Transform;
//     private Vector3 m_vecOriginVector;
// 
//     private float fDist;
//     private float fDistOffset;
// 
//     private float fSpringPower;
//     private float fSpringPowerOffset = 50f;
// 
//     private float fPower = 10f;
// 
//     void Awake()
//     {
//         m_pPusher = GetComponent<Pusher>();
//         m_Transform = transform;
//         m_vecOriginVector = transform.position;
//     }
// 
//     void Update()
//     {
//         fDist = Vector3.Distance(m_Transform.position, m_vecOriginVector);
//         fDistOffset = Mathf.Clamp01(fDist * 0.1f);
//         fSpringPower = (1 - fDist) * fSpringPowerOffset;
// 
//         if (Input.GetKey(KeyCode.Space))
//         {
//             m_pPusher.DoPushToDirection(Vector3.down, fPower);
// 
//             fPower -= 0.01f;
//             fPower = Mathf.Clamp(fPower, 0, 100);
//         }
//         else if (fDist < 0.4f)
//         {
//             m_Transform.position = m_vecOriginVector;
//         }
//         else if (fDist > 0.5f)
//         {
//             m_pPusher.DoPushToDirection(Vector3.up, fSpringPower);
//         }
//         else if (fDist > 2f)
//         {
//             m_pPusher.DoStop();
//         }
// 
//         else if (Input.GetKeyDown(KeyCode.Space))
//         {
//             fPower = 10f;
//             m_pPusher.DoPlay();
//         }
//    
	// Use this for initialization
    void Start () {
        springBody = GetComponent<Rigidbody>();
    }
	
    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetKey(KeyCode.Space)) {
            springBody.AddForce(Vector3.up * -10.0f, ForceMode.Impulse);
        }
    }
}

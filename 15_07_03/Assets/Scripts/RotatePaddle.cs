using UnityEngine;
using System.Collections;

public class RotatePaddle : MonoBehaviour
{

    private HingeJoint hinge;
    private JointMotor moter;

    // Use this for initialization
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        moter = new JointMotor();
        moter.force = 100f;
        hinge.useMotor = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            moter.targetVelocity = 300f;
        }
        else {
            moter.targetVelocity = -500f;
        }
        hinge.motor = moter;
    }
}

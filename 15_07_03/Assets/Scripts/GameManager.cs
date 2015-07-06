using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Maker m_pBallMaker;

    //----------------------------------------------------------------

	void Start () {
        m_pBallMaker.SetMakeObjectName("Prefabs/Ball");
        MakeBall();
    }
	
	void Update () {
	
	}

    //----------------------------------------------------------------

    void MakeBall()
    {
        m_pBallMaker.DoStartMake();
        m_pBallMaker.DoStopMake();
    }

}

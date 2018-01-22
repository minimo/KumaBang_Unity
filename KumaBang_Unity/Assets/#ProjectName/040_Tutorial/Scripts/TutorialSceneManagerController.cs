using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSceneManagerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int numStage = ApplicationManagerController.Instance.playingStageNumber;

        //チュートリアル準備
        string name = "Prefabs/Tutorial" + numStage;
        GameObject tutorial1 = Instantiate((GameObject)Resources.Load(name + "_1"));
        tutorial1.transform.SetParent(this.transform);
        GameObject tutorial2 = Instantiate((GameObject)Resources.Load(name + "_1"));
        tutorial2.transform.SetParent(this.transform);
	}
}

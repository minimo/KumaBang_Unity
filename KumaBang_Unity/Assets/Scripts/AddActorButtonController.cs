using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddActorButtonController : MonoBehaviour {

    GameObject sceneManager = null;

	// Use this for initialization
	void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SceneManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void click () {
        SceneManager sc = this.sceneManager.GetComponent<SceneManager>();
        sc.addActor();
        sc.addActorFadeInOut();
    }
}

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
        this.sceneManager.GetComponent<SceneManager>().addActor();
        this.sceneManager.GetComponent<SceneManager>().addActorFadeInOut();
    }
}

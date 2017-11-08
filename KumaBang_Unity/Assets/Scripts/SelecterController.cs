using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecterController : MonoBehaviour {

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
        this.sceneManager.GetComponent<SceneManager>().openSelecter();
    }
}

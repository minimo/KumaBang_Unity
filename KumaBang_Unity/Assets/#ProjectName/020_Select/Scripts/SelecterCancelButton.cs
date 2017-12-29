using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecterCancelButton : MonoBehaviour {

    SelectSceneManager sceneManager = null;

	// Use this for initialization
	void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SelectSceneManager").GetComponent<SelectSceneManager>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void click() {
        this.sceneManager.closeSelecter();
    }
}

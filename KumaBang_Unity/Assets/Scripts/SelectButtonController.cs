using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButtonController : MonoBehaviour {

    GameObject sceneManager = null;

    Button button;

	// Use this for initialization
	void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SceneManager");
        this.button = this.GetComponent<Button>();
        this.button.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.sceneManager.GetComponent<SceneManager>().selecter) {
            this.button.interactable = false;
        } else {
            this.button.interactable = true;
        }
	}

    public void click () {
        if (this.sceneManager.GetComponent<SceneManager>().selecter) return;
        this.sceneManager.GetComponent<SceneManager>().openSelecter();
    }
}

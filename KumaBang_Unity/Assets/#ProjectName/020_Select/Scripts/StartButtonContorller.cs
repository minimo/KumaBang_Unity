using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonContorller : MonoBehaviour {

    SelectSceneManager sceneManager = null;

    Button button;    

	// Use this for initialization
	void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SelectSceneManager").GetComponent<SelectSceneManager>();
        this.button = this.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.sceneManager.selecter) {
            this.button.interactable = false;
        } else {
            this.button.interactable = true;
        }
	}
    public void click () {
        if (this.sceneManager.selecter) return;
        this.sceneManager.openStartDialog();
    }
}

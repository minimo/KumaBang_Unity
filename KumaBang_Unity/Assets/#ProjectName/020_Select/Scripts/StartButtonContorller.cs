using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonContorller : MonoBehaviour {

    GameObject sceneManager = null;

    Button button;    

	// Use this for initialization
	void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SelectSceneManager");		
        this.button = this.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.sceneManager.GetComponent<SelectSceneManager>().selecter) {
            this.button.interactable = false;
        } else {
            this.button.interactable = true;
        }
	}
    public void click () {
        if (this.sceneManager.GetComponent<SelectSceneManager>().selecter) return;
        this.sceneManager.GetComponent<SelectSceneManager>().openStartDialog();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddActorButtonController : MonoBehaviour {

    SelectSceneManager sceneManager = null;

    Button button = null;

	// Use this for initialization
	void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SelectSceneManager").GetComponent<SelectSceneManager>();
        this.button = this.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!this.sceneManager.isInteractive) {
            this.button.interactable = false;
        } else {
            this.button.interactable = true;
            if (this.sceneManager.getNumActor() == this.sceneManager.getMaxActor()) {
                this.button.interactable = false;            
            }
        }
	}

    public void click () {
        SelectSceneManager sc = this.sceneManager.GetComponent<SelectSceneManager>();
        if (sc.isInteractive == false) return;
        sc.addActor();
        sc.addActorFadeInOut();
    }
}

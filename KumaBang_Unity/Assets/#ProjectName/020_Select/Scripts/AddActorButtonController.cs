using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddActorButtonController : MonoBehaviour {

    SelectSceneManager sceneManager = null;

	// Use this for initialization
	void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SelectSceneManager").GetComponent<SelectSceneManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.sceneManager.getNumActor() == this.sceneManager.getMaxActor()) {
            Button btn = this.GetComponent<Button>();
            btn.interactable = false;            
        }
	}

    public void click () {
        SelectSceneManager sc = this.sceneManager.GetComponent<SelectSceneManager>();
        sc.addActor();
        sc.addActorFadeInOut();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddActorButtonController : MonoBehaviour {

    GameObject sceneManager = null;
    SelectSceneManager scm = null;

	// Use this for initialization
	void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SelectSceneManager");
        this.scm = this.sceneManager.GetComponent<SelectSceneManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.scm.getNumActor() == this.scm.getMaxActor()) {
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

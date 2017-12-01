using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddActorButtonController : MonoBehaviour {

    GameObject sceneManager = null;
    SceneManager scm = null;

	// Use this for initialization
	void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SceneManager");
        this.scm = this.sceneManager.GetComponent<SceneManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.scm.getNumActor() == this.scm.getMaxActor()) {
            Button btn = this.GetComponent<Button>();
            btn.interactable = false;            
        }
	}

    public void click () {
        SceneManager sc = this.sceneManager.GetComponent<SceneManager>();
        sc.addActor();
        sc.addActorFadeInOut();
    }
}

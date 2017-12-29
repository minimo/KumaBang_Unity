using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapSelecterController : MonoBehaviour {

    //増分
    [SerializeField] int diff = 0;

    SelectSceneManager sceneManager;

    // Use this for initialization
    void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SelectSceneManager").GetComponent<SelectSceneManager>();
    }
	
    // Update is called once per frame
    void Update () {
    }

    void OnMouseDown() {
        if(this.sceneManager.selecter) return;
        bool isRight = true;
        if (diff < 0) isRight = false;
        sceneManager.changeActorNext(isRight, Mathf.Abs(this.diff));
    }
}

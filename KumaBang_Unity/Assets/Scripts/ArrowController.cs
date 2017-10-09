using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    //右矢印フラグ
    public bool isRight = false;

    //シーンマネージャー
    GameObject sceneManager = null;

    // Use this for initialization
    void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SceneManager");

        //左右にフラフラ
        float move = -0.1f;
        if (this.isRight) move *= -1;
        iTween.MoveBy(gameObject,
            iTween.Hash(
                "x", move,
                "easeType", "easeInOutSine",
                "loopType", "pingPong",
                "time", 0.5f
            ));
    }
	
    // Update is called once per frame
    void Update () {
    }

    public void click () {
        this.sceneManager.GetComponent<SceneManager>().changeActor(this.isRight);
    }
}

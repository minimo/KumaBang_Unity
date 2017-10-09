using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour {

    public bool isMoving = false;

    public int actorID = 0;

    // Use this for initialization
    void Start () {
    }
	
    // Update is called once per frame
    void Update () {
    }

    //アクター移動処理
    public void flick (bool isRight) {
        if (this.isMoving) return;

        float move = 10.0f;
        if (isRight) move *= -1.0f;
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", move,
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 1.0f,
                "oncomplete", "OnCompleteCallback",
                "oncompletetarget", this.gameObject
            ));

        this.isMoving = true;
    }

    //iTween動作終了コールバック
    public void OnCompleteCallback() {
        this.isMoving = false;
        Debug.Log("moveComplete");
    }
}

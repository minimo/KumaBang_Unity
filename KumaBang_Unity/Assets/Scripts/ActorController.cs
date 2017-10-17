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

    public void OnMouseDown () {
        if (this.isMoving) return;
        iTween.PunchScale(this.gameObject,
            iTween.Hash(
                "x", 0.1f,
                "y", 0.1f,
            	"time", 0.5f
            ));
    }

    //アクター移動処理
    public void flick (bool isRight, float delay = 0.0f) {
        if (this.isMoving) return;

        float move = 10.0f;
        if (isRight) move *= -1.0f;
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", move,
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 1.0f,
                "delay", delay,
                "oncomplete", "OnCompleteCallback",
                "oncompletetarget", this.gameObject
            ));

        this.isMoving = true;
    }

    //iTween動作終了コールバック
    public void OnCompleteCallback() {
        this.isMoving = false;
    }
}

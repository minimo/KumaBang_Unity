using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorIconController : MonoBehaviour {

    bool oneTime = false;

	// Use this for initialization
    void Start () {
	}

	// Update is called once per frame
    void Update () {
    }

    //一時アイコンとして設定
    void setOneTime() {
        this.oneTime = true;
        Destroy(this.gameObject, 2.0f);
    }

    public void flick(bool isRight, int incremental) {
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", incremental * (isRight? -1: 1),
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 1.0f
            )
        );
    }

    public void screenIn(bool isRight, int incremental = 1) {
        Vector3 pos = this.transform.position;
        pos.x = isRight? 3.0f + incremental: -3.0f - incremental;
        this.transform.position = pos;
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", (1.0f + incremental) * (isRight? -1: 1),
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 1.0f
            )
        );
    }
    public void screenOut(bool isRight, int incremental = 1) {
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", (3.0f + incremental) * (isRight? -1: 1),
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 1.0f
            )
        );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorIconController : MonoBehaviour {

	// Use this for initialization
    void Start () {
	}

	// Update is called once per frame
    void Update () {
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
}

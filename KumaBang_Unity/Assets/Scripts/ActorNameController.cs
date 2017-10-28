using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorNameController : MonoBehaviour {

    [SerializeField] GameObject name;

    public void setName(string newName) {
        this.name.GetComponent<Text>().text = newName;
    }

    //移動処理
    public void flick(bool isRight, float delay = 0.0f) {
        float move = 10.0f;
        if (isRight) move *= -1.0f;
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", move,
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 1.0f,
                "delay", delay + 0.1f,
                "oncomplete", "OnCompleteCallback",
                "oncompletetarget", this.gameObject
            ));
    }
}

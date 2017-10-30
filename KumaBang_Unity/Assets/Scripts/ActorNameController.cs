﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorNameController : MonoBehaviour {

    [SerializeField] GameObject nowActorName;

    public void setName(string newName) {
        this.nowActorName.GetComponent<Text>().text = newName;
    }

    //移動処理
    public void flick(bool isRight, float delay = 0.0f) {
        StartCoroutine(flickCoroutine(isRight));
        return;
    }

    private IEnumerator flickCoroutine(bool isRight) {
        float move = 10.0f;
        if (isRight) move *= -1.0f;
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", move,
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 1.0f,
                "delay", 0.1f
            ));
        yield return new WaitForSeconds(1.1f);

        Vector3 pos = this.transform.position;
        pos.x = isRight? 10.0f: -10.0f;
        this.transform.position = pos;
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", move,
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 1.0f
            ));
        
    }
}
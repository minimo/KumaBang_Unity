﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapSelecterController : MonoBehaviour {

    //増分
    [SerializeField] int diff = 0;

    GameObject sceneManager;

    // Use this for initialization
    void Start () {
        this.sceneManager = GameObject.Find("SceneManager");
    }
	
    // Update is called once per frame
    void Update () {
    }

    void OnMouseDown() {
        if(this.sceneManager.GetComponent<SceneManager>().selecter) return;
        bool isRight = true;
        if (diff < 0) isRight = false;
        sceneManager.GetComponent<SceneManager>().changeActorNext(isRight, Mathf.Abs(this.diff));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameSceneViewController view;

    public GameObject spriteObject = null;

    //ステージ上座標
    int stageX = 0, stageY = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void setStartPosition(int x, int y) {
        this.stageX = x;
        this.stageY = y;
    }
}

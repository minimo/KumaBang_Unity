using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //ゲーム画面用ビュー
        GameObject view = Instantiate((GameObject)Resources.Load("Prefabs/GameView"));
        view.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

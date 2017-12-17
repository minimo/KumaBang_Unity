using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

    SoundManagerController soundManager;

	// Use this for initialization
	void Start () {
        //ゲーム画面用ビュー
        GameObject view = Instantiate((GameObject)Resources.Load("Prefabs/GameView"));
        view.transform.parent = this.transform;

        this.soundManager = SoundManagerController.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

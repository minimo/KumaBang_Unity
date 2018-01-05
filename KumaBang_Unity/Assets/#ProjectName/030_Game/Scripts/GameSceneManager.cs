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

        //サウンドマネージャー取得
        this.soundManager = SoundManagerController.Instance;
        this.soundManager.addSound("bgm_stage1", "Sounds/DS-124m");
        soundManager.playBGM("bgm_stage1");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

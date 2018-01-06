using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour {

    SoundManagerController soundManager;

	// Use this for initialization
	void Start () {
        //ゲーム画面用ビュー
        GameObject view = Instantiate((GameObject)Resources.Load("Prefabs/GameSceneView"));
        view.transform.parent = this.transform;

        //ゲーム画面用キャンバス
        GameObject canvas = Instantiate((GameObject)Resources.Load("Prefabs/GameSceneCanvas"));
        canvas.transform.parent = this.transform;

        //サウンドマネージャー取得
        this.soundManager = SoundManagerController.Instance;
        //使用音声ファイル読み込み
        this.soundManager.addSound("bgm_stage1", "Sounds/DS-124m");
        soundManager.playBGM("bgm_stage1");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameSceneManager : MonoBehaviour {

    //アプリケーションマネージャー
    ApplicationManagerController app;

    public int gameScore = 0;
    public int zanki = 3;
    int stageLevel = 1;

    //サウンドマネージャー
    SoundManagerController soundManager;

    //ビュー管理
    GameObject sceneView;
    GameSceneViewController sceneViewController;

    //キャンバス管理
    GameObject sceneCanvas;
    GameSceneCanvasController SceneCanvasController;

    //管理フラグ
    public bool isGameStart = false;
    bool isAlreadyView = false;
    bool isAlreadyCanvas = false;

    [SerializeField] int startStageNumber = 1;

	// Use this for initialization
	void Start () {
        //アプリケーションマネージャー取得
        this.app = ApplicationManagerController.Instance;
        this.app.currentSceneManager = this.gameObject;

        //ステージ情報
        this.app.playingStageNumber = this.startStageNumber;

        //サウンドマネージャー取得
        this.soundManager = SoundManagerController.Instance;
        //使用音声ファイル読み込み
        this.soundManager.addSound("bgm_clear", "Sounds/bgm_StageClear");
        this.soundManager.addSound("playermiss", "Sounds/se_PlayerMiss");
        this.soundManager.addSound("paneldrop", "Sounds/se_PanelThrough");

        this.setupScene();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void setupScene(bool isRestart = false) {
        this.isGameStart = false;
        this.isAlreadyView = false;
        this.isAlreadyCanvas = false;

        //ゲーム画面用ビュー
        GameObject view = Instantiate((GameObject)Resources.Load("Prefabs/GameSceneView"));
        view.transform.SetParent(this.transform);
        this.sceneView = view;
        this.sceneViewController = view.GetComponent<GameSceneViewController>();

        //ゲーム画面用キャンバス
        GameObject canvas = Instantiate((GameObject)Resources.Load("Prefabs/GameSceneCanvas"));
        canvas.transform.SetParent(this.transform);
        this.sceneCanvas = canvas;
        this.SceneCanvasController = canvas.GetComponent<GameSceneCanvasController>();

        //シーン開始フェード
        GameObject fd = Instantiate((GameObject)Resources.Load("Prefabs/Mask_first"));
        fd.transform.position = new Vector3(2.5f, -2.5f);

        //リスタート時は以下の処理を行わない
        if (isRestart) return;

        //ステージBGM再生
        this.soundManager.addSound("stagebgm", "Sounds/bgm_Stage" + this.app.playingStageNumber);
        this.soundManager.playBGM("stagebgm", 3.0f);
    }

    //スコア加算処理
    public void addScore(int point, Vector3 pos) {
        this.gameScore += point;
        this.SceneCanvasController.displayScore(point, pos);
    }

    void gameOver() {
    }

    //シーンビューステージ構築完了
    void OnAlreadyStartView() {
        this.isAlreadyView = true;
        if (this.isAlreadyCanvas) this.OnStartStage();
    }

    //シーンキャンバスステージ構築完了
    void OnAlreadyStartCanvas() {
        this.isAlreadyCanvas = true;
        if (this.isAlreadyView) this.OnStartStage();
    }

    //ステージ開始
    void OnStartStage() {
        this.isAlreadyView = false;
        this.isAlreadyCanvas = false;
        this.isGameStart = true;

        this.sceneView.SendMessage("OnStartStage");
        this.sceneCanvas.SendMessage("OnStartStage");
    }

    //ステージクリア
    void OnStageClear(bool isPerfect) {
        this.soundManager.playBGM("bgm_clear", 0.5f, false);
        this.sceneCanvas.SendMessage("OnStageClear", isPerfect);
        this.app.playingStageNumber++;
        DOVirtual.DelayedCall(7.0f, () => {
            Destroy(this.sceneView);
            Destroy(this.sceneCanvas);
            this.setupScene();
        });
    }
    //プレーヤーミス
    void OnPlayerMiss() {
        this.zanki--;
        if (zanki < 0) {
            this.zanki = 0;
            this.gameOver();
            return;
        }

        this.soundManager.playSE("se_playerMiss");
        this.sceneCanvas.SendMessage("OnPlayerMiss");
        DOVirtual.DelayedCall(5.0f, () => {
            Destroy(this.sceneView);
            Destroy(this.sceneCanvas);
            this.setupScene();
        });
    }
}

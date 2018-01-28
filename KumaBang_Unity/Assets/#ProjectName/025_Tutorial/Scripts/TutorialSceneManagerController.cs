using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TutorialSceneManagerController : MonoBehaviour {

    //サウンドマネージャー
    SoundManagerController soundManager;

    //ビュー管理
    GameObject sceneView;
    GameSceneViewController sceneViewController;

    //キャンバス管理
    GameObject sceneCanvas;
    GameSceneCanvasController SceneCanvasController;

	// Use this for initialization
	void Start () {
        //チュートリアル画面用ビュー
        GameObject view = Instantiate((GameObject)Resources.Load("Prefabs/TutorialSceneView"));
        view.transform.SetParent(this.transform);
        this.sceneView = view;
        this.sceneViewController = view.GetComponent<GameSceneViewController>();

        //ゲーム画面用キャンバス
        GameObject canvas = Instantiate((GameObject)Resources.Load("Prefabs/TutorialSceneCanvas"));
        canvas.transform.SetParent(this.transform);
        this.sceneCanvas = canvas;
        this.SceneCanvasController = canvas.GetComponent<GameSceneCanvasController>();

        //サウンドマネージャー取得
        this.soundManager = SoundManagerController.Instance;
        this.soundManager.addSound("bgm_tutorial", "Sounds/bgm_tutorial");
        this.soundManager.addSound("change", "Sounds/yf_cursor22");

        soundManager.playBGM("bgm_tutorial", 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
	}

    //最終ページ到達
    void OnFinish() {
        this.sceneCanvas.SendMessage("OnStartButtonChange");
    }

    void OnNextPage() {
        this.sceneView.SendMessage("OnSwipe", new Vector2(-1, 0));
    }

    void OnNextScene() {
        SceneManager.LoadScene("GameScene");
    }
}

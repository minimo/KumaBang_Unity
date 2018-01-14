using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameSceneCanvasController : MonoBehaviour {

    //シーンマネージャー
    GameSceneManager sceneManager;

    Text scoreText;
    int dispScore = 0;  //表示用スコア
    int _dispScore = 0; //表示用スコア保存

	// Use this for initialization
	void Start () {
        //アプリケーションマネージャー取得
        GameObject currentScene = ApplicationManagerController.Instance.currentSceneManager;
        this.sceneManager = currentScene.GetComponent<GameSceneManager>();

        //スコア表示
        GameObject txtObj = Instantiate((GameObject)Resources.Load("Prefabs/ScoreText"));
        txtObj.transform.SetParent(this.transform);
        txtObj.transform.position = new Vector3(1.5f, 2.0f, 0);
        this.scoreText = txtObj.GetComponent<Text>();

        this.initStage();
	}
	
	// Update is called once per frame
	void Update () {
        if (this._dispScore != this.sceneManager.gameScore) {
            this._dispScore = this.sceneManager.gameScore;
            DOTween.To(
                () => this.dispScore,
                num => this.dispScore = num,
                this._dispScore,
                1.0f
            );
        }
        this.scoreText.text = "Score: " + this.dispScore;
	}

    void initStage() {
        //ステージ番号表示
        GameObject stageNumber = Instantiate((GameObject)Resources.Load("Prefabs/StageNumber"));
        stageNumber.transform.parent = this.transform;
        stageNumber.transform.position = new Vector3(-2.5f, -2.5f, 0.0f);
        stageNumber.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        var seq = DOTween.Sequence();
        seq.Append(stageNumber.transform.DOMove(new Vector3(2.5f, -2.5f, 0.0f), 0.5f)
            .SetEase(Ease.InOutSine)
            .SetDelay(2.0f)
        );
        seq.Append(stageNumber.transform.DOMove(new Vector3(10.0f, -2.5f, 0.0f), 0.5f)
            .SetEase(Ease.InOutSine)
            .SetDelay(1.0f)
            .OnComplete(() => {
                this.transform.parent.gameObject.SendMessage("OnAlreadyStartCanvas");
            })
        );

        //ゴールパネル標識
    }

    void OnStartStage() {
    }

    void OnPointGet(Vector3 pos, int score) {
    }
}

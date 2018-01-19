using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameSceneCanvasController : MonoBehaviour {

    //シーンマネージャー
    GameSceneManager sceneManager;

    //スコア表示用
    Text scoreText;
    int dispScore = 0;  //表示用スコア
    int _dispScore = 0; //表示用スコア保存

    //残機表示用
    Text zankiText;

	// Use this for initialization
	void Start () {
        //アプリケーションマネージャー取得
        GameObject currentScene = ApplicationManagerController.Instance.currentSceneManager;
        this.sceneManager = currentScene.GetComponent<GameSceneManager>();

        //スコア同期
        this.dispScore = this.sceneManager.gameScore;
        this._dispScore = this.sceneManager.gameScore;

        //スコア表示
        GameObject txtObj = Instantiate((GameObject)Resources.Load("Prefabs/ScoreText"));
        txtObj.transform.SetParent(this.transform);
        txtObj.transform.position = new Vector3(1.5f, 2.0f, 0);
        this.scoreText = txtObj.GetComponent<Text>();

        //残機表示
        txtObj = Instantiate((GameObject)Resources.Load("Prefabs/ZankiText"));
        txtObj.transform.SetParent(this.transform);
        txtObj.transform.position = new Vector3(4.0f, 2.0f, 0);
        this.zankiText = txtObj.GetComponent<Text>();

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
                0.2f
            );
        }
        this.scoreText.text = "Score: " + this.dispScore;
	}

    void initStage() {
        //ステージ番号表示
        GameObject stageNumber = Instantiate((GameObject)Resources.Load("Prefabs/StageNumber"));
        stageNumber.transform.SetParent(this.transform);
        stageNumber.transform.position = new Vector3(-2.5f, -2.5f, 0.0f);
        stageNumber.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        stageNumber.GetComponent<Text>().text = "STAGE " + ApplicationManagerController.Instance.playingStageNumber;

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
                Destroy(stageNumber);
            })
        );

        //ゴールパネル標識
    }

    public void displayScore(int point, Vector3 pos) {
        pos.y += 0.25f;
        //ステージ番号表示
        GameObject canvasText = Instantiate((GameObject)Resources.Load("Prefabs/CanvasTextLabel"));
        canvasText.transform.SetParent(this.transform);
        canvasText.transform.position = pos;
        canvasText.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        canvasText.GetComponent<Text>().text = "" + point;

        var seq = DOTween.Sequence();
        seq.Append(canvasText.transform.DOMove(new Vector3(0.0f, 0.5f, 0.0f), 2.0f)
            .SetEase(Ease.OutQuint)
            .SetRelative()
        );

        Text renderer = canvasText.GetComponent<Text>();
        DOTween.ToAlpha(
            () => renderer.color,
            color => renderer.color = color,
            0.0f,
            2.0f
        ).OnComplete(() => {
            Destroy(canvasText);
        });
    }

    void OnStartStage() {
    }

    void OnStageClear(bool isPerfect) {
        //ステージ番号表示
        GameObject canvasText = Instantiate((GameObject)Resources.Load("Prefabs/CanvasTextLabel"));
        canvasText.transform.SetParent(this.transform);
        canvasText.transform.position = new Vector3(2.5f, 10.0f, 0.0f);
        canvasText.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        canvasText.GetComponent<Text>().text = "CLEAR !!";

        var seq = DOTween.Sequence();
        seq.Append(canvasText.transform.DOMove(new Vector3(2.5f, -2.5f, 0.0f), 0.5f)
            .SetEase(Ease.OutBounce)
            .SetDelay(1.0f)
        );
        seq.Append(canvasText.transform.DOMove(new Vector3(2.5f, -10.0f, 0.0f), 0.5f)
            .SetEase(Ease.InOutSine)
            .SetDelay(1.0f)
            .OnComplete(() => {
                this.transform.parent.gameObject.SendMessage("OnAlreadyStartCanvas");
                Destroy(canvasText);
            })
        );
    }

    void OnPlayerMiss() {
        //ステージ番号表示
        GameObject canvasText = Instantiate((GameObject)Resources.Load("Prefabs/CanvasTextLabel"));
        canvasText.transform.SetParent(this.transform);
        canvasText.transform.position = new Vector3(2.5f, 10.0f, 0.0f);
        canvasText.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        canvasText.GetComponent<Text>().text = "MISS!!";

        var seq = DOTween.Sequence();
        seq.Append(canvasText.transform.DOMove(new Vector3(2.5f, -2.5f, 0.0f), 0.5f)
            .SetEase(Ease.OutBounce)
            .SetDelay(1.0f)
        );
        seq.Append(canvasText.transform.DOMove(new Vector3(2.5f, -10.0f, 0.0f), 0.5f)
            .SetEase(Ease.InOutSine)
            .SetDelay(1.0f)
            .OnComplete(() => {
                this.transform.parent.gameObject.SendMessage("OnAlreadyStartCanvas");
                Destroy(canvasText);
            })
        );
    }

    void OnGameOver() {
    }
}

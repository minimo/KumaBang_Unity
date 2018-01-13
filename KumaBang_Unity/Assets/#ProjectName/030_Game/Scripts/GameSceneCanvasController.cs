using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameSceneCanvasController : MonoBehaviour {

    //シーンマネージャー
    GameSceneManager sceneManager;

	// Use this for initialization
	void Start () {
        GameObject currentScene = ApplicationManagerController.Instance.currentSceneManager;
        this.sceneManager = currentScene.GetComponent<GameSceneManager>();

        this.initStage();
	}
	
	// Update is called once per frame
	void Update () {
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
    }

    void OnStartStage() {
//        this.initStage();
    }
}

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

        this.transform.parent.gameObject.SendMessage("OnAlreadyStartCanvas");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnStartStage() {
        //ステージ番号表示
        GameObject stageNumber = Instantiate((GameObject)Resources.Load("Prefabs/StageNumber"));
        stageNumber.transform.parent = this.transform;
        stageNumber.transform.position = new Vector3(-200.0f, 100.0f, 0.0f);
        stageNumber.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        var seq = DOTween.Sequence();
        seq.Append(stageNumber.transform.DOMove(new Vector3(0.0f, 100.0f, 0.0f), 1.0f)
            .SetEase(Ease.InOutSine)
            .SetDelay(3.0f)
        );
        seq.Append(stageNumber.transform.DOMove(new Vector3(200.0f, 100.0f, 0.0f), 1.0f)
            .SetEase(Ease.InOutSine)
            .SetDelay(1.0f)
        );
    }
}

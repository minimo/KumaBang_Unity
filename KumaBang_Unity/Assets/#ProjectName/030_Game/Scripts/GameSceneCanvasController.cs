using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameSceneCanvasController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnStartStage() {
        GameObject stageNumber = Instantiate((GameObject)Resources.Load("Prefabs/StageNumber"));
        stageNumber.transform.parent = this.transform;
        stageNumber.transform.position = new Vector3(-200.0f, 0.0f, 0.0f);
        stageNumber.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        var seq = DOTween.Sequence();
        seq.Append(stageNumber.transform.DOMove(new Vector3(0.0f, 0.0f, 0.0f), 1.0f)
            .SetEase(Ease.InOutSine)
            .SetDelay(1.0f)
        );
        seq.Append(stageNumber.transform.DOMove(new Vector3(200.0f, 0.0f, 0.0f), 1.0f)
            .SetEase(Ease.InOutSine)
            .SetDelay(1.0f)
        );
    }
}

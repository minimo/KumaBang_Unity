using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialSceneCanvasController : MonoBehaviour {

    GameObject startButton;

	// Use this for initialization
	void Start () {
        this.enterStartButton();
	}
    	
	// Update is called once per frame
	void Update () {
		
	}

    void enterSkipButton() {
        //スキップボタン
        GameObject btn = Instantiate((GameObject)Resources.Load("Prefabs/SkipButton"));
        btn.transform.SetParent(this.transform);
        btn.transform.localScale = Vector3.one;
        btn.transform.position = new Vector3(2.0f, 6.0f);
        btn.transform.DOLocalMove(new Vector3(270f, 600f), 0.5f)
            .SetEase(Ease.InOutSine)
            .SetDelay(3.0f);
    }

    void enterStartButton() {
        //スタートボタン
        GameObject btn = Instantiate((GameObject)Resources.Load("Prefabs/StartButton"));
        btn.transform.SetParent(this.transform);
        btn.transform.localScale = Vector3.one;
        btn.transform.position = new Vector3(0.0f, -6.0f);
        btn.transform.DOLocalMove(new Vector3(0f, -550f), 0.5f)
            .SetEase(Ease.InOutSine)
            .SetDelay(2.0f);
        this.startButton = btn;
    }

    void OnStartButtonChange() {
        this.startButton.SendMessage("OnChangeText");
    }

    void OnStart() {
        this.transform.parent.SendMessage("OnNextScene");
    }

    void OnNextPage() {
        this.transform.parent.SendMessage("OnNextPage");
    }

}

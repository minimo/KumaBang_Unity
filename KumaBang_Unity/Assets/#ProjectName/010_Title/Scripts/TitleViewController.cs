using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleViewController : MonoBehaviour {

    SoundManagerController soundManager;

    public bool isTapped = false;

	// Use this for initialization
	void Start () {
        //バックグラウンド作成
        GameObject go = Instantiate((GameObject)Resources.Load("Prefabs/TitleBackGround"));
        go.transform.parent = this.transform;

        go = Instantiate((GameObject)Resources.Load("Prefabs/MaskSprite"));
        go.transform.parent = this.transform;
        go.GetComponent<MaskSpriteController>().RotateOut(1.0f);

        this.soundManager = SoundManagerController.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTapScreen() {
        if (isTapped) return;
        this.isTapped = true;

        GameObject go = Instantiate((GameObject)Resources.Load("Prefabs/MaskSprite"));
        go.transform.parent = this.transform;
        go.GetComponent<MaskSpriteController>().RotateIn(1.0f);
        go.GetComponent<MaskSpriteController>().isSendMessage = true;

        this.soundManager.playSE("TapScreen");
    }

    void OnAnimationEnd() {
        this.transform.parent.gameObject.SendMessage("OnNextScene");
    }
}

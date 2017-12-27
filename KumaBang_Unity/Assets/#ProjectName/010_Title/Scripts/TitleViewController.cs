using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleViewController : MonoBehaviour {

    SoundManagerController soundManager;

    public bool isTapped = false;

	// Use this for initialization
	void Start () {
        //バックグラウンド作成
        GameObject go;
        go = Instantiate((GameObject)Resources.Load("Prefabs/TitleBackGround"));
        go.transform.parent = this.transform;

        //木漏れ日１
        go = Instantiate((GameObject)Resources.Load("Prefabs/TreeLight"));
        go.transform.parent = this.transform;

        //木漏れ日２
        go = Instantiate((GameObject)Resources.Load("Prefabs/TreeLight"));
        go.transform.parent = this.transform;
        Vector3 pos = go.transform.position;
        pos.x = -2.0f;
        go.transform.position = pos;
        go.GetComponent<TreeLightController>().progress = 0.0005f;

        //マスキング用スプライト
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

        this.soundManager.playSE("start");
    }

    void OnAnimationEnd() {
        this.transform.parent.gameObject.SendMessage("OnNextScene");
    }
}

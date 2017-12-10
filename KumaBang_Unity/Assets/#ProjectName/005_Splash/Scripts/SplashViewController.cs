using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashViewController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //スプラッシュ背景
        GameObject back = Instantiate((GameObject)Resources.Load("Prefabs/SplashBack"));
        back.transform.parent = this.transform;

        //ロゴ
        GameObject logo = Instantiate((GameObject)Resources.Load("Prefabs/SplashLogo"));
        logo.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnPlaySound() {
        this.transform.parent.gameObject.SendMessage("OnPlaySound");
    }

    void OnSplashComplete() {
        this.transform.parent.gameObject.SendMessage("OnNextScene");
    }
}

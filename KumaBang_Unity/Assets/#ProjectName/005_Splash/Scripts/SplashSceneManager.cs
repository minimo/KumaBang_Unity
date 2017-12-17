using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSceneManager : MonoBehaviour {

    SoundManagerController soundManager;

	// Use this for initialization
	void Start () {
        //スプラッシュ用ビュー
        GameObject view = Instantiate((GameObject)Resources.Load("Prefabs/SplashView"));
        view.transform.parent = this.transform;

        this.soundManager = SoundManagerController.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnNextScene() {
        SceneManager.LoadScene("TitleScene");
    }

    public void OnPlaySound() {
        soundManager.playSE("splash");
    }
}

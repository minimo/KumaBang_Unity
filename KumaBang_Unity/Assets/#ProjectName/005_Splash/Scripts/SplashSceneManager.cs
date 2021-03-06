﻿using System.Collections;
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
        this.soundManager.addSound("splash", "Sounds/se_maoudamashii_onepoint05");

        StartCoroutine(playSoundCoroutine(1.8f));
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
    private IEnumerator playSoundCoroutine(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        this.OnPlaySound();
    }
}

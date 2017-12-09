using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //スプラッシュ用ビュー
        GameObject view = Instantiate((GameObject)Resources.Load("Prefabs/SplashView"));
        view.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnNextScene() {
        SceneManager.LoadScene("TitleScene");
    }
}

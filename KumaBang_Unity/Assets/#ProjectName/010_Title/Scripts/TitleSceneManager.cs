using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //タイトル用キャンバス
        GameObject cvs = Instantiate((GameObject)Resources.Load("Prefabs/TitleCanvas"));
        cvs.transform.parent = this.transform;

        //タイトル用ビュー
        GameObject view = Instantiate((GameObject)Resources.Load("Prefabs/TitleView"));
        view.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnNextScene() {
        SceneManager.LoadScene("SelectScene");
    }

	public void OnRecieve () {
		Debug.Log("recieve");
	}
}

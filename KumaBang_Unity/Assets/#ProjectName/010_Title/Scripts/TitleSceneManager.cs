using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour {

    SoundManagerController soundManager;

	// Use this for initialization
	void Start () {
        //タイトル用キャンバス
        GameObject cvs = Instantiate((GameObject)Resources.Load("Prefabs/TitleCanvas"));
        cvs.transform.parent = this.transform;

        //タイトル用ビュー
        GameObject view = Instantiate((GameObject)Resources.Load("Prefabs/TitleView"));
        view.transform.parent = this.transform;

        this.soundManager = SoundManagerController.Instance;
        this.soundManager.addSound("start", "Sounds/soundlogo41");
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

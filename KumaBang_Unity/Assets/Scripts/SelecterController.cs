using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelecterController : MonoBehaviour {

    GameObject sceneManager = null;

	// Use this for initialization
	void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SceneManager");
        this.transform.localScale = new Vector3(0, 0, 0);
        this.transform.DOScale(Vector3.one, 0.1f);

        //アクターアイコン準備
        SceneManager sc = this.sceneManager.GetComponent<SceneManager>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void click () {
        this.sceneManager.GetComponent<SceneManager>().openSelecter();
    }

    public void close() {
        this.transform.DOScale(Vector3.zero, 0.1f);
        Destroy(this.gameObject, 0.1f);
    }
}

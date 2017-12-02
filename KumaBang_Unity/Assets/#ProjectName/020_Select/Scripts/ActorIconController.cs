using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorIconController : MonoBehaviour {

    GameObject sceneManager;

    //セレクターで使用しているかフラグ
    public bool isSelecter = false;
    public GameObject selecter = null;
    public float rad = Mathf.PI;
    public float radMax = 0;
    public float offset = 1.2f;
    public int index = 0;

    // Use this for initialization
    void Start () {
        this.sceneManager = GameObject.Find("SelectSceneManager");
    }

	// Update is called once per frame
    void Update () {
        if (this.isSelecter) {
            Vector3 pos = this.transform.position;
            pos.x = Mathf.Sin(this.rad) * this.offset;
            pos.y = Mathf.Cos(this.rad) * this.offset - 2.0f;
            this.transform.position = pos;
            this.rad += 0.5f;
            if (this.rad > this.radMax) this.rad = this.radMax;
        }
    }

    //一時アイコンとして設定
    public void setOneTime() {
        Destroy(this.gameObject, 2.0f);
    }

    public void flick(bool isRight, int incremental) {
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", incremental * (isRight? -1: 1),
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 1.0f
            )
        );
    }

    public void screenIn(bool isRight, int incremental = 1) {
        Vector3 pos = this.transform.position;
        pos.x = isRight? 4.0f - incremental: -2.0f - incremental;
        this.transform.position = pos;
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", (isRight? -1: 1),
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 1.0f
            )
        );
    }
    public void screenOut(bool isRight, int incremental = 1) {
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", (2.0f + incremental) * (isRight? -1: 1),
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 2.0f
            )
        );
    }

    void OnMouseDown() {
        if (this.isSelecter) {
            this.sceneManager.GetComponent<SelectSceneManager>().changeActor(this.index);
            this.sceneManager.GetComponent<SelectSceneManager>().closeSelecter();
        }
    }
}

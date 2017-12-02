using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    //右矢印フラグ
    public bool isRight = false;

    //シーンマネージャー
    GameObject sceneManager = null;

    //初期位置
    float startX;

    // Use this for initialization
    void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SelectSceneManager");

        //初期位置記録
        this.startX = this.transform.position.x;

        this.setup();
    }
	
    // Update is called once per frame
    void Update () {
    }

    public void OnMouseDown () {
        this.sceneManager.GetComponent<SelectSceneManager>().changeActorNext(this.isRight);
    }

    void setup () {
        this.transform.position = new Vector3(this.startX, 0, 0);
        //左右にフラフラ
        float move = -0.1f;
        if (this.isRight) move *= -1;
        iTween.MoveBy(gameObject,
            iTween.Hash(
                "x", move,
                "easeType", "easeInOutSine",
                "loopType", "pingPong",
                "time", 0.5f
            ));
    }

    public void change() {
        StartCoroutine("switchUI");
        return;
    }
    private IEnumerator switchUI() {
        float move = -3.0f;
        if (this.isRight) move *= -1.0f;

        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", move,
                "easeType", iTween.EaseType.easeInOutSine,
                "time", 0.5f,
                "oncomplete", "OnCompleteCallback",
                "oncompletetarget", this.gameObject
            ));
        yield return new WaitForSeconds(0.5f);

        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", -move,
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 0.5f,
                "oncomplete", "setup",
                "oncompletetarget", this.gameObject,
                "delay", 0.1f
            ));
    }
}

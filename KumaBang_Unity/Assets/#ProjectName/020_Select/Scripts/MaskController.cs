using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaskController : MonoBehaviour {
    SelectSceneManager sceneManager;

    private float _alpha = 1.0f;
    public float alpha{
        set {
            this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, value);
            this._alpha = value;
        }
    }


	// Use this for initialization
	void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SelectSceneManager").GetComponent<SelectSceneManager>();

        this.alpha = 0.0f;
        this.fade(0.6f, 0.2f);
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, this._alpha);
	}

    public void setAlpha(float alpha) {
        if (alpha < 0.0f) alpha = 0.0f;
        if (alpha > 1.0f) alpha = 1.0f;
        this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }

    public void fade(float a, float time) {
        DOTween.To(
            () => this._alpha,          // 何を対象にするのか
            num => this._alpha = num,   // 値の更新
            a,                          // 最終的な値
            time                        // アニメーション時間
        );
    }
    void OnMouseDown() {
        if (this.sceneManager.selecter) this.sceneManager.GetComponent<SelectSceneManager>().closeSelecter();
        this.sceneManager.GetComponent<SelectSceneManager>().closeStartDialog();
        Debug.Log("Selecter cancel");
    }
}

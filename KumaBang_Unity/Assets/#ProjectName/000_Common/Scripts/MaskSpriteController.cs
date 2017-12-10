using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaskSpriteController : MonoBehaviour {

    bool isRotate = false;
    bool isClockWise = true;

    public bool isSendMessage = false;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

    public void RotateIn(float time = 3.0f, float delay = 0.0f) {
        this.isRotate = true;
        this.transform.localScale = Vector3.zero;

        var sequence = DOTween.Sequence();
        sequence.Append(this.transform.DORotate(new Vector3(0.0f, 0.0f, 360), time, RotateMode.FastBeyond360));
        sequence.Join(this.transform.DOScale(new Vector3(16.0f, 16.0f), time)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => {
                GameObject.Destroy(this.gameObject, 0.5f);
                this.animationComplete();
            }));
//        this.transform.DOScale(6.0f, time).SetEase(Ease.InOutQuad);,
    }
    public void RotateOut(float time = 3.0f, float delay = 0.0f) {
        this.isRotate = true;
        this.transform.localScale = new Vector3(16.0f, 16.0f, 1.0f);

        var sequence = DOTween.Sequence();
        sequence.Append(this.transform.DORotate(new Vector3(0.0f, 0.0f, 360), time, RotateMode.FastBeyond360));
        sequence.Join(this.transform.DOScale(new Vector3(0.0f, 0.0f), time)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => {
                GameObject.Destroy(this.gameObject, 0.5f);
                this.animationComplete();
            }));
//        this.transform.DOScale(6.0f, time).SetEase(Ease.InOutQuad);,
    }

    void animationComplete() {
        if (this.isSendMessage) {
            this.transform.parent.gameObject.SendMessage("OnAnimationEnd");
        }
    }
}

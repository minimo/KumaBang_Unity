using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaskSpriteController : MonoBehaviour {

    bool isRotate = false;
    bool isClockWise = true;
	// Use this for initialization
	void Start () {
        this.RotateIn(2.0f);
	}

	// Update is called once per frame
	void Update () {
	}

    public void RotateIn(float time = 1.0f) {
        this.isRotate = true;
        this.transform.localScale = Vector3.zero;

        var sequence = DOTween.Sequence();
        sequence.Append(this.transform.DORotate(new Vector3(0.0f, 0.0f, 360), time, RotateMode.FastBeyond360));
        sequence.Join(this.transform.DOScale(new Vector3(6.0f, 6.0f), time)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => {
                GameObject.Destroy(this.gameObject);
            }));
//        this.transform.DOScale(6.0f, time).SetEase(Ease.InOutQuad);,
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SplashLogoController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var sequence = DOTween.Sequence();
        sequence.Append(this.transform.DOScaleY(0.9f, 2.0f).SetEase(Ease.OutBounce).SetDelay(0.5f));
        sequence.Join(this.transform.DOMoveY(-2.0f, 2.0f).SetEase(Ease.OutBounce).SetDelay(0.5f)
            .OnComplete(() => {
                this.transform.parent.gameObject.SendMessage("OnPlaySound");
            }));

        SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();
        sequence.Append(DOTween.ToAlpha(
            () => renderer.color,
            color => renderer.color = color,
            0.0f,
            1.0f
        ).SetDelay(1.0f).OnComplete(() => {
            this.transform.parent.gameObject.SendMessage("OnSplashComplete");
        }));
	}
	
	// Update is called once per frame
	void Update () {

	}
}

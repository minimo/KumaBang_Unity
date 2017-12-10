using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaskController2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();
        DOTween.ToAlpha(
            () => renderer.color,
            color => renderer.color = color,
            0.0f,
            1.0f
        ).OnComplete(() => {
            Destroy(this.gameObject);
        });
	}
}

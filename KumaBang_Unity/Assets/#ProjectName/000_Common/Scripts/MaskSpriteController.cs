using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaskSpriteController : MonoBehaviour {

    [SerializeField] Vector3 size;

	// Use this for initialization
	void Start () {
        this.RotateIn(10.0f);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(0.0f, 0.0f, 3.0f);
	}

    public void RotateIn(float time = 1.0f) {
        this.transform.localScale = Vector3.zero;
        this.transform.DOScale(20.0f, time);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialPanelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void screenIn() {
        this.transform.DOLocalMove(new Vector3(0, 0), 1.5f)
                .SetEase(Ease.OutElastic)
                .SetDelay(2.0f)
                .OnComplete( ()=>{
                });
    }

    void ScreenOut() {
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    [SerializeField]
    bool isUpper = false;

    // Use this for initialization
    void Start () {
    }
	
    // Update is called once per frame
    void Update () {
    }

    public void changeStatus(int num) {
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "y", 3.0f,
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 0.5f,
                "oncomplete", "OnCompleteCallback",
                "oncompletetarget", this.gameObject
            ));
    }

    //iTween動作終了コールバック
    public void OnCompleteCallback() {
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "y", -3.0f,
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 0.5f
            ));
    }
}

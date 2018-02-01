using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFButtonController : MonoBehaviour {
	
    GameSceneViewController view = null;
    bool isFastForward = false;

	// Use this for initialization
	void Start () {
    }

	// Update is called once per frame
	void Update () {
        if (this.view == null) this.view = this.transform.parent.GetComponent<GameSceneViewController>();

        if (this.isFastForward && !this.view.isEvent) {
            Time.timeScale = 3.0f;
        } else {
            Time.timeScale = 1.0f;
        }
	}

    void OnMouseDown() {
        this.isFastForward = true;
    }
    void OnMouseUp() {
        this.isFastForward = false;
    }
}

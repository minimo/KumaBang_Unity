using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLightController : MonoBehaviour {

    public float progress = 0.001f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.transform.position;
        pos.x += this.progress;
        this.transform.position = pos;

        if (this.transform.position.x > 7.0f) {
    		pos = this.transform.position;
            pos.x  = -6.0f;
            this.transform.position = pos;
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //シーン構造構築
        GameObject go = new GameObject("HomeCanvas");
        go.transform.parent = this.transform;

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

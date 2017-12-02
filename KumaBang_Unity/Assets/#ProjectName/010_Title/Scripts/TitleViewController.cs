using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleViewController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //バックグラウンド作成
        GameObject go = Instantiate((GameObject)Resources.Load("Prefabs/TitleBackGround"));
        go.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

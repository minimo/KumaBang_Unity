﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackGroundController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnMouseDown () {
        this.transform.parent.gameObject.SendMessage("OnTapScreen");
    }
}

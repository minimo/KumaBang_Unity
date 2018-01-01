using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogButtonController : MonoBehaviour {

    [SerializeField] bool isYes = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void click() {
		Debug.Log("dialog button clicked.");
    }
}

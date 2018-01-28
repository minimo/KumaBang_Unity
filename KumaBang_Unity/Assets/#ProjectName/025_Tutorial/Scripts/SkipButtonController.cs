using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipButtonController : MonoBehaviour {

    bool isStart = false;

    [SerializeField] GameObject buttonText;

	// Use this for initialization
	void Start () {
        this.buttonText.GetComponent<Text>().text = "Next";
	}
	
	// Update is called once per frame
	void Update () {		
	}

    public void OnChangeText() {
        this.isStart = true;
        this.buttonText.GetComponent<Text>().text = "Start";
    }

    public void OnClick() {
        if (this.isStart) {
            this.transform.parent.SendMessage("OnStart");
        } else {
            this.transform.parent.SendMessage("OnNextPage");
        }
    }
}

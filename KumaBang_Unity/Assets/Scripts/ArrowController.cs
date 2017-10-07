using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    public bool isLeft = false;

    // Use this for initialization
    void Start () {
        float move = 0.1f;
        if (this.isLeft) move *= -1;
        iTween.MoveBy( gameObject, 
            iTween.Hash(
                "x", move,
                "easeType", "easeInOutSine",
                "loopType", "pingPong",
                "time", 0.5f
            ));
    }
	
    // Update is called once per frame
    void Update () {	
    }
}

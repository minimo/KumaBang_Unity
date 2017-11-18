using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    [SerializeField] bool isUpper = false;

    GameObject sceneManager;

    // Use this for initialization
    void Start () {
        this.sceneManager = GameObject.Find("SceneManager");
    }
	
    // Update is called once per frame
    void Update () {
    }

    public void changeStatus(int num) {
        StartCoroutine("switchUI");
        return;
    }

    private IEnumerator switchUI() {
        float move = -3.0f;
        if (this.isUpper) move *= -1;
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "y", move,
                "easeType", iTween.EaseType.easeInOutSine,
                "time", 0.5f
            ));
        yield return new WaitForSeconds(0.5f);

        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "y", -move,
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 0.5f,
                "delay", 0.1f
            ));
    }
}

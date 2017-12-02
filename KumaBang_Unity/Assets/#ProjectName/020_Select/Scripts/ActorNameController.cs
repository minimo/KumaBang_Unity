using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorNameController : MonoBehaviour {

    [SerializeField] GameObject nowActorName;

    // Use this for initialization
    void Start () {
    }

    public void setName(string newName) {
        this.nowActorName.GetComponent<Text>().text = newName;
    }

    //移動処理
    public void flick(bool isRight, string newName) {
        StartCoroutine(flickCoroutine(isRight, newName));
        return;
    }

    private IEnumerator flickCoroutine(bool isRight, string newName) {
        float move = 10.0f;
        if (isRight) move *= -1.0f;
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", move,
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 0.3f,
                "delay", 0.1f
            ));
        yield return new WaitForSeconds(0.5f);

        this.nowActorName.GetComponent<Text>().text = newName;
        Vector3 pos = this.transform.position;
        pos.x = isRight? 10.0f: -10.0f;
        this.transform.position = pos;
        iTween.MoveBy(this.gameObject,
            iTween.Hash(
                "x", move,
                "easeType", iTween.EaseType.easeOutQuint,
                "time", 0.3f,
                "delay", 0.1f
            ));
        
    }
}

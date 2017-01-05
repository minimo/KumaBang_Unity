using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartDialogController : MonoBehaviour {

    SelectSceneManager sceneManager = null;

    bool isClose = false;

	// Use this for initialization
	void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SelectSceneManager").GetComponent<SelectSceneManager>();

        this.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
        this.transform.DOScale(Vector3.one, 0.3f).SetEase (Ease.OutQuad);
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnButtonYes() {
        if (this.isClose) return;
        this.isClose = true;

        //シーンマネージャーに選択を通知
        this.sceneManager.gameObject.SendMessage("OnDecisionActor");
        this.sceneManager.closeStartDialog();
    }

    void OnButtonNo() {
        if (this.isClose) return;
        this.isClose = true;
        this.sceneManager.closeStartDialog();
    }

    private IEnumerator closeCoroutine() {
        yield return new WaitForSeconds(0.5f);
        this.transform.DOScale(new Vector3(0.0f, 1.0f, 1.0f), 0.3f).SetEase (Ease.OutQuad);
        Destroy(this.gameObject);
    }
}

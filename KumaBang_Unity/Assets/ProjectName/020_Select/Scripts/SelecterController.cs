using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelecterController : MonoBehaviour {

    GameObject sceneManager = null;
    [SerializeField] GameObject actorIcon;

	// Use this for initialization
	void Start () {
        //シーンマネージャー取得
        this.sceneManager = GameObject.Find("SceneManager");

        //出現アニメーション
        this.transform.localScale = new Vector3(0, 0, 0);
        this.transform.DOScale(Vector3.one, 0.1f).SetEase (Ease.OutBounce);

        //アクターアイコン準備
        SceneManager sc = this.sceneManager.GetComponent<SceneManager>();
        Sprite [] iconImages = sc.actorIconImages;

        float radUnit = (Mathf.PI * 2) / sc.getNumActor();
        for (int i = 0; i < sc.getNumActor(); i++) {
            GameObject icon = Instantiate(this.actorIcon, new Vector3(0, 0, -10), Quaternion.identity, this.transform);
            icon.GetComponent<SpriteRenderer>().sprite = iconImages[i];
            ActorIconController asc = icon.GetComponent<ActorIconController>();
            asc.rad = 0;
            asc.radMax = radUnit * i;
            asc.selecter = this.gameObject;
            asc.index = i;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}

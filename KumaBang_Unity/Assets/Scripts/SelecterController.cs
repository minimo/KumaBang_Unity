using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        Vector3 pos = this.transform.position;
        GameObject icon = Instantiate(this.actorIcon, pos, Quaternion.identity, this.transform);
        icon.GetComponent<SpriteRenderer>().sprite = iconImages[0];
//        icon.GetComponent<SpriteRenderer>().sortingLayerName = "Selecter";
	}
	
	// Update is called once per frame
	void Update () {
	}
}

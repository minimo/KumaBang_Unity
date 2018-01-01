using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelController : MonoBehaviour {

    //ドラッグ用
	Vector3 screenPoint, pointOffset;

    //ステージ上座標
    public int stageX = 0, stageY = 0;
    public int offsetX, offsetY;

    //パネルインデックス
    public int index = 0;

    //各種フラグ
    public bool isOnPlayer = false;     //プレイヤーが乗っている
    public bool isDrop = false;         //ドロップ中
    public bool isPointing = false;     //ユーザーポイント中
    public bool isOnItem = false;       //アイテムが乗っている
    public bool isOnEnemy = false;       //敵が乗っている

	// Use this for initialization
	void Start () {		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = this.transform.position;
        pos.z = pos.y;
        if (this.isPointing) pos.z = -5.0f;
        this.transform.position = pos;
    }

    public void setStagePosition(int x, int y) {
        this.stageX = x;
        this.stageY = y;
    }
    public void setOffsetPosition(int x, int y) {
        this.offsetX = x;
        this.offsetY = y;
    }

    //ポイント開始処理
    void OnMouseDown() {
        if (this.isDrop || this.isOnEnemy || this.isOnItem || this.isOnPlayer || this.isPointing) return;
        this.isPointing = true;

		//パネル位置をスクリーン座標に変換
		this.screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
		//ワールド座標上のマウスカーソルとパネルの座標の差分
		this.pointOffset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    //パネルドラッグ処理
    void OnMouseDrag() {
		Vector3 currentScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint (currentScreenPoint) + this.pointOffset;
		this.transform.position = currentPosition;
    }

    //ポイント離す
    void OnMouseUp() {
        if (!this.isPointing) return;
        this.isPointing = false;

        //パネル位置量子化
        float x = this.stageX + this.offsetX;
        float y = this.stageY + this.offsetY;
        this.transform.DOLocalMove(new Vector3(x, y), 0.5f)
                    .SetEase(Ease.OutBounce);
    }
}

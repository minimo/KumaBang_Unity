using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelController : MonoBehaviour {

    public GameSceneViewController view;

    //ドラッグ用
	Vector3 screenPoint, pointOffset;

    //ステージ上座標
    public int stageX = 0, stageY = 0;
    int clickX, clickY;
    public int offsetX, offsetY;
    int beforeStageX, beforeStageY; //移動前座標

    //パネルインデックス
    public int index = 0;

    //各種フラグ
    public bool isStart = false;
    public bool isGoal = false;
    public bool isOnPlayer = false; //プレイヤーが乗っている
    public bool isDrop = false;     //ドロップ中
    public bool isPointing = false; //ユーザーポイント中
    public bool isOnItem = false;   //アイテムが乗っている
    public bool isOnEnemy = false;  //敵が乗っている
    public bool isDisableMove = false; //移動不可
    public bool isDisableShaffle = false; //シャッフル不可
    public bool isOnOtherPanel = false; //他のパネルの上にいる

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        int px = this.view.playerController.stageX;
        int py = this.view.playerController.stageY;
        if (this.stageX == px && this.stageY == py) this.isOnPlayer = true;


        //Z座標調整
        if (!this.isDrop) {
            Vector3 pos = this.transform.position;
            pos.z = pos.y;
            if (this.isPointing) pos.z = -5.0f;
            this.transform.position = pos;
        }
    }

    public void setStagePosition(int x, int y) {
        this.stageX = x;
        this.stageY = y;
    }

    public void moveStagePosition(int x, int y) {
        this.stageX = x;
        this.stageY = y;

        //パネル位置量子化
        float sx = this.stageX + this.offsetX;
        float sy = this.stageY + this.offsetY;
        this.transform.DOLocalMove(new Vector3(sx+0.5f, -sy-0.5f), 0.1f).SetEase(Ease.OutBounce);
    }

    public void setOffsetPosition(int x, int y) {
        this.offsetX = x;
        this.offsetY = y;
    }

    //パネル移動可能かチェック
    public bool checkEnableMove() {
        if (this.isDrop 
            || this.isOnEnemy 
            || this.isOnItem 
            || this.isOnPlayer 
            || this.isPointing 
            || this.isDisableMove
            || !this.view.isGameStart) return false;
        return true;
    }

    //ポイント開始処理
    void OnMouseDown() {
        if (!this.checkEnableMove()) return;
        this.isPointing = true;

		//パネル位置をスクリーン座標に変換
		this.screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
		//ワールド座標上のマウスカーソルとパネルの座標の差分
		this.pointOffset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y - 20.0f, screenPoint.z));

        //親に自分がアクティブパネルである事を設定
        this.view.activePanel = this.gameObject;
        this.view.activePanelController = this;

        //フラグリセット
        this.isOnOtherPanel = false;
    }

    //パネルドラッグ処理
    void OnMouseDrag() {
        if (!this.isPointing) return;
		Vector3 currentScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint (currentScreenPoint) + this.pointOffset;
		this.transform.position = currentPosition;

        //ステージ上パネル座標計算
        int x = (int)(this.transform.position.x);
        int y = (int)(-this.transform.position.y);
        if (x < 0) x = 0; else if (x > 4) x = 4;
        if (y < 0) y = 0; else if (y > 4) y = 4;
        PanelController p = this.view.getPanel(x, y);
        if (p == null) {
            this.stageX = x;
            this.stageY = y;
        } else if (p.checkEnableMove()) {
            this.stageX = x;
            this.stageY = y;
        }

//        Debug.Log("X:"+this.stageX+" Y:"+this.stageY);
    }

    //ポイント離す
    void OnMouseUp() {
        if (!this.isPointing) return;
        this.isPointing = false;

        //パネル位置量子化
        float x = this.stageX + this.offsetX;
        float y = this.stageY + this.offsetY;
        this.transform.DOLocalMove(new Vector3(x+0.5f, -y-0.5f), 0.3f).SetEase(Ease.OutBounce);

        //親のアクティブパネル設定を解除
        this.view.activePanel = null;
        this.view.activePanelController = null;
    }
}

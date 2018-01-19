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
    public bool isOnPlayerBefore = false;
    public bool isDrop = false;     //ドロップ中
    public bool isPointing = false; //ユーザーポイント中
    public bool isOnItem = false;   //アイテムが乗っている
    public bool isOnEnemy = false;  //敵が乗っている
    public bool isDisableMove = false; //移動不可
    public bool isDisableShaffle = false; //シャッフル不可
    public bool isOnOtherPanel = false; //他のパネルの上にいる

    //投入後
    float enterZ = 0.0f;

	// Use this for initialization
	void Start () {
        this.enterZ = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.view.playerController) {
            int px = this.view.playerController.stageX;
            int py = this.view.playerController.stageY;
            if (this.stageX == px && this.stageY == py)
                this.isOnPlayer = true;
            else
                this.isOnPlayer = false;
        }

        //プレイヤーが降りたのでパネルドロップ
//        if (this.isOnPlayerBefore && !this.isOnPlayer) this.drop();

        //Z座標調整
        if (!this.isDrop) {
            Vector3 pos = this.transform.position;
            pos.z = pos.y;
            pos.z += this.enterZ;
            if (this.enterZ < 0) this.enterZ = 0.0f;
            this.enterZ -= 0.01f;
            if (this.isPointing) pos.z = -15.0f;
            this.transform.position = pos;
        }

        this.isOnPlayerBefore = this.isOnPlayer;
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

    //パネルドロップ
    public void drop() {
        if (this.isDrop || this.isPointing) return;
        this.isDrop = true;
        this.view.stageMap[this.stageX, this.stageY] = null;

        var seq = DOTween.Sequence();
        seq.Append(this.transform.DOLocalMove(new Vector3(0.0f, -2.0f), 2.0f)
            .SetEase(Ease.InQuad)
            .SetRelative()
        );

        SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();
        DOTween.ToAlpha(
            () => renderer.color,
            color => renderer.color = color,
            0.0f,
            1.0f
        ).OnComplete(() => {
            Destroy(this.gameObject);
        });
        SoundManagerController.Instance.playSE("paneldrop");
    }

    //自分を落として新規パネルを投入する
    public void change(int index) {
        if (this.isDrop || this.isPointing) return;
        this.isDrop = true;

        GameObject newPanel = this.view.enterPanel(index, this.stageX, this.stageY);
        this.view.stageMap[this.stageX, this.stageY] = newPanel;

        SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();
        DOTween.ToAlpha(
            () => renderer.color,
            color => renderer.color = color,
            0.0f,
            1.0f
        ).OnComplete(() => {
            Destroy(this.gameObject);
        });
        SoundManagerController.Instance.playSE("paneldrop");
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

    //パネルが進行方向に対し通過可能かチェック
    public bool checkEnablePass(int direction) {
        switch (this.index) {
            case 1:
                if (direction == 1 || direction == 3) return true;
                break;
            case 2:
                if (direction == 0 || direction == 2) return true;
                break;
            case 3:
                return true;
            case 4:
                if (direction == 0 || direction == 3) return true;
                break;
            case 5:
                if (direction == 0 || direction == 1) return true;
                break;
            case 6:
                if (direction == 2 || direction == 3) return true;
                break;
            case 7:
                if (direction == 1 || direction == 2) return true;
                break;
            case 8:
                if (direction == 3) return true;
                break;
            case 9:
                if (direction == 0) return true;
                break;
            case 10:
                if (direction == 1) return true;
                break;
            case 11:
                if (direction == 2) return true;
                break;
            case 12:
                if (direction == 3) return true;
                break;
            case 13:
                if (direction == 0) return true;
                break;
            case 14:
                if (direction == 1) return true;
                break;
            case 15:
                if (direction == 2) return true;
                break;
        }
        return false;
    }

    //ポイント開始処理
    void OnMouseDown() {
        if (!this.checkEnableMove() || this.isPointing) return;
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
        this.transform.DOLocalMove(new Vector3(x+0.5f, -y-0.5f), 0.3f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => {
                this.isPointing = false;
            });

        //親のアクティブパネル設定を解除
        this.view.activePanel = null;
        this.view.activePanelController = null;
    }
}

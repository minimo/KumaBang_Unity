using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {

    public GameSceneViewController view;

    //ステージ上座標
    public int stageX = 0, stageY = 0;
    int beforeStageX = 0, beforeStageY = 0; //前フレーム

    //前フレームワールド座標
    Vector3 beforePosition = new Vector3(0.0f, 0.0f, 0.0f);

    //現在進行方向
    // 0:上 1:右 2:下 3:左
    int direction = 0;

    //進行方向チェックポイント
    Vector3 checkPoint = new Vector3();

    //移動速度
    float speed = 0.01f;

    //各種フラグ
    bool isStart = false;
    bool isMiss = false;

	private int idX = Animator.StringToHash("x"), idY = Animator.StringToHash("y");
	private int idMiss = Animator.StringToHash("miss");
	private Animator animator = null;

	void Start () {
        this.view = this.transform.parent.GetComponent<GameSceneViewController>();
		this.animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
        if (!this.isStart || this.isMiss) return;

        //ステージ上座標
        this.stageX = Mathf.FloorToInt(this.transform.position.x);
        this.stageY = Mathf.FloorToInt(-this.transform.position.y);
        Debug.Log("wx: "+ this.transform.position.x+" wy:"+this.transform.position.y+" x: "+ this.stageX+" y:"+this.stageY);

        //次のパネル移動を検知
        if (this.beforeStageX != this.stageX && this.beforeStageY != this.stageX) {
            //足元のパネルを取得
            PanelController pc = this.view.getPanel(this.stageX, this.stageY);
            if (pc) {
                if (pc.isPointing || pc.isDrop || !pc.checkEnablePass(this.direction)) this.miss();
            } else {
                this.miss();
            }
            Debug.Log("wx: "+ this.transform.position.x+" wy:"+this.transform.position.y);
        }

        //移動処理
        Vector3 p = this.transform.position;
        switch (this.direction) {
            case 0:
                p.y += this.speed;
                break;
            case 1:
                p.x += this.speed;
                break;
            case 2:
                p.y -= this.speed;
                break;
            case 3:
                p.x -= this.speed;
                break;
        }
        this.transform.position = p;

        this.checkCheckPointPass();

        //前フレームからの移動量
        float moveX = p.x - this.beforePosition.x;
        float moveY = p.y - this.beforePosition.y;

        //アニメーションに移動量をセット
		this.animator.SetFloat(idX, moveX);
    	this.animator.SetFloat(idY, moveY);

        //現座標保存
        this.beforeStageX = this.stageX;
        this.beforeStageY = this.stageY;
        this.beforePosition = this.transform.position;
	}

    public void setStartPosition(int x, int y) {
        this.stageX = x;
        this.stageY = y;
        this.beforeStageX = -1;
        this.beforeStageY = -1;
    }

    public void setDirection(int dir) {
        if (dir < 0) dir = 0;
        if (dir > 3) dir = 3;
        this.direction = dir;
    }

    public void setCheckPoint(float x, float y) {
        this.checkPoint.x = x + 0.5f;
        this.checkPoint.y = y;
    }

    //チェックポイント通過を検知
    public void checkCheckPointPass() {
        if (this.direction == 0 && this.transform.position.y < this.checkPoint.y) return;
        if (this.direction == 1 && this.transform.position.x < this.checkPoint.x) return;
        if (this.direction == 2 && this.transform.position.y > this.checkPoint.y) return;
        if (this.direction == 3 && this.transform.position.x > this.checkPoint.x) return;

        int nextDirection = 0;
        int idx = this.view.getPanelIndex(this.stageX, this.stageY);
        switch (idx) {
            case 1:
            case 2:
            case 3:
                nextDirection = this.direction;
                break;
            case 4:
                if (this.direction == 3) nextDirection = 2; else nextDirection = 1;
                break;
            case 5:
                if (this.direction == 1) nextDirection = 2; else nextDirection = 3;
                break;
            case 6:
                if (this.direction == 2) nextDirection = 1; else nextDirection = 0;
                break;
            case 7:
                if (this.direction == 1) nextDirection = 0; else nextDirection = 3;
                break;
        }

        Vector3 p = this.transform.position;
        switch (nextDirection) {
            case 0:
                p.y += 1.0f;
                break;
            case 1:
                p.x += 1.0f;
                break;
            case 2:
                p.y -= 1.0f;
                break;
            case 3:
                p.x -= 1.0f;
                break;
        }
        this.checkPoint = p;
        this.direction = nextDirection;
    }

    void miss() {
        this.isMiss = true;
        var seq = DOTween.Sequence();
        seq.Append(this.transform.DOLocalMove(new Vector3(0.0f, 0.2f), 0.1f)
            .SetEase(Ease.InOutCirc)
            .SetRelative()
        );
        seq.Append(this.transform.DOLocalMove(new Vector3(0.0f, -0.2f), 0.1f)
            .SetEase(Ease.InOutCirc)
            .SetRelative()
        );
    	this.animator.SetBool(idMiss, true);

        this.view.SendMessage("OnPlayerMiss");
    }

    void OnReadyStart() {
        this.isStart = true;
    }
}

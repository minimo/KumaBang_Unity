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

    //移動制御用Tweener
    Tween tweener;

    //移動速度
    float speed = 2.0f;

    //乗っているパネル
    public PanelController footPanel = null;

    //各種フラグ
    bool isStart = false;
    bool isMiss = false;

    //アニメーション制御用
	private Animator animator = null;
	private int idX = Animator.StringToHash("x"), idY = Animator.StringToHash("y");
	private int idMiss = Animator.StringToHash("miss");
	private int idClear = Animator.StringToHash("clear");

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

        //次のパネル移動を検知
        if (this.beforeStageX != this.stageX || this.beforeStageY != this.stageY) {
            //足元のパネルを取得
            PanelController pc = this.view.getPanel(this.stageX, this.stageY);
            if (pc) {
                if (pc.isPointing || pc.isDrop || !pc.checkEnablePass(this.direction)) {
                    this.miss();
                } else {
                    this.footPanel.drop();
                    this.view.addScore(1000, this.footPanel.gameObject.transform.position);
                    this.footPanel = pc;
                }
            } else {
                this.miss();
            }
//            Debug.Log("change panel");
//            Debug.Log("wx: "+ this.transform.position.x+" wy:"+this.transform.position.y+" x: "+ this.stageX+" y:"+this.stageY);
        }

        //前フレームからの移動量
        float moveX = this.transform.position.x - this.beforePosition.x;
        float moveY = this.transform.position.y - this.beforePosition.y;

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

    //次のパネルへ移動
    public void moveToNextPanel() {
        //次進行方向
        int nextDirection = -1;

        //足元のパネルを取得
        PanelController pc = this.view.getPanel(this.stageX, this.stageY);
        switch (pc.index) {
            case 1:
            case 2:
            case 3:
                nextDirection = this.direction;
                break;
            case 4:
                if (this.direction == 0) nextDirection = 1;
                if (this.direction == 3) nextDirection = 2;
                break;
            case 5:
                if (this.direction == 0) nextDirection = 3;
                if (this.direction == 1) nextDirection = 2;
                break;
            case 6:
                if (this.direction == 2) nextDirection = 1;
                if (this.direction == 3) nextDirection = 0;
                break;
            case 7:
                if (this.direction == 1) nextDirection = 0;
                if (this.direction == 2) nextDirection = 3;
                break;
        }
        this.direction = nextDirection;

        //次のパネルへの移動量
        float x = 0, y = 0;
        if (this.direction == 0) y = 1.0f;
        if (this.direction == 1) x = 1.0f;
        if (this.direction == 2) y = -1.0f;
        if (this.direction == 3) x = -1.0f;

        this.tweener = this.transform.DOLocalMove(new Vector3(x, y), this.speed)
            .SetEase(Ease.Linear)
            .SetRelative()
            .OnComplete( ()=>{
                this.moveToNextPanel();
            });
    }

    void miss() {
        this.tweener.Kill();
        this.isMiss = true;
        var seq = DOTween.Sequence();
        seq.Append(this.transform.DOLocalMove(new Vector3(0.0f, 0.2f), 0.1f)
            .SetEase(Ease.OutSine)
            .SetRelative()
        );
        seq.Append(this.transform.DOLocalMove(new Vector3(0.0f, -0.2f), 0.1f)
            .SetEase(Ease.InSine)
            .SetRelative()
        );
    	this.animator.SetBool(idMiss, true);

        this.view.SendMessage("OnPlayerMiss");
        Debug.Log("Player miss.");
    }
    void clear() {
        this.tweener.Kill();
        this.isMiss = true;
        var seq = DOTween.Sequence();
        seq.Append(this.transform.DOLocalMove(new Vector3(0.0f, 0.2f), 0.1f)
            .SetEase(Ease.OutSine)
            .SetRelative()
        );
        seq.Append(this.transform.DOLocalMove(new Vector3(0.0f, -0.2f), 0.1f)
            .SetEase(Ease.InSine)
            .SetRelative()
        );
    	this.animator.SetBool(idMiss, true);

        this.view.SendMessage("OnPlayerMiss");
        Debug.Log("Player miss.");
    }

    void OnReadyStart() {
        this.isStart = true;

        float x = 0.0f, y = 0.0f;
        if (this.direction == 0) y = 1.0f;
        if (this.direction == 1) x = 1.0f;
        if (this.direction == 2) y = -1.0f;
        if (this.direction == 3) x = -1.0f;

        this.tweener = this.transform.DOLocalMove(new Vector3(x, y), this.speed)
            .SetEase(Ease.Linear)
            .SetRelative()
            .OnComplete(()=>{
                this.moveToNextPanel();
            });
    }
}

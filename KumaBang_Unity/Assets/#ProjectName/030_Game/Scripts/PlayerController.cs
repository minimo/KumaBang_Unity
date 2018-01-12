using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {

    public GameSceneViewController view;

    public GameObject spriteObject = null;

    //ステージ上座標
    public int stageX = 0, stageY = 0;

    //現在進行方向
    // 0:右 1:下 2:左 3:上
    int direction = 0;

    //移動速度
    float speed = 0.01f;

    //各種フラグ
    bool isStart = false;

	private int idX = Animator.StringToHash("x"), idY = Animator.StringToHash("y");
	private Animator animator = null;

	void Start () {
		this.animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (!this.isStart) return;

        //ステージ上座標
        this.stageX = (int)(this.transform.position.x);
        this.stageY = (int)(-this.transform.position.y);

        float moveX = 0.01f;
        float moveY = 0.00f;

        Vector3 p = this.transform.position;
        p.x += moveX;
        p.y += moveY;
        this.transform.position = p;

        //アニメーションに移動量をセット
		this.animator.SetFloat(idX, moveX);
    	this.animator.SetFloat(idY, moveY);
	}

    void setStartPosition(int x, int y) {
        this.stageX = x;
        this.stageY = y;
    }

    void OnReadyStart() {
        this.isStart = true;
    }
}

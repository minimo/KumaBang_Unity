﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    //操作用GameObject
    public GameObject UpperUI;
    public GameObject LowerUI;
    public GameObject Arrow_R;
    public GameObject Arrow_L;
    public GameObject BackGround;
    public GameObject Selecter;

    //アクター画像
    [SerializeField] Sprite [] actorImages;

    //アクターアイコン
    [SerializeField] Sprite [] actorIconImages;

    //アクター最大数
    [SerializeField] int maxActor = 7;

    //選択中アクター番号
    int nowActor = 0;

    //アクターの土台
    GameObject actorBase;
    //アクターアイコンの土台
    GameObject iconBase;

    //アクター元プレハブ
    [SerializeField] GameObject actor;

    //追加済みアクターリスト
    List<GameObject> actors = new List<GameObject>();

    //アクター切り替えインターバル
    float actoSwitchInterval = 0.5f;

    //アクターアイコン元プレハブ
    [SerializeField] GameObject actorIcon;

    //追加済みアクターアイコンリスト
    List<GameObject> actorIcons = new List<GameObject>();

    //アクター名元プレハブ
    [SerializeField] GameObject actorName;

    //セレクター
    GameObject selecter = null;

    string [] actorNames = {
        "Actor 1",
        "Actor 2",
        "Actor 3",
        "Actor 4",
        "Actor 5",
        "Actor 6",
        "Actor 7"
    };

    //フェード
    [SerializeField] GameObject fadeStarCanvas;
    Fade fadeStar = null;
    [SerializeField] GameObject fadeBackCanvas;
    Fade fadeBack = null;

    // Use this for initialization
    void Start () {
        this.actorBase = GameObject.Find("ActorBase");
        this.iconBase = GameObject.Find("IconBase");

        this.addActor();
        this.addActor();
        this.addActor();
        this.addActor();
        this.addActor();

        this.actors[0].transform.position = new Vector3(0, 0, 0);
        this.initIconPosition(0);

        //フェード初期処理
        this.fadeStar = this.fadeStarCanvas.GetComponent<Fade>();
    }
	
    // Update is called once per frame
    void Update () {
    }

    //アイコン位置初期化
    public void initIconPosition(int center = -99) {
        if (center == -99) {
            center = this.nowActor;
        } else {
            if (center >= this.actorIcons.Count || center < 0) center = 0;
        }
        float y = -4.35f;

        for (int i = 0; i < this.actorIcons.Count; i++) {
            this.actorIcons[i].transform.position = new Vector3(20, y, 0);
        }

        if (this.actorIcons.Count < 4) {
            this.actorIcons[center].transform.position = new Vector3(0, y, 0);
            if (this.actors.Count > 1) this.actorIcons[this.iconOrder(center, 1)].transform.position = new Vector3(1.0f, y, 0);
            if (this.actors.Count > 2) this.actorIcons[this.iconOrder(center, 2)].transform.position = new Vector3(-1.0f, y, 0);
        } else {
            for (int i = -2; i < 3; i++) {
                int num = this.iconOrder(center, i);
                if (i < 3) {
                    this.actorIcons[num].transform.position = new Vector3(i * 1.0f, y, 0);
                } else if (i < 5) {
                    float x = -3.0f + (i-2) * 1.0f;
                    if (this.actorIcons.Count == 4) x += 1.0f;
                    this.actorIcons[num].transform.position = new Vector3(x, y, 0);
                } else {
                    this.actorIcons[num].transform.position = new Vector3(10, y, 0);
                }
            }
        }
    }

    void moveIcon(bool isRight, int incremental) {
        int diff = isRight?incremental: -incremental;
        //次のセンターアイコン取得
        int center = this.iconOrder(this.nowActor, diff);

        //アイコンを配置
        int num;
        for (int i = -2; i < 3; i++) {
            num = this.iconOrder(center, i);
            Vector3 pos = this.actorIcons[num].transform.position;
            pos.x = diff + i;
            this.actorIcons[num].transform.position = pos;
        }

        //アイコン移動処理
        for (int i = 0; i < this.actors.Count; i++) {
            this.actorIcons[i].GetComponent<ActorIconController>().flick(isRight, incremental);
        }
    }

    //アイコン移動
    void moveIcon_old(bool isRight, int incremental) {
        //アイコン移動処理
        int num = 0;
        for (int i = -2; i < 3; i++) {
            num = this.iconOrder(this.nowActor, i);
            this.actorIcons[num].GetComponent<ActorIconController>().flick(isRight, incremental);
        }
        int numIn = this.iconOrder(this.nowActor, -3);
        int numOut = this.iconOrder(this.nowActor, 2);
        if (isRight) {
            numIn = this.iconOrder(this.nowActor, 3);
            numOut = this.iconOrder(this.nowActor, -2);
        }

        if (incremental == 1) {
            //入るアイコンと出て行くアイコンが同じ場合ダミーを作成する
            if (numIn == numOut) {
                Vector3 pos = this.actorIcons[numOut].transform.position;
                GameObject outIcon = Instantiate(this.actorIcon, pos, Quaternion.identity, this.iconBase.transform);
                outIcon.GetComponent<SpriteRenderer>().sprite = this.actorIconImages[numOut];
                outIcon.GetComponent<ActorIconController>().setOneTime();
                outIcon.GetComponent<ActorIconController>().screenOut(isRight, incremental);

                this.actorIcons[numIn].GetComponent<ActorIconController>().screenIn(isRight, incremental);
            } else {
                this.actorIcons[numOut].GetComponent<ActorIconController>().screenOut(isRight, incremental);
                this.actorIcons[numIn].GetComponent<ActorIconController>().screenIn(isRight, incremental);
            }
        } else {            
        }
    }

    int iconOrder(int center, int inc) {
        if (inc == 0) return center;
        int ret = (center + inc) % this.actors.Count;
        if (ret < 0) ret += this.actors.Count;
        return ret;
    }

    //アクター切り替え
    public void changeActor(int num) {
        if (num < 0 || num > this.actors.Count - 1) return;
        if (num  == this.nowActor) return;

        //立ち絵移動処理
        GameObject nextActor = this.actors[num];
        nextActor.transform.position = new Vector3(0, 0.0f, 0.0f);

        GameObject nowActor = this.actors[this.nowActor];
        nowActor.GetComponent<ActorController>().flick(true);

        //アクター名移動処理
        this.actorName.GetComponent<ActorNameController>().flick(true, this.actorNames[num]);

        this.nowActor = num;
    }

    //アクター切り替え
    public void changeActorNext(bool isRight, int incremental = 1) {
        if (this.maxActor == 1
            || this.actors[this.nowActor].GetComponent<ActorController>().isMoving
            || this.actors.Count == 1) return;

        //現在メインの左右に隣合う番号のアクターを配置
        int next = 0;
        float move = 10.0f;

        if (isRight) {
            next = this.nowActor + incremental;
            if (next >= this.actors.Count) next -= this.actors.Count;
            move *= -1.0f;
        } else {
            next = this.nowActor - incremental;
            if (next < 0) next += this.actors.Count;
        }

        //立ち絵移動処理
        GameObject nextActor = this.actors[next];
        nextActor.transform.position = new Vector3(-move, 0.0f, 0.0f);
        nextActor.GetComponent<ActorController>().flick(isRight, 0.5f);

        GameObject nowActor = this.actors[this.nowActor];
        nowActor.GetComponent<ActorController>().flick(isRight);

        //アクター名移動処理
        this.actorName.GetComponent<ActorNameController>().flick(isRight, this.actorNames[next]);

        //アイコン切り替え
        this.moveIcon(isRight, incremental);

        this.nowActor = next;

        //UI移動処理
        this.UpperUI.GetComponent<UIController>().changeStatus(this.nowActor);
        this.LowerUI.GetComponent<UIController>().changeStatus(this.nowActor);
        this.Arrow_L.GetComponent<ArrowController>().change();
        this.Arrow_R.GetComponent<ArrowController>().change();
    }

    //新アクター追加
    public bool addActor() {
        if (this.actors.Count == this.maxActor) return false;

        //立ち絵追加
		Vector3 pos = new Vector3(10.0f, 0, 0);
        GameObject ac = Instantiate(this.actor, pos, Quaternion.identity, this.actorBase.transform);
        if (ac == null) return false;
        ac.GetComponent<SpriteRenderer>().sprite = this.actorImages[this.actors.Count];
        this.actors.Add(ac);

        //アイコン追加
        if (this.nowActor < 3) {
    		pos = new Vector3(5.0f, -4.3f, 0);
        } else {
    		pos = new Vector3(5.0f, -4.3f, 0);
        }
        GameObject icon = Instantiate(this.actorIcon, pos, Quaternion.identity, this.iconBase.transform);
        if (icon == null) return false;
        icon.GetComponent<SpriteRenderer>().sprite = this.actorIconImages[this.actorIcons.Count];
        this.actorIcons.Add(icon);
        return true;
    }

    //新規アイコン追加
    void AddActorIcon() {
        //追加アイコンが表示外
        if (this.nowActor > 2) return;
    }

    public void OnSwipe (Vector2 dir) {
        if (dir.x < 0) 
            this.changeActorNext(true);
        else if (dir.x > 0)
            this.changeActorNext(false);
    }

    //セレクターを画面に追加
    public void openSelecter() {
        if (this.selecter) return;
		Vector3 pos = new Vector3(0.0f, 0, 0);
        this.selecter = Instantiate(this.Selecter, pos, Quaternion.identity, this.actorBase.transform);
    }

    //セレクターを閉じる
    public void closeSelecter() {
        if (this.selecter == null) return;
        Destroy(this.selecter);
        this.selecter = null;
    }

    Sprite [] getActorIconImage() {
        return this.actorIconImages;
    }

    //新規アクター追加フェードインアウト
    public void addActorFadeInOut() {
        StartCoroutine("addActorFadeCoroutine");
    }
    private IEnumerator addActorFadeCoroutine() {
        this.fadeStar.FadeIn(0.5f);
        yield return new WaitForSeconds(1.0f);
        this.changeActor(this.actors.Count - 1);
        this.initIconPosition();
		this.fadeStar.FadeOut(0.5f);
    }

    //背景切り替えフェードインアウト
    public void backgroundFadeInOut() {
        StartCoroutine("backgrondFadeCoroutine");
    }
    private IEnumerator backgrondFadeCoroutine() {
        this.fadeBack.FadeIn(0.5f);
        yield return new WaitForSeconds(1.0f);
		this.fadeBack.FadeOut(0.5f);
    }
}


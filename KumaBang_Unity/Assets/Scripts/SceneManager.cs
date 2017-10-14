using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public GameObject UpperUI;
    public GameObject LowerUI;
    public GameObject Arrow_R;
    public GameObject Arrow_L;

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

    //アクターアイコン元プレハブ
    [SerializeField] GameObject actorIcon;

    //追加済みアクターアイコンリスト
    List<GameObject> actorIcons = new List<GameObject>();

    //アクター画像
    [SerializeField] Sprite [] actorImages;
    //アクターアイコン
    [SerializeField] Sprite [] actorIconImages;

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
    }
	
    // Update is called once per frame
    void Update () {
    }

    //アイコン位置初期化
    void initIconPosition(int center) {

        if (center >= this.actorIcons.Count || center < 0) center = 0;
        float y = -4.35f;

        if (this.actorIcons.Count < 4) {
            this.actorIcons[center].transform.position = new Vector3(0, y, 0);
            if (this.actors.Count > 1) this.actorIcons[this.iconOrder(center, 1)].transform.position = new Vector3(1.0f, y, 0);
            if (this.actors.Count > 2) this.actorIcons[this.iconOrder(center, 2)].transform.position = new Vector3(-1.0f, y, 0);
        } else {
            for (int i = 0; i < this.actorIcons.Count; i++) {
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

    int iconOrder(int center, int inc) {
        if (inc == 0) return center;
        int ret = (center + inc) % this.actors.Count;
        return ret;
    }

    //アクター切り替え（番号指定）
    public void changeActor(int center) {
        if (this.maxActor == 1
            || this.actors[this.nowActor].GetComponent<ActorController>().isMoving
            || this.actors.Count == 1
            || center >= this.actors.Count
            || center == this.nowActor) return;

        //差分を計算
        bool isRight = true;
        int diff = center - this.nowActor;
        if (diff > 3) {
        }
        diff = Mathf.Abs(diff);
        this.changeActorNext(isRight, diff);
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
        nextActor.GetComponent<ActorController>().flick(isRight);

        GameObject nowActor = this.actors[this.nowActor];
        nowActor.GetComponent<ActorController>().flick(isRight);

        //アイコン移動処理
        for (int i = 0; i < this.actorIcons.Count; i++) {
            this.actorIcons[i].GetComponent<ActorIconController>().flick(isRight, incremental);
        }
        this.nowActor = next;

        //UI移動処理
        this.UpperUI.GetComponent<UIController>().changeStatus(this.nowActor);
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
		pos = new Vector3(0, -4.3f, 0);
        GameObject icon = Instantiate(this.actorIcon, pos, Quaternion.identity, this.iconBase.transform);
        if (icon == null) return false;
        icon.GetComponent<SpriteRenderer>().sprite = this.actorIconImages[this.actorIcons.Count];
        this.actorIcons.Add(icon);

        return true;
    }

    public void OnSwipe (Vector2 dir) {
        if (dir.x < 0) 
            this.changeActorNext(true);
        else if (dir.x > 0)
            this.changeActorNext(false);
    }
}

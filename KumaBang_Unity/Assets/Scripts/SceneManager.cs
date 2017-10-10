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

        //アイコン位置初期化
        for (int i = 0; i < this.actorIcons.Count; i++) {
            if (i < 5) {
            } else {
            }
        }
    }
	
    // Update is called once per frame
    void Update () {
    }

    //アクター切り替え
    public void changeActor(bool isRight) {
        if (this.maxActor == 1 || this.actors[this.nowActor].GetComponent<ActorController>().isMoving) return;

        //現在メインの左右に隣合う番号のアクターを配置
        int next = 0;
        float move = 10.0f;

        if (isRight) {
            next = this.nowActor + 1;
            if (next == this.actors.Count) next = 0;
            move *= -1.0f;
        } else {
            next = this.nowActor - 1;
            if (next < 0) next = this.actors.Count - 1;
        }

        GameObject nextActor = this.actors[next];
        nextActor.transform.position = new Vector3(-move, 0.0f, 0.0f);
        nextActor.GetComponent<ActorController>().flick(isRight);

        GameObject nowActor = this.actors[this.nowActor];
        nowActor.GetComponent<ActorController>().flick(isRight);

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
            this.changeActor(true);
        else if (dir.x > 0)
            this.changeActor(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public GameObject UpperUI;
    public GameObject LowerUI;
    public GameObject Arrow_R;
    public GameObject Arrow_L;

    //アクター最大数
    [SerializeField]
    int maxActor = 7;

    //選択中アクター番号
    int nowActor = 0;

    //アクターの土台
    GameObject actorBase;

    //アクター元プレハブ
    [SerializeField]
    GameObject actor;

    //追加済みアクターリスト
    List<GameObject> actors = new List<GameObject>();

    //アクター画像
    [SerializeField]
    Sprite [] actorImages;

    // Use this for initialization
    void Start () {
//        Sprite [] images = Resources.LoadAll<Sprite>("Images/Actor");
        this.actorBase = GameObject.Find("ActorBase");
        this.addActor();
        this.addActor();
        this.addActor();
        this.addActor();
        this.addActor();
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

        //UI移動処理
        iTween.MoveBy(this.UpperUI,
            iTween.Hash(
                "y", 10.0f,
                "easeType", iTween.EaseType.easeInOutSine,
                "time", 0.5f
            ));

        this.nowActor = next;
    }

    //新アクター追加
    public bool addActor() {
        if (this.actors.Count == this.maxActor) return false;

		Vector3 pos = new Vector3(this.actors.Count * 10.0f, 0, 0);
        GameObject ac = Instantiate(actor, pos, Quaternion.identity, this.actorBase.transform);
        if (ac == null) return false;
        ac.GetComponent<SpriteRenderer>().sprite = this.actorImages[this.actors.Count];
        this.actors.Add(ac);
        return true;
    }
}

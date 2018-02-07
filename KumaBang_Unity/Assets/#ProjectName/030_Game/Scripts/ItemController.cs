using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemController : MonoBehaviour {

    MasterItemData data;

    [SerializeField]GameObject itemSprite;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = this.transform.position;
        pos.z = pos.y - 4.0f;
        this.transform.position = pos;
	}

    //アイテムパラメータセット
    public void setParameter(int id, int x, int y, float delay = 0.0f) {
        //アイテム情報セット
        string name = "";
        switch (id) {
            case 1:
                name = "Apple";
                break;
            case 2:
                name = "Bag";
                break;
        }
        MasterItemData mst = (MasterItemData)Resources.Load("ItemMaster/Item"+name);
		this.data = Instantiate(mst);

        //スプライトセット
        SpriteRenderer sp = this.itemSprite.GetComponent<SpriteRenderer>();
        sp.sprite = this.data.itemSprite;

        //座標セット
        this.transform.position = new Vector3(x + 0.5f, -y + 0.25f + 10.0f);
        //落下演出
        this.transform.DOLocalMove(new Vector3(0, -10.0f, 0.0f), 1.0f)
            .SetEase(Ease.OutBounce)
            .SetRelative()
            .SetDelay(delay)
            .OnComplete(() => {
                this.itemSprite.transform.DOLocalMove(new Vector3(0, 0.1f, 0.0f), 1.0f)
                    .SetEase(Ease.InOutSine)
                    .SetRelative()
                    .SetLoops(-1, LoopType.Yoyo);
                this.addShadow();
            });
    }

    void addShadow() {
        //影追加
        GameObject shadow = Instantiate((GameObject)Resources.Load("Prefabs/Shadow"));
        Vector3 pos = this.transform.position;
        pos.y -= 0.5f;
        shadow.transform.position = pos;
        shadow.transform.localScale = new Vector3(2, 2, 1);
        shadow.transform.SetParent(this.transform);
    }

    void OnTriggerEnter2D(){
        Destroy(this.gameObject);
    }
}

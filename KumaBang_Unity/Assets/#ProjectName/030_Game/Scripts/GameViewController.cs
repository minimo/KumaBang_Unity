using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameViewController : MonoBehaviour {

    //ステージの大きさ
    int stageWidth = 5, stageHeight = 5;

    //スクリーン上オフセット
    int offsetX = -2, offsetY = -2;

    //ステージパネルマップ

    //パネルスプライト
    [SerializeField] GameObject sourcePanel;
    [SerializeField] List<Sprite> panelSprites;

    //選択中パネル
    public GameObject activePanel = null;

	// Use this for initialization
	void Start () {
		this.initStage();
	}
	
	// Update is called once per frame
	void Update () {
	}

    //ステージ初期化
    void initStage() {
        //パネルの準備
        for (int y = 0; y < this.stageHeight; y++) {
            for (int x = 0; x < this.stageWidth; x++) {
                float d = Random.Range(0.0f, 1.0f);
                this.enterPanel(Random.Range(1, 10), x, y, d);
            }
        }
    }

    //パネル投入
    void enterPanel(int index, int x, int y, float delay) {
        float px = x + this.offsetX, py = y + this.offsetY;
        GameObject p = Instantiate(this.sourcePanel);
        Vector3 pos = new Vector3(px, py + 10.0f);
        p.transform.position = pos;
        p.transform.parent = this.transform;

        PanelController pc = p.GetComponent<PanelController>();
        pc.setStagePosition(x, y);
        pc.setOffsetPosition(this.offsetX, this.offsetY);

        SpriteRenderer sp = p.GetComponent<SpriteRenderer>();
        sp.sprite = this.panelSprites[index];

        //落下演出
        p.transform.DOLocalMove(new Vector3(0, -10.0f, 0.0f), 1.0f)
                    .SetEase(Ease.OutBounce)
                    .SetDelay(delay)
                    .SetRelative();
    }

    void panelMove() {
        if (this.activePanel == null) return;
    }
}

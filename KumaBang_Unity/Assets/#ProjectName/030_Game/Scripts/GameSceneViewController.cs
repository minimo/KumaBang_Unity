﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//ゲーム中のビューとステージの管理を行う
public class GameSceneViewController : MonoBehaviour {

    //ステージの大きさ
    int stageWidth = 5, stageHeight = 5;

    //スクリーン上オフセット
    int offsetX = -2, offsetY = -2;

    //パネルスプライト
    [SerializeField] GameObject sourcePanel;
    [SerializeField] List<Sprite> panelSprites;

    //選択中パネル
    public GameObject activePanel = null;
    public PanelController activePanelController = null;

    //ステージデータ
    MapReader panelData;
    MapReader itemData;

    //現在ステージパネル
    GameObject [,] stageMap;

    //プレイヤーオブジェクト
    public GameObject player;

	// Use this for initialization
	void Start () {
		this.initStage();
	}
	
	// Update is called once per frame
	void Update () {
	}

    //ステージ開始処理
    void StartStage() {
    }

    //ステージ初期化
    void initStage() {
        //ステージデータ読み込み
        this.panelData = new MapReader("Map/Stage1_panel");
        this.itemData = new MapReader("Map/Stage1_item");

        //ステージパネルデータ配列
        this.stageMap = new GameObject[this.stageWidth, this.stageHeight];

        //パネルの準備
        for (int y = 0; y < this.stageHeight; y++) {
            for (int x = 0; x < this.stageWidth; x++) {
                float d = Random.Range(0.0f, 1.0f);
                int idx = this.panelData.mapData[x, y];
                GameObject p = this.enterPanel(idx, x, y, d);
                PanelController pc = p.GetComponent<PanelController>();
                //パネル設定
                switch (this.itemData.mapData[x, y]) {
                    case 0:
                        break;
                    default:
                        pc.isDisableShaffle = true;
                        break;
                }
                this.stageMap[x, y] = p;
            }
        }

        //パネルシャッフル
        for (int i = 0; i < 10; i++) {
            int x1 = Random.Range(0, 5), y1 = Random.Range(0, 5);
            int x2 = Random.Range(0, 5), y2 = Random.Range(0, 5);
            this.panelSwap(x1, y1, x2, y2);
        }
    }

    //パネル投入
    GameObject enterPanel(int index, int x, int y, float delay) {
        float px = x + this.offsetX, py = -(y + this.offsetY);
        GameObject panel = Instantiate(this.sourcePanel);
        Vector3 pos = new Vector3(px, py + 10.0f);
        panel.transform.position = pos;
        panel.transform.parent = this.transform;

        PanelController pc = panel.GetComponent<PanelController>();
        pc.index = index;
        pc.setStagePosition(x, y);
        pc.setOffsetPosition(this.offsetX, this.offsetY);
        pc.view = this;

        SpriteRenderer sp = panel.GetComponent<SpriteRenderer>();
        sp.sprite = this.panelSprites[index];

        //落下演出
        panel.transform.DOLocalMove(new Vector3(0, -10.0f, 0.0f), 1.0f)
                    .SetEase(Ease.OutBounce)
                    .SetDelay(delay)
                    .SetRelative();
        return panel;
    }

    void panelMove() {
        if (this.activePanel == null) return;
    }

    bool panelSwap(int x1, int y1, int x2, int y2) {
        PanelController pc1 =　this.stageMap[x1, y1].GetComponent<PanelController>();
        PanelController pc2 =　this.stageMap[x2, y2].GetComponent<PanelController>();

        //シャッフル可能パネルか判定
        if (pc1.isDisableShaffle || pc2.isDisableShaffle) return false;

        //ビュー上の座標交換
        Vector3 v1 = this.stageMap[x1, y1].transform.position;
        Vector3 v2 = this.stageMap[x2, y2].transform.position;
        this.stageMap[x1, y1].transform.position = v2;
        this.stageMap[x2, y2].transform.position = v1;

        //マップ上パネルの交換
        GameObject tmp = this.stageMap[x1, y1];
        this.stageMap[x1, y1] = this.stageMap[x2, y2];
        this.stageMap[x2, y2] = tmp;

        //パネルに設定した座標の交換
        pc1.stageX = x2;
        pc1.stageY = y2;
        pc2.stageX = x1;
        pc2.stageY = y1;
        return true;
    }
}

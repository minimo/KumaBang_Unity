using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//ゲーム中のビューとステージの管理を行う
public class GameSceneViewController : MonoBehaviour {

    //アプリケーションマネージャー
    ApplicationManagerController app;

    //シーンマネージャー
    GameSceneManager sceneManager;

    //パネルスプライト
    [SerializeField] GameObject sourcePanel;
    [SerializeField] List<Sprite> panelSprites;

    //選択中パネル
    public GameObject activePanel = null;
    public PanelController activePanelController = null;
    int activePanelBeforeX, activePanelBeforeY;

    //ステージデータ
    MapReader panelData;
    MapReader itemData;

    //現在ステージパネル
    public GameObject [,] stageMap;

    //ステージの大きさ
    int stageWidth = 5, stageHeight = 5;

    //スクリーン上オフセット
    int offsetX = 0, offsetY = 0;

    //スタート、ゴール位置
    int startX, startY, goalX, goalY;

    //プレイヤーオブジェクト
    public GameObject player = null;
    public PlayerController playerController = null;

    //フラグ管理
    public bool isGameStart = false;
    public bool isEvent = false;

    //経過フレーム
    int time = 0;

	// Use this for initialization
	void Start () {
        //アプリケーションマネージャー取得
        this.app = ApplicationManagerController.Instance;

		this.initStage();
	}
	
	// Update is called once per frame
	void Update () {

        //パネル移動処理
        if (this.activePanel) {
            int x = activePanelController.stageX;
            int y = activePanelController.stageY;
            int bx = this.activePanelBeforeX;
            int by = this.activePanelBeforeY;
            if (bx != -1) {
                if (x != bx || y != by) {
                    GameObject panel = this.stageMap[x, y];
                    if (panel && panel != this.activePanel) {
                        PanelController pc = panel.GetComponent<PanelController>();
                        if (pc.checkEnableMove()) {
                            pc.moveStagePosition(bx, by);
                            this.stageMap[x, y] = this.stageMap[bx, by];
                            this.stageMap[bx, by] = panel;
                        }
                    } else {
                        this.stageMap[x, y] = activePanel;
                        this.stageMap[bx, by] = null;
                    }
                }
            }
            this.activePanelBeforeX = x;
            this.activePanelBeforeY = y;
        } else {
            this.activePanelBeforeX = -1;
            this.activePanelBeforeY = -1;
        }

        if (this.isGameStart) {
            this.time++;
        }
	}

    //スコア加算
    public void addScore(int point, Vector3 pos) {
        this.sceneManager.addScore(point, pos);
    }

    //ステージ初期化
    void initStage() {
        GameObject currentScene = ApplicationManagerController.Instance.currentSceneManager;
        this.sceneManager = currentScene.GetComponent<GameSceneManager>();

        //ステージデータ読み込み
        string stageName = "Map/Stage" + this.app.playingStageNumber;
        this.panelData = new MapReader(stageName + "_panel");
        this.itemData = new MapReader(stageName + "_item");

        //ステージパネルデータ配列
        this.stageMap = new GameObject[this.stageWidth, this.stageHeight];

        //パネルの準備
        for (int y = 0; y < this.stageHeight; y++) {
            for (int x = 0; x < this.stageWidth; x++) {
                float delay = Random.Range(0.0f, 1.0f);
                int idx = this.panelData.mapData[x, y];
                GameObject p = this.enterPanel(idx, x, y);
                //落下演出
                Vector3 pos = p.transform.position;
                pos.y += 10.0f;
                p.transform.position = pos;
                p.transform.DOLocalMove(new Vector3(0, -10.0f, 0.0f), 1.0f)
                    .SetEase(Ease.OutBounce)
                    .SetDelay(delay)
                    .SetRelative();
                PanelController pc = p.GetComponent<PanelController>();
                //初期アイテムセットとパネル設定
                int id = this.itemData.mapData[x, y];
                switch (id) {
                    case 0:
                        break;
                    case 1:
                    case 2:
                        this.enterItem(id, x, y, 3.0f);
                        pc.isOnItem = true;
                        break;
                    //スタートパネル
                    case 8:
                        this.startX = x;
                        this.startY = y;
                        pc.isStart = true;
                        pc.isDisableMove = true;
                        break;
                    //ゴールパネル
                    case 9:
                        this.goalX = x;
                        this.goalY = y;
                        pc.isStart = true;
                        pc.isDisableMove = true;
                        break;
                }
                if (this.itemData.mapData[x, y] != 0) pc.isDisableShaffle = true;
                this.stageMap[x, y] = p;
            }
        }

        //パネルシャッフル
        for (int i = 0; i < 20; i++) {
            int x1 = Random.Range(0, 5), y1 = Random.Range(0, 5);
            int x2 = Random.Range(0, 5), y2 = Random.Range(0, 5);
            this.swapPanel(x1, y1, x2, y2);
        }
        this.transform.parent.gameObject.SendMessage("OnAlreadyStartView");

        //ゴールパネル標識
        GameObject canvasText = Instantiate((GameObject)Resources.Load("Prefabs/GoalText"));
        canvasText.transform.SetParent(this.transform);
        canvasText.transform.position = new Vector3(this.goalX + 0.5f, 5.0f, -5.0f);

        DOVirtual.DelayedCall(2.0f, () => {
            canvasText.transform.DOMove(new Vector3(this.goalX + 0.5f, -this.goalY + 0.5f , -5.0f), 2.0f)
                .SetEase(Ease.OutBounce);
            Destroy(canvasText, 7.0f);
        });

        //早送りボタン
        GameObject ffb = Instantiate((GameObject)Resources.Load("Prefabs/FFButton"));
        ffb.transform.SetParent(this.transform);
        ffb.transform.position = new Vector3(2.5f, -6.5f, -5.0f);
    }

    //パネル投入
    public GameObject enterPanel(int index, int x, int y) {
        float px = x + this.offsetX, py = -(y + this.offsetY);
        GameObject panel = Instantiate(this.sourcePanel);
        Vector3 pos = new Vector3(px + 0.5f, py - 0.5f);
        panel.transform.position = pos;
        panel.transform.SetParent(this.transform);

        PanelController pc = panel.GetComponent<PanelController>();
        pc.index = index;
        pc.setStagePosition(x, y);
        pc.setOffsetPosition(this.offsetX, this.offsetY);
        pc.view = this;

        SpriteRenderer sp = panel.GetComponent<SpriteRenderer>();
        sp.sprite = this.panelSprites[index];

        return panel;
    }

    //指定座標のパネル取得
    public PanelController getPanel(int x, int y) {
        if (x < 0 || y < 0) return null;
        if (x >= this.stageWidth || y >= this.stageHeight) return null;
        GameObject panel = this.stageMap[x, y];
        if (panel == null) return null;
        return panel.GetComponent<PanelController>();
    }

    //指定座標のパネルインデックス取得
    public int getPanelIndex(int x, int y) {
        if (x < 0 || y < 0) return -2;
        if (x >= this.stageWidth || y >= this.stageHeight) return -2;
        GameObject panel = this.stageMap[x, y];
        if (panel == null) return -1;
        return panel.GetComponent<PanelController>().index;
    }

    bool swapPanel(int x1, int y1, int x2, int y2) {
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

    //アイテム投入
    public void enterItem(int itemID, int x, int y, float delay = 0.0f) {
        GameObject item = Instantiate((GameObject)Resources.Load("Prefabs/Item"));
        item.transform.SetParent(this.transform);
        item.GetComponent<ItemController>().setParameter(itemID, x, y, delay);
    }

    //ステージ開始処理
    void OnStartStage() {
        this.isGameStart = true;

        //プレイヤー投入
        if (this.player) Destroy(this.player.gameObject);
        this.player = Instantiate((GameObject)Resources.Load("Prefabs/Player"));
        this.player.transform.SetParent(this.transform);
        this.player.transform.position = new Vector3(this.startX + 0.5f, -this.startY + 4.5f, -10.0f);
        this.playerController = this.player.GetComponent<PlayerController>();
        this.playerController.setActorNumber(this.app.selectedActor);
        this.playerController.setStartPosition(this.startX, this.startY);

        //Tweener
        var seq = DOTween.Sequence();

        //初回進行方向
        PanelController pc = this.stageMap[this.startX, this.startY].GetComponent<PanelController>();
        int idx = pc.index;
        switch (idx) {
            case 8:
                this.playerController.setDirection(1);
                break;
            case 9:
                this.playerController.setDirection(2);
                break;
            case 10:
                this.playerController.setDirection(3);
                break;
            case 11:
                this.playerController.setDirection(0);
                break;
        }
        this.playerController.footPanel = pc;

        //落下演出
        seq.Append(this.player.transform.DOLocalMove(new Vector3(0, -5.0f, 0.0f), 1.0f)
            .SetEase(Ease.OutBounce)
            .SetRelative()
            .OnComplete(() => {
                this.player.SendMessage("OnReadyStart");
            })
        );
    }

    void OnPlayerMiss() {
        this.isEvent = true;
        this.sceneManager.SendMessage("OnPlayerMiss");
    }

    void OnStageClear() {
        this.isEvent = true;

        //パーフェクトかチェック
        int livePanelCount = 0;
        for (int y = 0; y < this.stageHeight; y++) {
            for (int x = 0; x < this.stageWidth; x++) {
                if (this.stageMap[x, y]) livePanelCount++;
            }
        }
        bool isPerfect = false;
        if (livePanelCount == 1) isPerfect = true;
        this.sceneManager.SendMessage("OnStageClear", isPerfect);
    }
}

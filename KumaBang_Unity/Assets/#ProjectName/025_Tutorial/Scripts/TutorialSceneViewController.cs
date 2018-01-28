using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialSceneViewController : MonoBehaviour {

    //サウンドマネージャー
    SoundManagerController soundManager;

    List<GameObject> tutorial = new List<GameObject>();

    int maxPage = 2;
    int nowPage = 0;

    bool isFinish = false;

	// Use this for initialization
	void Start () {
		int numStage = 1;

        //チュートリアル準備
        for (int i = 0; i < this.maxPage; i ++) {
            string name = "Prefabs/Tutorial" + numStage;
            GameObject tutorial1 = Instantiate((GameObject)Resources.Load(name + "_" + (i + 1)));
            tutorial1.transform.SetParent(this.transform);
            tutorial1.transform.position = new Vector3(6.0f * i, 0.0f);
            this.tutorial.Add(tutorial1);
        }
        this.tutorial[0].transform.position = new Vector3(0, 10.0f);
        this.tutorial[0].GetComponent<TutorialPanelController>().screenIn();

        //サウンドマネージャー取得
        this.soundManager = SoundManagerController.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSwipe (Vector2 dir) {
        if (dir.x == 0.0f) return;

        float moveX = 6.0f;
        if (dir.x > 0) {
            if (this.nowPage == 0) return;
            this.nowPage--;
        } else {
            this.nowPage++;
            if (this.nowPage == this.maxPage) return;
            if (this.nowPage == this.maxPage - 1 && !this.isFinish) {
                this.isFinish = true;
                this.transform.parent.SendMessage("OnFinish");
            }
            moveX *= -1;
        }

        for (int i = 0; i < this.maxPage; i ++) {
            GameObject go = this.tutorial[i];
            go.transform.DOLocalMove(new Vector3(moveX, 0), 0.2f)
                .SetEase(Ease.InOutSine)
                .SetRelative();
        }
        this.soundManager.playSE("change");
    }
}

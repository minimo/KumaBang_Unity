using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManagerController : SingletonMonoBehaviour<ApplicationManagerController> {

    //最大ステージ番号
    public int maxStageNumber = 5;
    //現在ステージ番号
    public int playingStageNumber = 1;

    //選択アクター番号
    public int selectedActor = 0;

    //エンドレスモードフラグ
    public bool isEndless = false;

    public GameObject currentSceneManager;

	void Awake () {
	}
}

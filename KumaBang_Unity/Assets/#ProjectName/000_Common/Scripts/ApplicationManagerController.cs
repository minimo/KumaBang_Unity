using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManagerController : SingletonMonoBehaviour<ApplicationManagerController> {

    //現在ステージ番号
    public int playingStageNumber = 1;

    //選択アクター番号
    public int selectedActor = 0;

	void Awake () {
	}
}

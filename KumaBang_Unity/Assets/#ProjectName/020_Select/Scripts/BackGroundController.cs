using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour {

    [SerializeField] GameObject [] backgrounds;

    //現在のバックグラウンド
    public GameObject nowBackGround;

    //現在のバックグラウンド番号
    public int nowBackGroundNumber;

	// Use this for initialization
	void Start () {
        //最初はフィールドを表示
        this.setBackGround(0);
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void setBackGround(int num) {
        if (num < 0 || this.backgrounds.Length - 1 < num) return;
        Destroy(this.nowBackGround);
        Vector3 pos = new Vector3(0, 0, 0);
        this.nowBackGround = Instantiate(this.backgrounds[num], pos, Quaternion.identity, this.transform);
        this.nowBackGroundNumber = num;
    }

    //切り替えアニメーション再生
    void switchAnimation() {
    }
}

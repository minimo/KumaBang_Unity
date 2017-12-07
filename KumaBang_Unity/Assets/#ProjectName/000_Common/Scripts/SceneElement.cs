using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneElement : MonoBehaviour {

    SceneElement parentElement = null;

    //デリゲート関数
    public delegate void MessageFunction();
    //関数保存配列
    Dictionary<string, MessageFunction> functions;

	virtual protected void Start () {
        //関数一覧初期化
        this.functions = new Dictionary<string, MessageFunction>();
	}

    //メッセージに紐つく関数を登録
    public void on(string msg, MessageFunction func) {
        //既に登録済みの場合は削除
        if (this.functions.ContainsKey(msg)) this.functions.Remove(msg);
        this.functions.Add(msg, func);
    }

    //メッセージに紐つく関数を削除
    public void off(string msg) {
        if (this.functions.ContainsKey(msg)) this.functions.Remove(msg);
    }

    //メッセージ発火
    public void flare(string msg) {
        if (!this.functions.ContainsKey(msg)) return;
        this.functions[msg]();
    }

    public void addChild(SceneElement obj) {
    }

    public void setParentElement(SceneElement obj) {
        this.parentElement = obj;
    }
}

using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// スワイプ方向を取得してコールバックする
/// 2017/7/5 Fantom (Unity 5.6.1p3)
/// http://fantom1x.blog130.fc2.com/blog-entry-250.html
///（使い方）
///・適当な GameObject にアタッチして、インスペクタから OnSwipe（Vector2 を１つ引数にとる）にコールバックする関数を登録すれば使用可。
///・またはプロパティ SwipeInput.Direction をフレーム毎監視しても良い（こちらの場合は無し（Vector2.zero）も含まれる）。
///（仕様説明）
///・タッチの移動量（エディタやスマホ以外の場合はマウス）で判定する。画面幅の Valid Width（％）以上移動したときスワイプとして認識する。
///・ただし、移動が制限時間（Timeout）を超えた時は無視する。
///・複数の指では認識できない（※２つ以上の指の場合はピンチの可能性もあるため無効とする）。
///・タッチデバイスを UNITY_ANDROID, UNITY_IOS としているので、他のデバイスも加えたい場合は #if の条件文にデバイスを追加する（Input.touchCount が取得できるもののみ）。
/// </summary>

public class SwipeInput : MonoBehaviour
{
    //設定値
    public bool widthReference = true;  //画面幅（Screen.width）サイズを比率の基準にする（false=高さ（Screen.height）を基準）
    public float validWidth = 0.25f;    //スワイプとして認識する移動量の画面比[画面幅に対する比率]（0.0～1.0：1.0で端から端まで。この値より長い移動量でスワイプとして認識する）
    public float timeout = 0.5f;        //スワイプとして認識する時間（これより短い時間でスワイプとして認識する）

    //Local Values
    Vector2 startPos;                   //スワイプ開始座標
    Vector2 endPos;                     //スワイプ終了座標
    float limitTime;                    //スワイプ時間制限（この時刻を超えたらスワイプとして認識しない）
    bool pressing;                      //押下中フラグ（単一指のみの取得にするため）

    Vector2 swipeDir = Vector2.zero;    //取得したスワイプ方向（フレーム毎判定用）[zeroがなしで、left, right, up, downが方向]

    //スワイプ方向取得プロパティ（フレーム毎取得用）
    public Vector2 Direction {
        get { return swipeDir; }
    }

    //スワイプイベントコールバック（インスペクタ用）
    [Serializable]
    public class SwipeHandler : UnityEvent<Vector2> {
    }

    public SwipeHandler OnSwipe;        //引数の Vector2 でスワイプ方向を返す


    //アクティブになったら、初期化する（アプリの中断などしたときはリセットする）
    void OnEnable()
    {
        pressing = false;
    }

    // Update is called once per frame
    void Update()
    {
        swipeDir = Vector2.zero;    //フレーム毎にリセット

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)   //タッチで取得したいプラットフォームのみ
        if (Input.touchCount == 1)  //複数の指は不可とする（※２つ以上の指の場合はピンチの可能性もあるため）
#endif
        {
            if (!pressing && Input.GetMouseButtonDown(0))   //押したとき（左クリック/タッチが取得できる）
            {
                pressing = true;
                startPos = Input.mousePosition;
                limitTime = Time.time + timeout;
            }
            else if (pressing && Input.GetMouseButtonUp(0))  //既に押されているときのみ（※この関数は２つ以上タッチの場合、どの指か判別できないので注意）
            {
                pressing = false;

                if (Time.time < limitTime)  //時間制限前なら認識
                {
                    endPos = Input.mousePosition;

                    Vector2 dist = endPos - startPos;
                    float dx = Mathf.Abs(dist.x);
                    float dy = Mathf.Abs(dist.y);
                    float requiredPx = widthReference ? Screen.width * validWidth : Screen.height * validWidth;

                    if (dy < dx)    //横方向として認識
                    {
                        if (requiredPx < dx)   //長さを超えていたら認識
                            swipeDir = Mathf.Sign(dist.x) < 0 ? Vector2.left : Vector2.right;
                    }
                    else    //縦方向として認識
                    {
                        if (requiredPx < dy)   //長さを超えていたら認識
                            swipeDir = Mathf.Sign(dist.y) < 0 ? Vector2.down : Vector2.up;
                    }

                    //コールバックイベント
                    if (swipeDir != Vector2.zero)
                    {
                        if (OnSwipe != null)
                            OnSwipe.Invoke(swipeDir);   //UnityEvent
                    }
                }
            }
        }
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)   //タッチで取得したいプラットフォームのみ
        else  //タッチが１つでないときは無効にする（※２つ以上の指の場合はピンチの可能性もあるため）
        {
            pressing = false;
        }
#endif
    }
}
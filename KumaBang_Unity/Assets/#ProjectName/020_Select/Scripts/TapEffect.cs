using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEffect : MonoBehaviour {

    public bool isActive = true;

    ParticleSystem tapEffect;              // タップエフェクト
    Camera _camera;                        // カメラの座標

	// Use this for initialization
	void Start () {
        this._camera = GameObject.Find("EffectCamera").GetComponent<Camera>();

        //パーティクルシステム
        GameObject go = Instantiate((GameObject)Resources.Load("Prefabs/TapParticle"));
        go.transform.parent = this.transform;
        this.tapEffect = go.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!this.isActive) return;

        //uGUIと重なっていたらパーティクルは出さない
        #if UNITY_EDITOR
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
        #else 
//            if (UnityEngine.EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
        #endif

        if(Input.GetMouseButtonDown(0)) {
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
            tapEffect.transform.position = pos;
            tapEffect.Emit(50);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
    private static T _instance;

    public static bool isInitialized() {
        return (_instance != null);
    }

    public static T Instance {
        get {
            if (_instance == null) {
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null) {
                    GameObject obj = (GameObject)Resources.Load("Prefabs/SoundManager");
                    GameObject instObj = GameObject.Instantiate(obj);
//                    Debug.LogError (typeof(T) + "is nothing");
                }
            }
            return _instance;
        }
    }
}
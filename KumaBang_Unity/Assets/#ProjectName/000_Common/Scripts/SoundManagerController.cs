using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerController : SingletonMonoBehaviour<SoundManagerController> {

    //BGM用
    GameObject bgmPlayerObject;
    AudioSource audioSourceBGM;

    //SoundEffect用
    GameObject soundPlayerObject;
    AudioSource audioSource;

    Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();

    void Awake() {
        if(this != Instance) {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        //BGM Player
        this.bgmPlayerObject = new GameObject("BGMPlayer");
        this.bgmPlayerObject.transform.parent = this.transform;
        this.audioSourceBGM = this.bgmPlayerObject.AddComponent<AudioSource>();
        this.audioSourceBGM.loop = true;

        //Sound Effect Player
        this.soundPlayerObject = new GameObject("SoundPlayer");
        this.soundPlayerObject.transform.parent = this.transform;
        this.audioSource = this.soundPlayerObject.AddComponent<AudioSource>();
    }

    public bool playSE(string name) {
        if (!this.sounds.ContainsKey(name)) return false;

        //Audio clipの取得
        AudioClip clip = this.sounds[name];
        audioSource.PlayOneShot(clip);
        return true;
    }

    public bool playBGM(string name) {
        if (!this.sounds.ContainsKey(name)) return false;

        //Audio clipの取得
        AudioClip clip = this.sounds[name];
        audioSourceBGM.Stop();
        audioSourceBGM.clip = clip;
        audioSourceBGM.Play();
        return true;
    }

    //音声追加
    public bool addSound(string name, string path) {
        GameObject source = (GameObject)Resources.Load(path);
        AudioClip clip = source.GetComponent<AudioSource>().clip;
        this.sounds.Add(name, clip);
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManagerController : SingletonMonoBehaviour<SoundManagerController> {

    //AudioClip保存
    Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();

    //再生用GameObject & AudioSource
    List<GameObject> playerObject = new List<GameObject>();
    List<AudioSource> audioSource = new List<AudioSource>();

    float masterVolumeBGM = 1.0f;
    float masterVolumeSE = 1.0f;

    int playingNumber = 0; //0:演奏していない 1:Primary 2:Secondary

    public bool isPlayingBGM = false;

    void Awake() {
        if(this != Instance) {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        //Sound Effect
        this.playerObject.Add(new GameObject("SoundPlayer"));
        this.playerObject[0].transform.parent = this.transform;
        this.audioSource.Add(this.playerObject[0].AddComponent<AudioSource>());

        //Primary BGM
        this.playerObject.Add(new GameObject("BGMPlayer"));
        this.playerObject[1].transform.parent = this.transform;
        this.audioSource.Add(this.playerObject[1].AddComponent<AudioSource>());
        this.audioSource[1].loop = true;

        //Secondary BGM
        this.playerObject.Add(new GameObject("BGMPlayer"));
        this.playerObject[2].transform.parent = this.transform;
        this.audioSource.Add(this.playerObject[2].AddComponent<AudioSource>());
        this.audioSource[2].loop = true;
    }

    public bool playSE(string name) {
        if (!this.sounds.ContainsKey(name)) return false;

        //Audio clipの取得
        AudioClip clip = this.sounds[name];
        this.audioSource[0].PlayOneShot(clip);
        return true;
    }

    public bool playBGM(string name, float fadeTime = 0.0f) {
        if (!this.sounds.ContainsKey(name)) return false;

        if (this.playingNumber != 0) {
            this.audioSource[this.playingNumber].Stop();
        }

        int next = 0;
        switch (this.playingNumber) {
            case 0:
            case 2:
                next = 1;
                break;
            case 1:
                next = 2;
                break;
        }

        this.audioSource[next].clip = this.sounds[name];
        this.audioSource[next].Play();
        this.isPlayingBGM = true;
        this.playingNumber = next;
        return true;
    }

    public bool stopBGM(float fadeTime = 0.0f, int num = 0) {
        if (!this.isPlayingBGM) return false;
 
        if (fadeTime == 0.0f) {
            this.audioSource[this.playingNumber].Stop();
            this.isPlayingBGM = false;
            this.playingNumber = 0;
        } else {
            DOTween.To(
                () => this.audioSource[this.playingNumber].volume,
                val =>  this.audioSource[this.playingNumber].volume = val,
                0.0f,
                fadeTime
            ).OnComplete(() => {
                audioSource[this.playingNumber].Stop();
                this.isPlayingBGM = false;
                this.playingNumber = 0;
            });
        }
        return true;
    }

    public void fadeIn(float time, int num) {
        if (!this.isPlayingBGM) return;
        if (num != 0) num = this.playingNumber;

        float endVolume = 1.0f;
        DOTween.To(
            () => this.audioSource[num].volume,          // 何を対象にするのか
            val =>  this.audioSource[num].volume = val,   // 値の更新
            endVolume,                          // 最終的な値
            time                    // アニメーション時間
        );
    }

    public void fadeOut(float time, int num) {
        if (!this.isPlayingBGM) return;
        if (num != 0) num = this.playingNumber;

        DOTween.To(
            () => this.audioSource[num].volume,          // 何を対象にするのか
            val =>  this.audioSource[num].volume = val,   // 値の更新
            0.0f,                          // 最終的な値
            time                    // アニメーション時間
        );
    }

    public void setVolumeBGM(float vol) {
        if (vol < 0.0f) vol = 0.0f;
        if (vol > 1.0f) vol = 1.0f;
        this.masterVolumeBGM = vol;
    }

    public void setVolumeSE(float vol) {
        if (vol < 0.0f) vol = 0.0f;
        if (vol > 1.0f) vol = 1.0f;
        this.masterVolumeSE = vol;
    }

    //音声追加
    public bool addSound(string name, string path) {
        GameObject source = (GameObject)Resources.Load(path);
        AudioClip clip = source.GetComponent<AudioSource>().clip;
        //キーが存在する場合は上書きする
        if (this.sounds.ContainsKey(name)) {
            this.sounds[name] = clip;
        } else {
            this.sounds.Add(name, clip);
        }
        return true;
    }
}

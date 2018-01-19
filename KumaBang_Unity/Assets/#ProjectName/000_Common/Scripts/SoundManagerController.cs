using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManagerController : SingletonMonoBehaviour<SoundManagerController> {

    //AudioClip保存
    Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();

    //再生用GameObject & AudioSource
    GameObject SEPlayer;
    AudioSource SEAudioSource;
    GameObject BGMPlayer;
    AudioSource BGMAudioSource;

    float masterVolumeBGM = 1.0f;
    float masterVolumeSE = 1.0f;

    public bool isPlayingBGM = false;

    void Awake() {
        //Sound Effect
        this.SEPlayer = new GameObject("SoundEffect");
        this.SEPlayer.transform.parent = this.transform;
        this.SEAudioSource = this.SEPlayer.AddComponent<AudioSource>();
    }

    public bool playSE(string name) {
        if (!this.sounds.ContainsKey(name)) return false;

        //Audio clipの取得
        AudioClip clip = this.sounds[name];
        this.SEAudioSource.PlayOneShot(clip);
        return true;
    }

    public bool playBGM(string name, float fadeTime = 0.0f, bool loop = true) {
        if (!this.sounds.ContainsKey(name)) return false;

        //プレイヤーを生成
        GameObject player = new GameObject("BGMPlayer");
        player.transform.parent = this.transform;
        AudioSource source = player.AddComponent<AudioSource>();
        source.loop = loop;

        source.clip = this.sounds[name];
        source.Play();
        if (fadeTime > 0.0f) {
            source.volume = 0.0f;
            this.fadeIn(player, fadeTime);

            if (this.BGMPlayer) {
                this.fadeOut(this.BGMPlayer, fadeTime, true);
            }
        } else {
            Destroy(this.BGMPlayer);
        }
        this.BGMPlayer = player;
        this.BGMAudioSource = source;

        this.isPlayingBGM = true;
        return true;
    }

    public bool stopBGM(float fadeTime = 0.0f, bool isDestroy = false) {
        if (!this.isPlayingBGM) return false;
 
        if (fadeTime == 0.0f) {
            this.BGMAudioSource.Stop();
            this.isPlayingBGM = false;
        } else {
            DOTween.To(
                () => this.BGMAudioSource.volume,
                val =>  this.BGMAudioSource.volume = val,
                0.0f,
                fadeTime
            ).OnComplete(() => {
                this.BGMAudioSource.Stop();
                this.isPlayingBGM = false;
            });
        }
        return true;
    }

    public void fadeIn(GameObject player, float time) {
        AudioSource source = player.GetComponent<AudioSource>();
        float endVolume = 1.0f;
        DOTween.To(
            () => source.volume,          // 何を対象にするのか
            val =>  source.volume = val,   // 値の更新
            endVolume,                          // 最終的な値
            time                    // アニメーション時間
        );
    }

    public void fadeOut(GameObject player, float time, bool isDestroy = false) {
        if (!this.isPlayingBGM) return;

        AudioSource source = player.GetComponent<AudioSource>();
        DOTween.To(
            () => source.volume,          // 何を対象にするのか
            val => source.volume = val,   // 値の更新
            0.0f,                          // 最終的な値
            time                    // アニメーション時間
        );
        if (isDestroy) Destroy(player, time);
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

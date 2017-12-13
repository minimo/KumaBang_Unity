using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerController : MonoBehaviour {
    GameObject soundPlayerObject = null;
    GameObject bgmPlayerObject = null;

    AudioSource audioSource;
    AudioSource audioSourceBGM;

    [SerializeField]GameObject soundArray;
    SoundArrayController soundArrayController;

    void Start() {
        this.soundArrayController = this.soundArray.GetComponent<SoundArrayController>();

        //AudioSource格納用
        this.soundPlayerObject = new GameObject("SoundPlayer");
        this.soundPlayerObject.transform.parent = this.transform;
        this.audioSource = this.soundPlayerObject.AddComponent<AudioSource>();

        //bgmPlayer
        this.bgmPlayerObject = new GameObject("BGMPlayer");
        this.bgmPlayerObject.transform.parent = this.transform;
        this.audioSourceBGM = this.bgmPlayerObject.AddComponent<AudioSource>();
        this.audioSourceBGM.loop = true;
    }

    public bool playSE(string name) {
        AudioClip clip = this.soundArrayController.getSound(name);
        if (clip == null) return false;
        audioSource.PlayOneShot(clip);
        return true;
    }

    public bool playBGM(string name) {
        AudioClip clip = this.soundArrayController.getSound(name);
        if (clip == null) return false;
        audioSourceBGM.Stop();
        audioSourceBGM.clip = clip;
        audioSourceBGM.Play();
        return true;
    }
}

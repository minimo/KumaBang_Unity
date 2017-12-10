using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerController : MonoBehaviour {
    GameObject soundPlayerObject = null;
    AudioSource audioSource;

    [SerializeField]GameObject soundArray;
    SoundArrayController soundArrayController;

    void Start() {
        this.soundArrayController = this.soundArray.GetComponent<SoundArrayController>();

        //AudioSource格納用
        this.soundPlayerObject = new GameObject("SoundPlayer");
        this.soundPlayerObject.transform.parent = this.transform;
        this.audioSource = this.soundPlayerObject.AddComponent<AudioSource>();
    }

    public bool playSE(string name) {
        AudioClip clip = this.soundArrayController.getSound(name);
        if (clip == null) return false;
        audioSource.PlayOneShot(clip);
        return true;
    }
}

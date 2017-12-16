﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundArrayController : MonoBehaviour {
    string [] names;
    AudioClip [] sounds;

    Dictionary<string, AudioClip> array = new Dictionary<string, AudioClip>();

    void Start() {
    }

    void loadSceneSound(string sceneName) {
        switch (sceneName) {
            case "splash":
                this.loadSceneSound_splash();
                break;
            case "title":
                break;
            case "select":
                break;
        }
    }

    void loadSceneSound_splash() {
    }

    public void addSound(string name, AudioClip source) {
        array.Add(name, source);
    }

    public AudioClip getSound(string name) {
        if (this.array.ContainsKey(name) == false) return null;
        return this.array[name];
    }
}
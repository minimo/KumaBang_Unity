using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundArrayController : MonoBehaviour {
    public string [] names;
    public AudioClip [] sounds;

    Dictionary<string, AudioClip> array = new Dictionary<string, AudioClip>();

    void Start() {
        //保存配列紐つけ処理
        for (int i = 0; i < this.sounds.Length; i++) {
            string name = null;
            if (i < this.names.Length) 
                name = this.names[i];
            else
                name = "sound_"+i.ToString();
            this.addSound(name, this.sounds[i]);
        }
    }

    public void addSound(string name, AudioClip source) {
        array.Add(name, source);
    }

    public AudioClip getSound(string name) {
        if (this.array.ContainsKey(name) == false) return null;
        return this.array[name];
    }
}

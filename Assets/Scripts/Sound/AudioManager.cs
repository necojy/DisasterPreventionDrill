using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//使用Array需呼叫


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] backgroundSounds;
    public AudioSource BackgroundSource;


    public void Awake()
    {
        instance = this;

        foreach(Sound backgroundSound in backgroundSounds)
        {
            if(!backgroundSound.source) backgroundSound.source = gameObject.AddComponent<AudioSource>();

            backgroundSound.source.clip = backgroundSound.clip;
            backgroundSound.source.playOnAwake = backgroundSound.playOnAwake;
            backgroundSound.source.volume = backgroundSound.volume;
            backgroundSound.source.pitch = backgroundSound.pitch;
            backgroundSound.source.loop = backgroundSound.loop;
        }
    }



    
    public void PlayBackground(string name)
    {
        //使用一個參數 x 來遍歷 ItemSound 陣列中的元素，並檢查每個元素的 name 屬性是否等於 name 參數。
        Sound s = Array.Find(backgroundSounds, backgroundSound => backgroundSound.name == name);

        BackgroundSource.Stop();
        BackgroundSource.loop = true;

        if (s == null)
        {
            Debug.Log(name + "Sound not found");
        }
        else if (s.name == "Factory Map Music")
        {
            BackgroundSource.clip = s.clip;
            BackgroundSource.Play();
        }
    }

    

}

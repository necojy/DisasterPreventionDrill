using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//使用Array需呼叫


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] backgroundSounds, itemSounds, elevatorSounds;
    public AudioSource BackgroundSource, itemSources, elevatorSources;


    public void Awake()
    {
        instance = this;

        foreach (Sound backgroundSound in backgroundSounds)
        {
            if (!backgroundSound.source) backgroundSound.source = gameObject.AddComponent<AudioSource>();

            backgroundSound.source.clip = backgroundSound.clip;
            backgroundSound.source.playOnAwake = backgroundSound.playOnAwake;
            backgroundSound.source.volume = backgroundSound.volume;
            backgroundSound.source.pitch = backgroundSound.pitch;
            backgroundSound.source.loop = backgroundSound.loop;
        }

        // foreach (Sound    in itemSounds)
        // {
        //     if (!itemSound.source) itemSound.source = gameObject.AddComponent<AudioSource>();

        //     itemSound.source.clip = itemSound.clip;
        //     itemSound.source.playOnAwake = itemSound.playOnAwake;
        //     itemSound.source.volume = itemSound.volume;
        //     itemSound.source.pitch = itemSound.pitch;
        //     itemSound.source.loop = itemSound.loop;
        // }
    }




    public void PlayBackground(string name)
    {
        //使用一個參數 x 來遍歷 ItemSound 陣列中的元素，並檢查每個元素的 name 屬性是否等於 name 參數。
        Sound s = Array.Find(backgroundSounds, backgroundSound => backgroundSound.name == name);

        // BackgroundSource.Stop();
        // BackgroundSource.loop = true;

        if (s == null)
        {
            Debug.Log(name + "Sound not found");
        }
        else if (s.name == "Earthquake")
        {
            BackgroundSource.PlayOneShot(s.clip);
        }
        else if (s.name == "Corridor")
        {
            BackgroundSource.clip = s.clip;
            BackgroundSource.loop = true; // 設置循環播放
            BackgroundSource.Play();
        }
        else if (s.name == "End")
        {
            BackgroundSource.clip = s.clip;
            BackgroundSource.loop = true; // 設置循環播放
            BackgroundSource.Play();
        }

    }

    public void PlayItemSound(string name)
    {
        //使用一個參數 x 來遍歷 ItemSound 陣列中的元素，並檢查每個元素的 name 屬性是否等於 name 參數。
        Sound s = Array.Find(itemSounds, itemSound => itemSound.name == name);

        // itemSources.Stop();

        if (s == null)
        {
            Debug.Log(name + "Sound not found");
        }
        else if (s.name == "Open_lightet")
        {
            itemSources.PlayOneShot(s.clip);
        }
        else if (s.name == "Open_cellPhone")
        {
            itemSources.PlayOneShot(s.clip);
        }
        else if (s.name == "Breaking_glass")
        {
            itemSources.PlayOneShot(s.clip);
        }
        else if (s.name == "Breaking_glass_2")
        {
            itemSources.PlayOneShot(s.clip);
        }
        else if (s.name == "Open_door")
        {
            itemSources.PlayOneShot(s.clip);
        }
        else if (s.name == "tile")
        {
            itemSources.PlayOneShot(s.clip);
        }
        else if (s.name == "tile2")
        {
            itemSources.PlayOneShot(s.clip);
        }
        else if (s.name == "rock slide")
        {
            itemSources.PlayOneShot(s.clip);
        }
        else if (s.name == "alert_Sound")
        {
            itemSources.PlayOneShot(s.clip);
        }
        else if (s.name == "safeDoor")
        {
            itemSources.PlayOneShot(s.clip);
        }
        else if (s.name == "water boiling")
        {
            itemSources.PlayOneShot(s.clip);
        }
        else if (s.name == "glass01")
        {
            itemSources.PlayOneShot(s.clip);
        }
        else if (s.name == "glass02")
        {
            itemSources.PlayOneShot(s.clip);
        }
        else if (s.name == "aircondition_before")
        {
            itemSources.PlayOneShot(s.clip);
        }
        else if (s.name == "aircondition_after")
        {
            itemSources.PlayOneShot(s.clip);
        }
    }

    public void PlayElevatorSound(string name)
    {
        Sound s = Array.Find(elevatorSounds, elevatorSound => elevatorSound.name == name);

        if (s == null)
        {
            Debug.Log(name + "Sound not found");
        }
        else if (s.name == "Open_Close_Door")
        {
            elevatorSources.PlayOneShot(s.clip);
        }
        else if (s.name == "Elevator_Shaking")
        {
            elevatorSources.PlayOneShot(s.clip);
        }


    }



    public void PauseSound(string name)
    {
        if (name == "BackgroundSource")
        {
            if (BackgroundSource.isPlaying)
            {
                BackgroundSource.Pause();
            }
        }
        if (name == "elevatorSources")
        {
            if (elevatorSources.isPlaying)
            {
                elevatorSources.Pause();
            }
        }

    }

    public void ResumeSound(string name)
    {
        if (name == "BackgroundSource")
        {
            if (BackgroundSource != null && BackgroundSource.clip != null)
            {
                BackgroundSource.UnPause();
            }
        }
    }

    public void StopAllSounds()
    {
        foreach (Sound backgroundSound in backgroundSounds)
        {
            if (backgroundSound.source && backgroundSound.source.isPlaying)
            {
                backgroundSound.source.Stop();
            }
        }

        foreach (Sound itemSound in itemSounds)
        {
            if (itemSound.source && itemSound.source.isPlaying)
            {
                itemSound.source.Stop();
            }
        }

        if (BackgroundSource.isPlaying)
        {
            BackgroundSource.Stop();
        }
        if (itemSources.isPlaying)
        {
            itemSources.Stop();
        }
    }

}

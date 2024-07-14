using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackGroundmusicControl : MonoBehaviour
{
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Corridor")
        {
            AudioManager.instance.PlayBackground("Corridor");
        }
        else
        {
            AudioManager.instance.StopAllSounds();
        }
    }
}

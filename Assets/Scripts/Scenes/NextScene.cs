using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public String nextSceneName;
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameManager.Instance.LoadNextScene(nextSceneName);
        }
    }
}

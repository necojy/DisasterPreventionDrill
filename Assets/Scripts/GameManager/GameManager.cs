using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject("GameManager").AddComponent<GameManager>();
                    DontDestroyOnLoad(instance.gameObject);
                }
            }
            return instance;
        }
    }

    public List<Slot> slots;

     public void LoadNextScene(string scene)
    {
        Debug.Log(" Load ");
        foreach (Slot slot in slots)
        {
            slot.RecordChanges();
        }
        SceneManager.LoadScene(scene);
    }

    public void ReloadScene(string scene)
    {
        Debug.Log(" ReLoad ");
        SceneManager.LoadScene(scene);
    }

    public void GetSlots(List<Slot> inventorySlots)
    {
        slots = inventorySlots;
    }
    
}

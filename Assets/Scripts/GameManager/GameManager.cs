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

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RebuildItemsInSlots();
    }

    public GameObject inventoryObject;
    void RebuildItemsInSlots()
    {  
        Debug.Log("RebuildItemsInSlots");
        // 啟用 Inventory，以便設置物品的父子關係
        if (inventoryObject != null)
        {
            inventoryObject.SetActive(true);
        }

        Slot[] slots = FindObjectsOfType<Slot>(); // 找到新場景中所有的插槽

        foreach (Slot slot in slots)
        {
            GameObject item = SlotItemManager.Instance.GetItemInSlot(slot);
            Debug.Log(slot+":"+item);
            if (item != null)
            {
                // 將物品設置為插槽的子物件，並根據保存的位置和旋轉設置其在插槽中的狀態
                Rigidbody rb = item.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.useGravity = false;
                }
                
                item.transform.SetParent(slot.transform, false);
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;

                Item itemComponent = item.GetComponent<Item>();
                if (itemComponent != null)
                {
                    item.transform.localEulerAngles = itemComponent.slotRotation;
                    itemComponent.inSlot = true;
                    itemComponent.currentSlot = slot;
                }

                // 更新插槽的 ItemInSlot 屬性
                slot.ItemInSlot = item;
                slot.slotImage.color = Color.clear;
            }
        }


    }
}

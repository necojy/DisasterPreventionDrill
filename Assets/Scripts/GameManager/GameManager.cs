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

    void Start()
    {
        
    }

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


    /* void RebuildItemsInSlots()
    {
        Slot[] slots = FindObjectsOfType<Slot>(); // 找到新場景中所有的插槽

        foreach (Slot slot in slots)
        {
            GameObject item = SlotItemManager.Instance.GetItemInSlot(slot);
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
 */
        // 啟用 Inventory 物件以便設置物品的父子關係
        
    
}

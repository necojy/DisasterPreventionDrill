using UnityEngine;
using System.Collections.Generic;

public class SlotItemManager : MonoBehaviour
{
    // 單例模式
    private static SlotItemManager instance;
    public static SlotItemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SlotItemManager>();
                if (instance == null)
                {
                    instance = new GameObject("SlotItemManager").AddComponent<SlotItemManager>();
                    DontDestroyOnLoad(instance.gameObject);
                }
            }
            return instance;
        }
    }

    // 保存物品及其所在插槽
    private Dictionary<GameObject, Slot> itemSlotMap = new Dictionary<GameObject, Slot>();

    // 註冊物品及其所在插槽
    public void RegisterItem(GameObject item, Slot slot)
    {
        if (!itemSlotMap.ContainsKey(item))
        {
            itemSlotMap[item] = slot;
            Debug.Log(slot+":"+item);
        }
    }

    // 取消註冊物品及其所在插槽
    public void UnregisterItem(Slot slot)
    {
        GameObject item = slot.ItemInSlot;
        if (item != null && itemSlotMap.ContainsKey(item) && itemSlotMap[item] == slot)
        {
            itemSlotMap.Remove(item);
        }
    }

    // 根據插槽獲取物品
    public GameObject GetItemInSlot(Slot slot)
    {
        return slot.ItemInSlot;
    }

}

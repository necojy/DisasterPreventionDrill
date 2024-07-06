using UnityEngine;
using System.Collections.Generic;

public class SlotItemManager : MonoBehaviour
{
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
   public Dictionary<string, string> itemSlotMap = new Dictionary<string, string>();

    // 註冊物品及其所在插槽
    public void RegisterItem(Slot slot, GameObject item)
    {
        itemSlotMap[slot.name] = item.name;
        Debug.Log(slot + ":" + item);
        
    }

    // 取消註冊物品及其所在插槽
    public void UnregisterItem(Slot slot)
    {
        if (itemSlotMap.ContainsKey(slot.name))
        {
            Debug.Log("Remove "+ slot + ":" + itemSlotMap[slot.name]);
            itemSlotMap.Remove(slot.name);    
        }
    }
}

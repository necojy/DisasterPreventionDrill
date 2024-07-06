using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Slot : MonoBehaviour
{
    public GameObject inventory;
    public GameObject ItemInSlot;
    public Image slotImage;
    public GameObject item;
    Color originalColor;

    public InputActionReference actionReference;
    public InputAction action;

    public bool ItemInArea;
    public bool HandInArea;

    public SlotItemManager slotItemManager;
    public GameObject[] ITEMS;

    void Awake()
    {
        slotItemManager = FindObjectOfType<SlotItemManager>();
    }
    void Start()
    {
        slotImage = GetComponentInChildren<Image>();
        originalColor = slotImage.color;
        action = actionReference.action;
        action.canceled += OnActionCanceled;
        action.performed += OnActionPerformed;
        ItemInArea = false;
        HandInArea = false;

        //載入場景時讀取slot存取內容
        Slot slot = this;
        if(slotItemManager != null){
            if(slotItemManager.itemSlotMap.ContainsKey(slot.name))
            {
                // 實例化該物件
                foreach (GameObject item in ITEMS)
                {
                    if(slotItemManager.itemSlotMap[slot.name] == item.name)
                    {
                        GameObject instantiatedItem = Instantiate(item);
                        instantiatedItem.name = item.name;
                        InsertItem(instantiatedItem);
                        break;
                    }
                }
            }
        }
        
    }


    private void OnTriggerStay(Collider coll)
    {
        if (ItemInSlot == null && IsItem(coll.gameObject))
        {
            item = coll.gameObject;
            ItemInArea = true;
        }

        if (coll.CompareTag("Left Hand") || coll.CompareTag("Right Hand"))
        {
            HandInArea = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (IsItem(coll.gameObject))
        {
            item = null;
            ItemInArea = false;
        }
        if (coll.CompareTag("Left Hand") || coll.CompareTag("Right Hand"))
        {
            HandInArea = false;
        }
    }

    private void OnActionCanceled(InputAction.CallbackContext context)
    {
        if (ItemInArea && ItemInSlot == null)
        {
            InsertItem(item);
            item = null;
        }
    }

    private void OnActionPerformed(InputAction.CallbackContext context)
    {
        if (ItemInSlot != null && gameObject.activeSelf && HandInArea)
        {
            RemoveItem();
        }
    }

    bool IsItem(GameObject obj)
    {
        return obj.GetComponent<Item>() != null;
    }

    public void InsertItem(GameObject obj)
    {
        Debug.Log("insert item");

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        Collider itemCollider = obj.GetComponent<Collider>();
        if (itemCollider != null)
        {
            itemCollider.isTrigger = true;
        }
        // 設置子物件的 Collider 為 trigger
        Collider[] childColliders = obj.GetComponentsInChildren<Collider>();
        foreach (var collider in childColliders)
        {
            collider.isTrigger = true;
        }

        obj.transform.SetParent(gameObject.transform, true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        Item itemComponent = obj.GetComponent<Item>();
        if (itemComponent != null)
        {
            obj.transform.localEulerAngles = itemComponent.slotRotation;
            itemComponent.inSlot = true;
            itemComponent.currentSlot = this;
        }

        ItemInSlot = obj;
        slotImage.color = Color.clear;
    }

    public void RemoveItem()
    {
        Debug.Log("remove item");

        if (ItemInSlot != null)
        {
            Rigidbody rb = ItemInSlot.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            Collider itemCollider = ItemInSlot.GetComponent<Collider>();
            if (itemCollider != null)
            {
                itemCollider.isTrigger = false;
            }
            // 恢復子物件的 Collider 的 trigger 屬性
            Collider[] childColliders = ItemInSlot.GetComponentsInChildren<Collider>();
            foreach (var collider in childColliders)
            {
                collider.isTrigger = false;
            }

            ItemInSlot.transform.SetParent(null);
            Item itemComponent = ItemInSlot.GetComponent<Item>();
            if (itemComponent != null)
            {
                itemComponent.inSlot = false;
                itemComponent.currentSlot = null;
            }

            ItemInSlot = null;
            ResetColor();
        }
    }

    public void ResetColor()
    {
        slotImage.color = originalColor;
    }

    public void RecordChanges()
    {
        Slot slot = this;
        if (ItemInSlot != null)
        {
            slotItemManager.RegisterItem(slot, ItemInSlot);
        }
        else
        {
            if(slotItemManager.itemSlotMap.ContainsKey(slot.name))
            {
                slotItemManager.UnregisterItem(slot);
            }
        }
    }
}

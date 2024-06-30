using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Slot : MonoBehaviour
{
    public GameObject inventory;
    public GameObject ItemInSlot;
    public Image slotImage;
    public GameObject item;
    Color originalColor;

    public InputActionReference actionReference;
    public InputAction action;

    public bool inArea;

    void Start()
    {
        slotImage = GetComponentInChildren<Image>();
        originalColor = slotImage.color;
        action = actionReference.action;
        action.canceled += OnActionCanceled;
        action.performed += OnActionPerformed;
        inArea = false;
    }

    private void OnTriggerStay(Collider coll)
    {
        if (ItemInSlot != null || !IsItem(coll.gameObject)) 
            return;

        item = coll.gameObject;
        inArea = true;

        XRGrabInteractable grabInteractable = item.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnSelectExited);
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (item != null)
        {
            XRGrabInteractable grabInteractable = item.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.selectExited.RemoveListener(OnSelectExited);
            }
        }

        item = null;
        inArea = false;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (inArea && ItemInSlot == null)
        {
            InsertItem(args.interactableObject.transform.gameObject);
        }
    }

    private void OnActionCanceled(InputAction.CallbackContext context)
    {
        if (inArea && ItemInSlot == null)
        {
            InsertItem(item);
            item = null;
        }
    }

    private void OnActionPerformed(InputAction.CallbackContext context)
    {
        if (ItemInSlot != null && inventory.activeSelf)
            RemoveItem();
    }

    bool IsItem(GameObject obj)
    {
        return obj.GetComponent<Item>() != null;
    }

    public void InsertItem(GameObject obj)
    {
        // 使用全局管理器來添加物品到插槽中
        SlotItemManager.Instance.RegisterItem(obj,this);

        Debug.Log("insert item");

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
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
        // 使用全局管理器來移除插槽中的物品
        SlotItemManager.Instance.UnregisterItem(this);
        if (ItemInSlot != null)
        {
            Rigidbody rb = ItemInSlot.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
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
}

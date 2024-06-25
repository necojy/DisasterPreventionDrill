using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Slot : MonoBehaviour
{
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

    }
    private void OnTriggerExit(Collider coll)
    {
        item = null;
        inArea = false;

    }

    //放開按鈕 放入道具
    private void OnActionCanceled(InputAction.CallbackContext context)
    {
        if(inArea && ItemInSlot == null)
        {
            InsertItem(item);
            item = null;
        }
    }

    private void OnActionPerformed(InputAction.CallbackContext context)
    {
        if(ItemInSlot != null)
            RemoveItem();
    }

    bool IsItem(GameObject obj)
    {
        return obj.GetComponent<Item>();
    }
    void InsertItem(GameObject obj)
    {
        Debug.Log("insert item");
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.SetParent(gameObject.transform, true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = obj.GetComponent<Item>().slotRotation;
        obj.GetComponent<Item>().inSlot = true;
        obj.GetComponent<Item>().currentSlot = this;
        ItemInSlot = obj;
        slotImage.color = Color.clear;
    }

    void RemoveItem()
    {
        Debug.Log("remove item");
        ItemInSlot.GetComponent<Rigidbody>().isKinematic = false;
        ItemInSlot.transform.SetParent(null);
        ItemInSlot.GetComponent<Item>().inSlot = false;
        ItemInSlot.GetComponent<Item>().currentSlot = null;
        ItemInSlot = null;
        ResetColor();
    }

    public void ResetColor()
    {
        slotImage.color = originalColor;
    }
}

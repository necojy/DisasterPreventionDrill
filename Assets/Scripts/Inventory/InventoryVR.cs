using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryVR : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Anchor;
    private bool isUIActive;
    private int currentSlotIndex;
    private List<Slot> slots;

    public InputActionReference secondaryActionReference;
    public InputActionReference primaryActionReference;
    private InputAction secondaryAction;
    private InputAction primaryAction;

    private void Start()
    {
        Inventory.SetActive(false);
        isUIActive = false;
        currentSlotIndex = 0;

        // 過濾具有 Slot 腳本的子對象
        slots = new List<Slot>(Inventory.GetComponentsInChildren<Slot>(true));

        secondaryAction = secondaryActionReference.action;
        secondaryAction.performed += ToggleInventory;

        primaryAction = primaryActionReference.action;
        primaryAction.performed += SwitchSlot;
        
    }

    private void Update()
    {
        Anchor = GameObject.Find("Inventory Anchor");
        if (isUIActive)
        {
            Inventory.transform.position = Anchor.transform.position;
            Inventory.transform.eulerAngles = new Vector3(Anchor.transform.eulerAngles.x + 15, Anchor.transform.eulerAngles.y, 0);
        }
    }

    private void OnDestroy()
    {
        secondaryAction.performed -= ToggleInventory;
        primaryAction.performed -= SwitchSlot;
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        isUIActive = !isUIActive;
        Inventory.SetActive(isUIActive);

        if (isUIActive)
        {
            ShowCurrentSlot();
        }
        else
        {
            HideAllSlots();
        }
    }

    private void SwitchSlot(InputAction.CallbackContext context)
    {
        if (!isUIActive) return;

        slots[currentSlotIndex].gameObject.SetActive(false);
        currentSlotIndex = (currentSlotIndex + 1) % slots.Count;
        ShowCurrentSlot();
    }

    private void ShowCurrentSlot()
    {
        slots[currentSlotIndex].gameObject.SetActive(true);
    }

    private void HideAllSlots()
    {
        foreach (var slot in slots)
        {
            slot.gameObject.SetActive(false);
        }
    }
}

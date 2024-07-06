using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class InventoryVR : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject inventory;
    public GameObject Anchor;
    public List<Slot> slots;
    private bool isUIActive;
    private int currentSlotIndex;
    

    public InputActionReference secondaryActionReference;
    public InputActionReference primaryActionReference;
    private InputAction secondaryAction;
    private InputAction primaryAction;

    private void Start()
    {
        inventory = GameObject.Find("Inventory");
        gameManager = transform.GetComponent<GameManager>();
        slots = new List<Slot>(inventory.GetComponentsInChildren<Slot>(true));
        gameManager.GetSlots(slots);
        inventory.SetActive(false);
        isUIActive = false;
        currentSlotIndex = 0;

        secondaryAction = secondaryActionReference.action;
        secondaryAction.performed += ToggleInventory;

        primaryAction = primaryActionReference.action;
        primaryAction.performed += SwitchSlot;

        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }
    private void Update()
    {
        Anchor = GameObject.Find("Inventory Anchor");
        if (isUIActive)
        {
            inventory.transform.position = Anchor.transform.position;
            inventory.transform.eulerAngles = new Vector3(Anchor.transform.eulerAngles.x + 15, Anchor.transform.eulerAngles.y, 0);
        }
    }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        inventory = GameObject.Find("Inventory");
        slots = new List<Slot>(inventory.GetComponentsInChildren<Slot>(true));
        gameManager.GetSlots(slots);
        inventory.SetActive(false);
    }

    private void OnDestroy()
    {
        secondaryAction.performed -= ToggleInventory;
        primaryAction.performed -= SwitchSlot;
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        isUIActive = !isUIActive;
        inventory.SetActive(isUIActive);

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
        HideAllSlots();
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

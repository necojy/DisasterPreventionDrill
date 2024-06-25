using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryVR : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Anchor;
    bool UIActive;

    public InputActionReference actionReference;
    public InputAction action;

    private void Start()
    {
        Inventory.SetActive(false);
        UIActive = false;
        action = actionReference.action;
        action.performed += ActivateBehavior;
    }

    private void Update()
    {
        if (UIActive)
        {
            Inventory.transform.position = Anchor.transform.position;
            Inventory.transform.eulerAngles = new Vector3(Anchor.transform.eulerAngles.x + 15, Anchor.transform.eulerAngles.y, 0);
        }
    }

    private void ActivateBehavior(InputAction.CallbackContext context)
    {
        UIActive = !UIActive;
        Inventory.SetActive(UIActive);
    }
}

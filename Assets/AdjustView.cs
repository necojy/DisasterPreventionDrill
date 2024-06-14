using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class AdjustView : MonoBehaviour
{
    public InputActionReference actionReference;
    public RectTransform viewer;

    private void Start() 
    {
        InputAction action = actionReference.action;
        action.performed += ActivateBehavior;
        action.Enable(); // 確保啟用該動作
    }

    private void OnDestroy()
    {
        InputAction action = actionReference.action;
        action.performed -= ActivateBehavior;
        action.Disable(); // 禁用該動作以避免內存洩漏
    }

    private void ActivateBehavior(InputAction.CallbackContext context)
    {   
        Debug.Log("123");
        
        Vector2 input = context.ReadValue<Vector2>();
        if (input.y < 0) // Down button
        {
            viewer.anchoredPosition = new Vector2(viewer.anchoredPosition.x, viewer.anchoredPosition.y - 100f);
        }
        else if (input.y > 0) // Up button
        {
            viewer.anchoredPosition = new Vector2(viewer.anchoredPosition.x, viewer.anchoredPosition.y + 100f);
        }

    }
}

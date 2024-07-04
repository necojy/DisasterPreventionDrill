using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class StoveKnob : MonoBehaviour
{
    public InputActionReference LeftReference;
    public InputActionReference RightReference;
    public GasEffect gasEffect;
    public Animator stove;
    private bool isOn = true;
    private bool isTouching = false;
    private InputAction leftHandGrab;
    private InputAction rightHandGrab;

    void Start()
    {
        stove = GameObject.Find("Stove_01 (2)").GetComponent<Animator>();

        leftHandGrab = LeftReference.action;
        leftHandGrab.performed += OnLeftHandGrab;
        leftHandGrab.Enable();

        rightHandGrab = RightReference.action;
        rightHandGrab.performed += OnRightHandGrab;
        rightHandGrab.Enable();
    }

    private void OnDestroy()
    {
        leftHandGrab.performed -= OnLeftHandGrab;
        rightHandGrab.performed -= OnRightHandGrab;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Left Hand") || coll.CompareTag("Right Hand"))
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Left Hand") || coll.CompareTag("Right Hand"))
        {
            isTouching = false;
        }
    }

    private void OnLeftHandGrab(InputAction.CallbackContext context)
    {
        if (isTouching)
        {
            TurnOff();
        }
    }

    private void OnRightHandGrab(InputAction.CallbackContext context)
    {
        if (isTouching)
        {
            TurnOff();
        }
    }

    public void TurnOff()
    {
        gasEffect = FindObjectOfType<GasEffect>();
        if (isOn && gasEffect != null)
        {
            Debug.Log("Stove is turned off");
            stove.SetBool("isTurnOff", true);
            gasEffect.CloseEffect();
            isOn = false;
        }
    }
}

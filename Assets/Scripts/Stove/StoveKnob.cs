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
    public Animator leftHandAnim;
    public Animator rightHandAnim;
    private bool isOn = true;
    public bool isTouchingRight = false;
    public bool isTouchingLeft = false;
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
        if (coll.CompareTag("Left Hand"))
        {
            isTouchingLeft = true;
        }
        if (coll.CompareTag("Right Hand"))
        {
            isTouchingRight = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Left Hand"))
        {
            isTouchingLeft = false;
        }
        if (coll.CompareTag("Right Hand"))
        {
            isTouchingRight = false;
        }
    }

    private void OnLeftHandGrab(InputAction.CallbackContext context)
    {
        if (isTouchingLeft)
        {
            TurnOff();
        }
    }

    private void OnRightHandGrab(InputAction.CallbackContext context)
    {
        if (isTouchingRight)
        {
            TurnOff();
        }
    }

    public void TurnOff()
    {
        gasEffect = FindObjectOfType<GasEffect>();
        leftHandAnim = GameObject.Find("Left Hand Model").GetComponent<Animator>();
        rightHandAnim = GameObject.Find("Right Hand Model").GetComponent<Animator>();
        if (isOn && gasEffect != null)
        {
            Debug.Log("Stove is turned off");
            stove.SetBool("isTurnOff", true);
            if(isTouchingLeft)
            {
                leftHandAnim.SetBool("isTurnOff",true);
            }
            if(isTouchingRight)
            {
                rightHandAnim.SetBool("isTurnOff",true);
            }
            gasEffect.CloseEffect();
            isOn = false;
        }

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        leftHandAnim.SetBool("isTurnOff",false);
        rightHandAnim.SetBool("isTurnOff",false);
    }
}

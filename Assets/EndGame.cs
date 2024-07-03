using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private GameObject end;
    private Animator endAnimator;
    private Camera endCamera;
    private Camera MainCamera;

    private void Start()
    {
        end = GameObject.Find("ending");
        endAnimator = end.GetComponent<Animator>();
        endCamera = GameObject.Find("endCamera").GetComponent<Camera>();
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        endCamera.enabled = false;
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            MainCamera.enabled = false;
            endCamera.enabled = true;
            endAnimator.SetBool("isEnding", true);
        }
    }
}

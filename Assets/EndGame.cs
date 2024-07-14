using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // private GameObject end;
    //private Animator endAnimator;
    private Camera endCamera;
    private Camera MainCamera;
    private GameObject endImage;

    private void Start()
    {
        // end = GameObject.Find("ending");
        //endAnimator = end.GetComponent<Animator>();
        endCamera = GameObject.Find("endCamera").GetComponent<Camera>();
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        endCamera.enabled = false;
        
        endImage = GameObject.Find("endingCanvas");
        endImage.SetActive(false);

    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            MainCamera.enabled = false;
            endCamera.enabled = true;
            StartCoroutine(WaitToOpen());
            //endAnimator.SetBool("isEnding", true);
        }
    }

    private IEnumerator WaitToOpen()
    {
        yield return new WaitForSeconds(0.5f);
        endImage.SetActive(true);
    }
}

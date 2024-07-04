using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRodaHint : MonoBehaviour
{
    private Camera mainCamera;
    private Camera hintCamera;
    private GameObject selectRoda;
    private Animator SelectRodaAnimator;
    // private Animator hintCameraAnimator;

    // private GameObject left;
    // private Animator leftAnimator;

    // private GameObject right;
    // private Animator rightAnimator;
    private bool isTrigger = false;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        hintCamera = GameObject.Find("roadCamera").GetComponent<Camera>();
        // hintCameraAnimator = hintCamera.GetComponent<Animator>();

        // left = GameObject.Find("left");
        // leftAnimator = left.GetComponent<Animator>();

        // right = GameObject.Find("Right");
        // rightAnimator = right.GetComponent<Animator>();

        // left.SetActive(false);
        // right.SetActive(false);

        hintCamera.enabled = false;
        selectRoda = GameObject.Find("SelectRoad");
        SelectRodaAnimator = selectRoda.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTrigger)
        {
            // StartCoroutine(StartHint());
        }
    }

    public IEnumerator StartHint()
    {
        isTrigger = true;

        hintCamera.enabled = true;
        mainCamera.enabled = false;
        // left.SetActive(true);
        // right.SetActive(true);


        // rightAnimator.SetBool("isHint", true);
        // yield return new WaitForSeconds(5f);
        // right.SetActive(false);

        // hintCameraAnimator.SetBool("isRotate", true);
        // yield return new WaitForSeconds(2f);

        // leftAnimator.SetBool("isHint", true);
        // yield return new WaitForSeconds(2.5f);
        // left.SetActive(false);


        SelectRodaAnimator.SetBool("isRight", true);
        yield return new WaitForSeconds(2.5f);

        SelectRodaAnimator.SetBool("isRotate", true);
        yield return new WaitForSeconds(2f);

        SelectRodaAnimator.SetBool("isLeft", true);
        yield return new WaitForSeconds(2.5f);


        mainCamera.enabled = true;
        hintCamera.enabled = false;
        selectRoda.SetActive(false);
    }
}

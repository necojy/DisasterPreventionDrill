using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRodaHint : MonoBehaviour
{
    private GameObject mainCamera;
    private GameObject hintCamera;
    private Animator hintCameraAnimator;

    private GameObject left;
    private Animator leftAnimator;

    private GameObject right;
    private Animator rightAnimator;


    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");

        hintCamera = GameObject.Find("roadCamera");
        hintCameraAnimator = hintCamera.GetComponent<Animator>();

        left = GameObject.Find("left");
        leftAnimator = left.GetComponent<Animator>();

        right = GameObject.Find("Right");
        rightAnimator = right.GetComponent<Animator>();

        hintCamera.SetActive(false);
        left.SetActive(false);
        right.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StartHint());
        }
    }

    private IEnumerator StartHint()
    {
        hintCamera.SetActive(true);
        mainCamera.SetActive(false);
        left.SetActive(true);
        right.SetActive(true);


        rightAnimator.SetBool("isHint", true);
        yield return new WaitForSeconds(2.5f);
        right.SetActive(false);

        hintCameraAnimator.SetBool("isRotate", true);
        yield return new WaitForSeconds(2f);

        leftAnimator.SetBool("isHint", true);
        yield return new WaitForSeconds(2.5f);
        left.SetActive(false);


        mainCamera.SetActive(true);
        hintCamera.SetActive(false);
    }
}

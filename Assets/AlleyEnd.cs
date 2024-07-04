using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlleyEnd : MonoBehaviour
{
    private GameObject mainCamera;
    private GameObject hintCamera;
    public GameObject obj;
    private Animator objAnimator;
    private Animator hintCameraAnimator;
    private bool isTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        hintCamera = GameObject.Find("EndingCamera");
        hintCameraAnimator = hintCamera.GetComponent<Animator>();
        objAnimator = obj.GetComponent<Animator>();
        hintCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTrigger)
        {
            StartCoroutine(StartHint());
            // objAnimator = obj.GetComponent<Animator>();
            //StartCoroutine(PlayDead());
        }
    }
    private IEnumerator StartHint()
    {
        isTrigger = true;
        hintCamera.SetActive(true);
        mainCamera.SetActive(false);
        hintCameraAnimator.SetBool("Alley", true);
        objAnimator.SetBool("isFalling", true);
        yield return new WaitForSeconds(7f);
        objAnimator.SetBool("isFalling", false);
        yield return new WaitForSeconds(1f);
        mainCamera.SetActive(true);
        hintCamera.SetActive(false);
        //yield return null;
    }
    private IEnumerator PlayDead()
    {

        yield return new WaitForSeconds(5f);
    }
}

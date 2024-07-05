using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlleyEnd : MonoBehaviour
{
    private GameObject mainCamera;
    private GameObject hintCamera;
    private GameObject tempCamera;
    public GameObject obj;
    private Animator objAnimator;
    private Animator hintCameraAnimator;
    private Animator tempCameraAnimator;
    private bool isTrigger = false;
    // Start is called before the first frame update

    private ShowDeadCanvas showDeadCanvas;

    void Start()
    {
        StartCoroutine(showDeadCanvas.ShowDeadCanva());

        // mainCamera = GameObject.Find("Main Camera");
        // hintCamera = GameObject.Find("EndingCamera");
        // tempCamera = GameObject.Find("tempCamera");

        // hintCameraAnimator = hintCamera.GetComponent<Animator>();
        // tempCameraAnimator = tempCamera.GetComponent<Animator>();
        // objAnimator = obj.GetComponent<Animator>();

        // hintCamera.SetActive(false);
        // tempCamera.SetActive(false);

        showDeadCanvas = GetComponent<ShowDeadCanvas>();

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
        tempCamera.SetActive(true);
        // mainCamera.SetActive(false);

        hintCameraAnimator.SetBool("Alley", true);
        tempCameraAnimator.SetBool("Alley", true);

        objAnimator.SetBool("isFalling", true);
        yield return new WaitForSeconds(7f);

        objAnimator.SetBool("isFalling", false);
        yield return new WaitForSeconds(1f);



        // mainCamera.SetActive(true);
        hintCamera.SetActive(false);
        tempCamera.SetActive(false);
        // StartCoroutine(showDeadCanvas.ShowDeadCanva());
        //yield return null;
    }

}

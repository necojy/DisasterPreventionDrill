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
    private ShowDeadCanvas showDeadCanvas;


    void Start()
    {

        mainCamera = GameObject.Find("Main Camera");
        hintCamera = GameObject.Find("EndingCamera");
        tempCamera = GameObject.Find("tempCamera");
        showDeadCanvas = GameObject.Find("player deadth control").GetComponent<ShowDeadCanvas>();

        hintCameraAnimator = hintCamera.GetComponent<Animator>();
        tempCameraAnimator = tempCamera.GetComponent<Animator>();
        objAnimator = obj.GetComponent<Animator>();

        hintCamera.SetActive(false);
        tempCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTrigger)
        {

            StartCoroutine(StartHint());
            objAnimator = obj.GetComponent<Animator>();
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

        // objAnimator.SetBool("isFalling", false);
        // yield return new WaitForSeconds(1f);
        showDeadCanvas.deadReason = "被掉落物砸死";
        showDeadCanvas.reloadScene = "Streets";
        StartCoroutine(showDeadCanvas.ShowDeadCanva());

        // hintCamera.SetActive(false);
        // tempCamera.SetActive(false);
    }

}

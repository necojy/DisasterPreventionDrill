using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badendingsc : MonoBehaviour
{
    private GameObject mainCamera;
    private GameObject hintCamera;
    public GameObject obj;
    private Animator objAnimator;
    private Animator hintCameraAnimator;
    private bool isTrigger = false;
    private ShowDeadCanvas showDeadCanvas;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        hintCamera = GameObject.Find("AirconCamera");
        showDeadCanvas = GameObject.Find("player deadth control").GetComponent<ShowDeadCanvas>();
        
        hintCameraAnimator = hintCamera.GetComponent<Animator>();
        objAnimator = obj.GetComponent<Animator>();
        
        hintCamera.SetActive(false);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTrigger)
        {

            StartCoroutine(StartAnim());
            objAnimator = obj.GetComponent<Animator>();
        }
    }
    private IEnumerator StartAnim(){
        //isTrigger = true;
        mainCamera.SetActive(false);
        hintCamera.SetActive(true);

        hintCameraAnimator.SetBool("Apartment",true);
        objAnimator.SetBool("Airconfall",true);
        StartCoroutine(WaitToPlay());
        yield return new WaitForSeconds(4f);

        AudioManager.instance.PlayItemSound("aircondition_after");

        showDeadCanvas.deadReason = "被掉落物砸死";
        showDeadCanvas.reloadScene = "Streets";
        StartCoroutine(showDeadCanvas.ShowDeadCanva());
    }

    private IEnumerator WaitToPlay()
    {
        yield return new WaitForSeconds(2f);
        AudioManager.instance.PlayItemSound("aircondition_before");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Lighter : MonoBehaviour 
{
    public GameObject flame; 
    public GameObject explosion;

    public Animator Lighter_model;
    private bool isIgnite = false;


    // 控制手部動畫
    private XRGrabInteractable controller; // 判斷左右手
    public Animator Left_Lighter_Animation;
    public Animator Right_Lighter_Animation;
    private Animator GrabHand = null;

    public ShowCanvas showCanvas;


    void Start()
    {
        flame.SetActive(false);
        explosion.SetActive(false);
        isIgnite = false;
        controller = GetComponent<XRGrabInteractable>(); // 正確的初始化方式
        GrabHand = Left_Lighter_Animation;
    }

    public void Ignite()
    {
        if(!isIgnite)
        {
            isIgnite = true;
            if (controller.selectingInteractor != null)
            {
                if (controller.selectingInteractor.CompareTag("Left Hand")) GrabHand = Left_Lighter_Animation;
                else if (controller.selectingInteractor.CompareTag("Right Hand")) GrabHand = Right_Lighter_Animation;
            }

            Lighter_model.SetBool("open_model",true);
            GrabHand.SetBool("Lighter_Open",true);
            AudioManager.instance.PlayItemSound("Open_lightet");
            StartCoroutine(AnimateAndToggle());
            StartCoroutine(Explode());
        }

        else
        {
            isIgnite = false;
            flame.SetActive(false);
            GrabHand.SetBool("Lighter_Open",false);
            Lighter_model.SetBool("open_model",false);
        }

        
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(2.0f);
        explosion.SetActive(true);

        yield return new WaitForSeconds(1f);
        showCanvas.Dead(2);
        flame.SetActive(false);
    }

    //等待動畫撥放完畢
    IEnumerator AnimateAndToggle()
    {
        while (GrabHand.GetCurrentAnimatorStateInfo(0).IsName("open_lighter.anim"))
        {
            yield return null;
        }

        while (GrabHand.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 < 0.99f)
        {
            yield return null;
        }

        flame.SetActive(!flame.activeSelf);
    }

}

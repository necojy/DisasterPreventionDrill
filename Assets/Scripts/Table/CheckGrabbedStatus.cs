using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CheckGrabbedStatus : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private CameraShake cameraShake;
    // private ShowCanvas showCanvas;
    private HideUnderTable hideUnderTable;
    private ShowDeadCanvas showDeadCanvas;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        cameraShake = FindObjectOfType<CameraShake>();
        // showCanvas = FindObjectOfType<ShowCanvas>();
        showDeadCanvas = GameObject.Find("player deadth control").GetComponent<ShowDeadCanvas>();

        hideUnderTable = GameObject.Find("HideArea").GetComponent<HideUnderTable>();
        if (grabInteractable != null)
        {
            // 訂閱抓取事件
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    void Update()
    {
        // 檢查物件是否被抓取
        if (grabInteractable != null && grabInteractable.isSelected)
        {
            Debug.Log("Object is grabbed!");
            // 在這裡添加相應的處理邏輯
        }
        else
        {
            //Debug.Log("Object is not grabbed.");
            if (hideUnderTable.PromptEnd && cameraShake.isShaking == true)
            {
                // showCanvas.ShowDeadCanvas();
                showDeadCanvas.deadReason = "被掉落物砸死";
                showDeadCanvas.reloadScene = "livingRoom";
                StartCoroutine(showDeadCanvas.ShowDeadCanva());
            }

        }

    }

    // 處理被抓取事件
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("Object grabbed!");
        // 在這裡處理被抓取時的行為
    }

    // 處理被釋放事件
    private void OnReleased(SelectExitEventArgs args)
    {
        // 在這裡處理被釋放時的行為
        if (cameraShake.isShaking == true)
        {
            showDeadCanvas.deadReason = "被掉落物砸死";
            showDeadCanvas.reloadScene = "livingRoom";
            StartCoroutine(showDeadCanvas.ShowDeadCanva());
            // showCanvas.ShowDeadCanvas();
        }
    }
}

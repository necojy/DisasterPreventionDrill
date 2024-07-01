using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CheckGrabbedStatus : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private CameraShake cameraShake;
    private ShowCanvas showCanvas;
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        cameraShake = FindObjectOfType<CameraShake>();
        showCanvas = FindObjectOfType<ShowCanvas>();

        if (grabInteractable != null)
        {
            // 訂閱抓取事件
            grabInteractable.onSelectEntered.AddListener(OnGrabbed);
            grabInteractable.onSelectExited.AddListener(OnReleased);
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
            Debug.Log("Object is not grabbed.");
            // 在這裡添加相應的處理邏輯
        }
    }

    // 處理被抓取事件
    private void OnGrabbed(XRBaseInteractor interactor)
    {
        Debug.Log("Object grabbed!");
        // 在這裡處理被抓取時的行為
    }

    // 處理被釋放事件
    private void OnReleased(XRBaseInteractor interactor)
    {
        // 在這裡處理被釋放時的行為
        if(cameraShake.isShaking == true){
            showCanvas.ShowDeadCanvas();
        }
    }
}

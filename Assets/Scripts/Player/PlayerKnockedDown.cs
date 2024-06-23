using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockedDown : MonoBehaviour
{

    public ShowCanvas showCanvas;

    #region Timer
    public TimeControl timeControl;
    public GameObject Timer;

    #endregion


    #region 事件
    public CameraShake cameraShake;
    public HideUnderTable hideUnderTable;     // 躲桌子
    public Animator bookCaseAnimator;
    // private bool sucess = true;

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (cameraShake.isShaking && !timeControl.timeOut && other.CompareTag("Falling"))
        {
            bookCaseAnimator.SetBool("isFalling",true);
            showCanvas.Dead(1);
        }
    }

    private void Update()
    {
        if(timeControl.timeOut)
        {
            Timer.SetActive(false);
        }
        
        // if(!sucess) showCanvas.Dead();

        // 沒在時間內躲在桌子下
        if (cameraShake.isShaking && timeControl.timeOut && !hideUnderTable.isHiding && !showCanvas.reStart)
        {
            showCanvas.Dead(1);
        }
        
    }

    

}

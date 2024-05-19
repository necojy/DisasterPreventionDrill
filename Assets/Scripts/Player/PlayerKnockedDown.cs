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

    #region 復活參數
    public int rebirth_index = 0;
    public Transform[] rebirthPos;
    #endregion

    #region 事件
    public CameraShake cameraShake;
    public HideUnderTable hideUnderTable;     // 躲桌子
    public Animator bookCaseAnimator;
    private bool sucess = true;

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (cameraShake.isShaking && !timeControl.timeOut && other.CompareTag("Falling"))
        {
            bookCaseAnimator.SetBool("isFalling",true);
            showCanvas.Dead();
        }
    }

    private void Update()
    {
        if(timeControl.timeOut)
        {
            Timer.SetActive(false);
        }
        
        if(!sucess) showCanvas.Dead();

        // 沒在時間內躲在桌子下
        if (cameraShake.isShaking && timeControl.timeOut && !hideUnderTable.isHiding)
        {
            sucess = false;
        }
        
    }
    // private void Update() {

    //     if(timeControl.timeOut)
    //     {
    //         Timer.SetActive(false);
    //     }

    //     if(!hideUnderTable.isHiding && timeControl.startGame && timeControl.timeOut && !sucess)
    //     {
    //         showCanvas.Dead();
    //     }
    //     if(hideUnderTable.isHiding)
    //     {
    //         sucess = true;
    //     }
    // }

    // public void Rebirth()
    // {
    //     deadPanel.SetActive(false);
    //     fade_animator.SetBool("fadein",false);
    //     camera_offset.position = rebirthPos[rebirth_index].position;
    // }

}

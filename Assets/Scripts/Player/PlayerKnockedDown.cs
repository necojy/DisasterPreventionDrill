using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockedDown : MonoBehaviour
{
    #region 復活參數
    public int rebirth_index = 0;
    public Transform[] rebirthPos;
    #endregion

    public CameraShake cameraShake;
    public ShowCanvas showCanvas;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Falling") && cameraShake.isShaking)
        {
            showCanvas.Dead();
        }
    }

    // public void Rebirth()
    // {
    //     deadPanel.SetActive(false);
    //     fade_animator.SetBool("fadein",false);
    //     camera_offset.position = rebirthPos[rebirth_index].position;
    // }

}

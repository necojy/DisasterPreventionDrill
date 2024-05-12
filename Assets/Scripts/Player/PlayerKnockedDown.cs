using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockedDown : MonoBehaviour
{
    // 判斷是否在地震當下
    public CameraShake cameraShake;
    public Transform body;
    public Transform head;
    public GameObject deadPanel;
    public float canva_TO_headDistance = 2f; //死亡畫面到頭部的距離
    public float knockedDownTime = 1f; // 旋轉的時間
    public Animator fade_animator;
    private bool isRotating = false;

    //設定復活參數
    public int rebirth_index = 0;
    public Transform[] rebirthPos;

    private void Start() 
    {
        deadPanel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Faling") && !isRotating && cameraShake.isShaking )
        {
            StartCoroutine(RotateHead());
            Destroy(other.gameObject);
        }
    }

    //模擬人物倒下
    private IEnumerator RotateHead()
    {

        Quaternion targetRotation = Quaternion.Euler(body.rotation.eulerAngles.x - 90f, body.rotation.eulerAngles.y, body.rotation.eulerAngles.z);

        float elapsedTime = 0f;
        Quaternion startRotation = body.rotation;

        fade_animator.SetBool("fadein",true);
        while (elapsedTime < knockedDownTime)
        {
            body.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / knockedDownTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        body.rotation = targetRotation;

        yield return new WaitForSeconds(0.5f);
        isRotating = true;

        deadPanel.SetActive(true);
        body.rotation = Quaternion.Euler(0,0,0);

    }

    public void Rebirth()
    {
        deadPanel.SetActive(false);
        fade_animator.SetBool("fadein",false);
        body.position = rebirthPos[rebirth_index].position;
    }

}

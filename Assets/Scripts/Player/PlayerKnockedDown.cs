using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockedDown : MonoBehaviour
{
    public Transform body;
    public Transform head;
    public GameObject deadPanel;
    public float canva_TO_headDistance = 2f; //死亡畫面到頭部的距離
    public float knockedDownTime = 1f; // 旋轉的時間

    public Animator animator;

    private bool isRotating = false;

    private void Start() {
        deadPanel.SetActive(false);
    }
    private void Update()
    {
        //更新死亡畫面位置
        if (isRotating)
        {
            deadPanel.SetActive(true);
            body.rotation = Quaternion.Euler(0,0,0);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Faling") && !isRotating)
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

        animator.SetBool("fadein",true);
        while (elapsedTime < knockedDownTime)
        {
            body.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / knockedDownTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        body.rotation = targetRotation;

        yield return new WaitForSeconds(0.5f);
        isRotating = true;
    }
}

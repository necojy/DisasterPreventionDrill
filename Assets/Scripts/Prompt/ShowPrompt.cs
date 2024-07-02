using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowPrompt : MonoBehaviour
{
    public GameObject prompt;
    public float detectionRadius = 0.5f; // 檢測半徑
    private void Start()
    {
        prompt.gameObject.SetActive(false);
    }

    private void Update()
    {
        Detect();
        // 讓 Canvas 面向玩家位置
        if (prompt.activeSelf)
        {
            Vector3 playerPos = FindPlayerPosition();
            if (playerPos != Vector3.zero)
            {
                // 計算提示 UI 到玩家位置的方向向量
                Vector3 direction = (playerPos - prompt.transform.position).normalized;

                // 計算提示 UI 的目標旋轉
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // 反轉 Y 軸方向
                Vector3 euler = targetRotation.eulerAngles;
                euler.x = 0f;
                euler.z = 0f;
                euler.y += 180f; // 可根據需要調整角度
                targetRotation = Quaternion.Euler(euler);

                // 設置提示 UI 的旋轉
                prompt.transform.rotation = targetRotation;
            }
        }
    }

    private void Detect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                ShowHint();
                return;
            }
        }
        HideHint();
    }

    public void ShowHint()
    {
        if (!prompt.gameObject.activeSelf)
        {
            prompt.transform.position = new Vector3(transform.position.x,transform.position.y+0.1f,transform.position.z);
            prompt.gameObject.SetActive(true); // 顯示
        }
    }

    public void HideHint()
    {
        if (prompt.gameObject.activeSelf)
        {
            prompt.gameObject.SetActive(false); // 隱藏
        }
    }
    Vector3 FindPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("MainCamera");
        if (player != null)
        {
            return player.transform.position;
        }
        return Vector3.zero;
    }
}

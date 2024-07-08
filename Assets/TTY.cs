using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
public class TTY : MonoBehaviour
{
    public InputActionProperty recenter;

    public Transform head;
    public Transform original;
    public Transform target;


    private void Start()
    {
        StartCoroutine(UU());
    }


    public IEnumerator UU()
    {
        yield return new WaitForSeconds(3f);
        Recenter();

    }

    public void Recenter()
    {
        // 計算頭部（頭顯）位置和原始位置之間的偏移量
        Vector3 offset = head.position - original.position;
        offset.y = 0; // 忽略 y 軸上的偏移量，僅考慮水平平面的偏移

        // 將原始位置設置為目標位置減去偏移量
        original.position = target.position - offset;

        // 獲取目標的前向向量，並將 y 軸設置為 0，僅考慮水平平面的方向
        Vector3 targetFoward = target.forward;
        targetFoward.y = 0;

        // 獲取頭部（頭顯）的前向向量，並將 y 軸設置為 0，僅考慮水平平面的方向
        Vector3 cameraFoward = head.forward;
        cameraFoward.y = 0;

        // 計算頭部（頭顯）前向向量與目標前向向量之間的夾角
        float angle = Vector3.SignedAngle(cameraFoward, targetFoward, Vector3.up);

        // 以頭部（頭顯）位置為中心，繞 y 軸旋轉原始位置，使其對準目標方向
        original.RotateAround(head.position, Vector3.up, angle);
    }


}

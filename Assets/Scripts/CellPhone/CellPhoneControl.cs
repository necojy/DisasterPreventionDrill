using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPhoneControl : MonoBehaviour
{
    public GameObject CellPhoneCanvas;

    // 開啟Canvas
    public void ShowCanvas()
    {
        CellPhoneCanvas.SetActive(true);
    }

    // 關閉Canvas
    public void HideCanvas()
    {
        CellPhoneCanvas.SetActive(false);
    }
}

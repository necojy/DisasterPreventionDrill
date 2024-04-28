using UnityEngine;
using UnityEngine.UI;

public class CellPhoneControl : MonoBehaviour
{
    public GameObject CellPhoneCanvas;
    public Slider slider; 
    public GameObject spotlight; 

    private void Start() 
    {
        spotlight.SetActive(false);
        CellPhoneCanvas.SetActive(false);
    }

    public void OpenSpotLight()
    {
        spotlight.SetActive(!spotlight.active);
        CellPhoneCanvas.SetActive(!CellPhoneCanvas.active);

    }
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

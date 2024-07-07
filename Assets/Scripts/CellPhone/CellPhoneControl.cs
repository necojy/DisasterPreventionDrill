using UnityEngine;
using UnityEngine.UI;

public class CellPhoneControl : MonoBehaviour
{
    public GameObject spotlight;
    private GameObject alert;

    private void Start()
    {
        alert = GameObject.Find("alert");
        alert.SetActive(true);
        spotlight.SetActive(false);
    }

    public void OpenSpotLight()
    {
        AudioManager.instance.PlayItemSound("Open_cellPhone");
        spotlight.SetActive(!spotlight.activeSelf);
    }

    public void CloseAlert()
    {
        alert.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class CellPhoneControl : MonoBehaviour
{
    public GameObject spotlight; 

    private void Start() 
    {
        spotlight.SetActive(false);
    }

    public void OpenSpotLight()
    {
        AudioManager.instance.PlayItemSound("Open_cellPhone");
        spotlight.SetActive(!spotlight.activeSelf);
    }
}

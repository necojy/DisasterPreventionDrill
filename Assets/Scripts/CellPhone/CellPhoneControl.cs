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
        spotlight.SetActive(!spotlight.active);
    }
}

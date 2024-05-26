using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeOption : MonoBehaviour
{
         public float time = 5f;
    public GameObject Option_canvas;
    
    private void Start()
    {
        Option_canvas.SetActive(false);
        TT();
        Show_canvas(time);
    }
    public void TT(){
        float elapsed = 0.0f;
        float duration = 4f;
        while(elapsed < duration){
            elapsed += Time.deltaTime;
        }
    }

    public void Show_canvas(float duration){
        float elapsed = 0.0f;
        Option_canvas.SetActive(true);
        while(elapsed < duration){
            elapsed += Time.deltaTime;
        }
        Option_canvas.SetActive(false);
    }

}

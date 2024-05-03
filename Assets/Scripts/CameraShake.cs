using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Vector3 degree = new Vector3(0.05f,0.025f,0);
    private void Start() {
        StartCoroutine(Shake(10f));

    }
    public IEnumerator Shake(float duration)
    {

        //Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
           
            degree=-degree;
            transform.localPosition += degree;

            elapsed += Time.deltaTime;
            yield return null;
        }
        //transform.localPosition = originalPos;
    }

}

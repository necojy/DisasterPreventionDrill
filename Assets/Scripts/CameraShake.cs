using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator animator;
    private void Start() {
        StartCoroutine(Shake(10f,0.4f));

    }
    public IEnumerator Shake(float duration,float magnitude)
    {

        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        float x = 0.05f;
        float y = 0.025f;
        while(elapsed < duration)
        {
            //float x = Random.Range(-1f,1f) * magnitude;
            x=-x;y=-y;
            transform.localPosition += new Vector3(x,y,0);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
        animator.SetBool("turn_off",true);
    }

}

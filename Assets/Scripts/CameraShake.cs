using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{   
    public float time=10f;
    public float level=0.25f;
    public Animator bookCase_animator;
    public GameObject Option_canvas;

    [HideInInspector] public bool isShaking = false;

    public float waitForDown = 1f;
    public GameObject[] Fall_Object;

    private void Start() 
    {
        Option_canvas.SetActive(false);
        isShaking = true;
        StartCoroutine(Shake(time,level));
        bookCase_animator.SetBool("Fall",true);
        InitFallObject();
    }
    public IEnumerator Shake(float duration, float magnitude)
    {

        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while(elapsed < duration/2)
        {
           
            float x =Random.Range(-1f,1f)*magnitude;
            float y =Random.Range(-1f,1f)*magnitude;
            transform.localPosition = new Vector3(originalPos.x+x,originalPos.y+y,originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Option_canvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        Option_canvas.SetActive(false);
        

        while(elapsed < duration)
        {
           
            float x =Random.Range(-1f,1f)*magnitude;
            float y =Random.Range(-1f,1f)*magnitude;
            transform.localPosition = new Vector3(originalPos.x+x,originalPos.y+y,originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        
        isShaking = false;
        transform.localPosition = originalPos;
    }

    private void InitFallObject()
    {
        foreach(GameObject Fall in Fall_Object)
        {
            Fall.GetComponent<Rigidbody>().useGravity = false;
        }

        StartCoroutine(Falling());
    }

    private IEnumerator Falling()
    {
        yield return new WaitForSeconds(waitForDown);
        foreach(GameObject Fall in Fall_Object)
        {
            Fall.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}

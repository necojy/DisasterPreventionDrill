using System.Collections;
using System.Collections.Generic;
//using UnityEditor.TestTools.TestRunner.Api;
using UnityEngine;

public class ShowOption1 : MonoBehaviour
{
    public float time = 5f;
    public GameObject Option_canvas;
    private void Start(){
        Option_canvas.SetActive(false);
        StartCoroutine(Test());
    }
    public IEnumerator Test(){
        yield return new WaitForSeconds(3f);
        Option_canvas.SetActive(true);
        yield return new WaitForSeconds(5f);
        Option_canvas .SetActive(false);
    }
}

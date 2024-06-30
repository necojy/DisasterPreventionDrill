using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulldoor : MonoBehaviour
{
    public GameObject obj;
    //public GameObject handheight;
    private float rotatespeed = 1f;
    public Transform InPos;
    public Transform OrigPos;
    public Transform OutPos;
    public Transform hand;
    public Transform handle;
    private Vector3 startPos, handstart;

    public bool isOpening = false;
    private float CalculateLocation(Transform hand)
    {
        isOpening = true;
        Vector3 vectorHO = hand.position - OrigPos.position;
        vectorHO.y = 0;
        Vector3 vectorIO = InPos.position - OrigPos.position;
        //vectorIO.y=0;
        float dotProduct = Vector3.Dot(vectorHO, vectorIO);
        // 計算向量的長度
        float magnitudeHO = vectorHO.magnitude;
        float magnitudeIO = vectorIO.magnitude;
        //check
        // Debug.Log("dotProduct: " + dotProduct);
        // Debug.Log("magnitudeHO: " + magnitudeHO);
        // Debug.Log("magnitudeIO: " + magnitudeIO);
        // 計算夾角的餘弦值
        float cosAngle = dotProduct / (magnitudeHO * magnitudeIO);
        Debug.Log(cosAngle);
        float angleRadians = Mathf.Acos(cosAngle);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        return angleDegrees / 90;

    }

    void Update()
    {

        float currentLocation = CalculateLocation(hand);
        if (currentLocation > (89 / 90))
        {

        }
        //obj.transform.position = startPos;
        //handle.position = hand.position;
        obj.transform.rotation = Quaternion.Slerp(OrigPos.rotation, OutPos.rotation, currentLocation);
    }

    void Start()
    {
        //startPos = obj.transform.position;
        //handstart = hand.position;
        //     StartCoroutine(Playopendoor());
        // }
        // float rotatetime = 3f;
        // public IEnumerator Playopendoor(){
        //     float elapsedTime = 0f;
        //     while(elapsedTime < rotatetime){
        //         obj.transform.rotation = Quaternion.Slerp(InPos.rotation,OutPos.rotation,elapsedTime / rotatetime);
        //         elapsedTime += Time.deltaTime * rotatespeed;
        //         yield return null;
        //     }

    }
}
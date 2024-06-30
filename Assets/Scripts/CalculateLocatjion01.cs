using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class CalculateLocatjion01 : MonoBehaviour
{
    public GameObject obj;
    public Transform InPos;
    public Transform OutPos;
    public Transform hand;
    private float CalculateLocatjion(Transform hand){
        Vector3 OriginVec3 = OutPos.position - InPos.position;

        float length = OriginVec3.magnitude;
        OriginVec3.Normalize();

        Vector3 InPos2Hand = hand.position -InPos.position;
        return Vector3.Dot(InPos2Hand, OriginVec3)/length;
    }

    // protected virtual void HandAttachedUpdate(){
    //     float currentLocation = CalculateLocatjion(hand);
    //     obj.transform.position = Vector3.Lerp(InPos.position, OutPos.position, currentLocation);
    //     hand.position = Vector3.Lerp(InPos.position, OutPos.position, currentLocation);
    // }

    // Update is called once per frame
    void Update()
    {
        float currentLocation = CalculateLocatjion(hand);
        obj.transform.position = Vector3.Lerp(InPos.position, OutPos.position, currentLocation);
        
    }
}

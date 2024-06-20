using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevorStairOption : MonoBehaviour
{
    public Transform rotatePosition;
    public Transform xr_orig;
    public Transform camera_Offset;
    public float rotateSpeed = 1f;
    public float rotateViewTime = 1f;
    public Camera mainCamera;
    public Camera optionCamera;
    void Start()
    {
        StartCoroutine(MovePlayer(mainCamera.transform, optionCamera.transform, xr_orig));
    }

    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //StartCoroutine(MovePlayer(mainCamera.transform, rotatePosition));
        }
    }

    IEnumerator MovePlayer(Transform initial,Transform target, Transform playerPos)
    {
        Quaternion initialRotation = initial.rotation;
        //Vector3 direction = target.position - initial.position;
        //Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion targetRotation = target.rotation;
        Vector3 initialPos = playerPos.position;
        Vector3 targetPos = target.position;
        targetPos.y = initialPos.y;
        float elapsedTime = 0f;
        while (elapsedTime < rotateViewTime)
        {
            camera_Offset.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotateViewTime);
            xr_orig.position = Vector3.Lerp(initialPos, targetPos, elapsedTime / rotateViewTime);
            elapsedTime += Time.deltaTime * rotateSpeed;
            yield return null;
        }
        //camera_Offset.position
        initial.position = target.position;

    }
}

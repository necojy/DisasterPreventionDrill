using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElevorStairOption : MonoBehaviour
{
    public GameObject glowbox;
    public Transform rotatePosition1;
    public Transform rotatePosition2;
    public Transform xr_orig;
    public Transform camera_Offset;
    //public Quaternion targetRotation;
    //public Vector3 targetPos;//目標位置
    public float rotateSpeed = 1f;
    public float rotateViewTime = 1f;
    public Camera mainCamera;
    public Camera optionCamera;


    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            optionCamera.enabled = true;
            optionCamera.transform.position = mainCamera.transform.position;
            optionCamera.transform.rotation = mainCamera.transform.rotation;
            mainCamera.enabled = false;


            StartCoroutine(MovePlayer(optionCamera.transform, rotatePosition1, xr_orig));
            StartCoroutine(Wait2Sec());

            //optionCamera.enabled = false;
            //mainCamera.enabled = true;
        }
    }
    IEnumerator Wait2Sec()
    {
        yield return new WaitForSeconds(2f);
    }
    IEnumerator MovePlayer(Transform initial, Transform target, Transform playerPos)
    {

        //初始角度、目標角度
        Quaternion initialRotation = initial.rotation;
        Quaternion targetRotation = target.rotation;
        //初始位置、目標位置
        Vector3 initialPos = initial.position;
        Vector3 targetPos = target.position;
        float elapsedTime = 0f;
        while (elapsedTime < rotateViewTime)
        {
            optionCamera.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotateViewTime);
            //camera_Offset.position = Vector3.Lerp(targetPos, initialPos, elapsedTime / rotateViewTime);
            optionCamera.transform.position = Vector3.Lerp(initialPos, targetPos, elapsedTime / rotateViewTime);
            elapsedTime += Time.deltaTime * rotateSpeed;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(MovePlayer2(optionCamera.transform, rotatePosition2, xr_orig));


        //glowbox.SetActive(false);

    }
    IEnumerator MovePlayer2(Transform initial, Transform target, Transform playerPos)
    {

        //初始角度、目標角度
        Quaternion initialRotation = initial.rotation;
        Quaternion targetRotation = target.rotation;
        //初始位置、目標位置
        Vector3 initialPos = initial.position;
        Vector3 targetPos = target.position;
        float elapsedTime = 0f;
        while (elapsedTime < rotateViewTime)
        {
            optionCamera.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotateViewTime);
            //camera_Offset.position = Vector3.Lerp(targetPos, initialPos, elapsedTime / rotateViewTime);
            optionCamera.transform.position = Vector3.Lerp(initialPos, targetPos, elapsedTime / rotateViewTime);
            elapsedTime += Time.deltaTime * rotateSpeed;
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        optionCamera.enabled = false;
        mainCamera.enabled = true;



        //glowbox.SetActive(false);

    }
}

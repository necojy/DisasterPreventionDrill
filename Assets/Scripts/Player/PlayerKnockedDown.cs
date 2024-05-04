using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockedDown : MonoBehaviour
{
    public GameObject head;
    public Transform head2;
    public float spawnDistance = 2;
    public GameObject menu;
    private bool isRotating = false;

    private void Update()
    {
        if (isRotating)
        {
            menu.SetActive(true);
            menu.transform.position = head2.position + new Vector3(head2.forward.x, head2.forward.y, head2.forward.z).normalized * spawnDistance;
            menu.transform.LookAt(new Vector3(head2.position.x,-1 *  menu.transform.position.y,-1 *  head2.position.z));
            menu.transform.rotation = Quaternion.Euler(menu.transform.rotation.eulerAngles.x -10f, menu.transform.rotation.eulerAngles.y, menu.transform.eulerAngles.z);

            // menu.transform.forward *= -1;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Faling") && !isRotating)
        {
            StartCoroutine(RotateHead());
        }
    }

    private IEnumerator RotateHead()
    {

        Quaternion targetRotation = Quaternion.Euler(head.transform.rotation.eulerAngles.x - 90f, head.transform.rotation.eulerAngles.y, head.transform.rotation.eulerAngles.z);
        float duration = 1f; // 旋轉的時間

        float elapsedTime = 0f;
        Quaternion startRotation = head.transform.rotation;

        while (elapsedTime < duration)
        {
            head.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        head.transform.rotation = targetRotation;

        isRotating = true;
    }
}

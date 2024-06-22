using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorShake : MonoBehaviour
{
    public GameObject elevator;
    public Animator anim;
    public GameObject light;
    private ElevatorArea elevatorArea;

    void Start()
    {
        elevator = GameObject.Find("Elevator");
        anim = elevator.GetComponent<Animator>();
        light = GameObject.Find("elevatorLight");
        elevatorArea = GameObject.Find("elevatorArea").GetComponent<ElevatorArea>();
    }

    public void OnCloseAnimationEnd()
    {
        if (elevatorArea.inElevator)
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake(){
        Debug.Log("Elevator broken");
        anim.SetBool("isShaking",true);
        yield return new WaitForSeconds(5);
        anim.SetBool("isShaking",false);
        light.SetActive(false);
    }

}

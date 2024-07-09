using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorShake : MonoBehaviour
{
    public GameObject elevator;
    public Animator anim;
    private ElevatorArea elevatorArea;
    private ShowDeadCanvas showDeadCanvas;


    void Start()
    {
        elevator = GameObject.Find("Elevator");
        anim = elevator.GetComponent<Animator>();
        elevatorArea = GameObject.Find("elevatorArea").GetComponent<ElevatorArea>();
        showDeadCanvas = GameObject.Find("player deadth control").GetComponent<ShowDeadCanvas>();

    }

    public void OnCloseAnimationEnd()
    {
        if (elevatorArea.inElevator)
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        Debug.Log("Elevator broken");
        anim.SetBool("isShaking", true);
        AudioManager.instance.PlayItemSound("mechanical-25592");

        yield return new WaitForSeconds(7f);
        showDeadCanvas.deadReason = "被困於電梯";
        showDeadCanvas.reloadScene = "Corridor";
        StartCoroutine(showDeadCanvas.ShowDeadCanva());
    }

}

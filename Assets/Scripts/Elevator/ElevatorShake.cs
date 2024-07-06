using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorShake : MonoBehaviour
{
    public GameObject elevator;
    public Animator anim;
    public GameObject light;
    private ElevatorArea elevatorArea;
    private ShowDeadCanvas showDeadCanvas;


    void Start()
    {
        elevator = GameObject.Find("Elevator");
        anim = elevator.GetComponent<Animator>();
        light = GameObject.Find("elevatorLight");
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
        yield return new WaitForSeconds(0.1f);
        //anim.SetBool("isShaking",false);
        light.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        showDeadCanvas.deadReason = "被掉落物砸死";
        showDeadCanvas.reloadScene = "Corridor";
        StartCoroutine(showDeadCanvas.ShowDeadCanva());
    }

}

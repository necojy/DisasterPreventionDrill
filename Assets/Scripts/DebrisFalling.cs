using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisFalling : MonoBehaviour
{
    private ShowCaption showCaption;
    private bool isFalling = false;
    private bool isHint = false;
    public GameObject left_door;
    private Pulldoor pulldoor;
    public GameObject[] fallObjects;
    public Animator fade_animator;
    public GameObject deadPanel;
    private GameObject SelectRoadHint;


    private void Start()
    {
        showCaption = GameObject.Find("ScreenText").GetComponent<ShowCaption>();
        SelectRoadHint = GameObject.Find("SelectRoad");
        pulldoor = left_door.GetComponent<Pulldoor>();
        Init();
    }

    // 控制物體先不掉落 
    private void Init()
    {
        foreach (GameObject fallObject in fallObjects)
        {
            fallObject.GetComponent<Rigidbody>().useGravity = false;
        }

        fallObjects[0].SetActive(false);
    }

    private void Update()
    {
        if (pulldoor.isOpening && !isHint)
        {
            StartHint();
        }

        // 加上背包判定
        // if(isFalling && 包包沒背)
    }

    //開門時觸發提示
    private void StartHint()
    {
        isHint = true;
        fallObjects[0].SetActive(true);
        // StartCoroutine(FallObjects(0, 3));
        OpenScreenText();
    }

    //觸碰後開始事件
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StartFalling());
        }
    }

    //開啟字幕
    public void OpenScreenText()
    {
        // 設定字幕內容
        showCaption.ChangeCaptionContent("好像有東西掉下來了");

        // 開啟字幕
        StartCoroutine(showCaption.ShowWords());
    }

    private IEnumerator FallObjects(int start_index, int end_index)
    {
        isFalling = true;

        for (int i = start_index; i < end_index; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.3f, 0.6f));
            fallObjects[i].GetComponent<Rigidbody>().useGravity = true;
        }

        isFalling = false;

    }

    private void ShowDeadCanvas()
    {
        fade_animator.SetBool("fadein", true);
        //要等1s
        // deadPanel.SetActive(true);

        // StartCoroutine(DeadRecip());
    }

    private IEnumerator StartFalling()
    {
        yield return StartCoroutine(FallObjects(1, 7));
        Destroy(gameObject);
    }
}

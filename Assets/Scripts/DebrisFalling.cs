using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisFalling : MonoBehaviour
{
    private ShowCaption showCaption;
    private bool isFalling = false;
    public GameObject[] fallObjects;
    public Animator fade_animator;
    public GameObject deadPanel;


    private void Start()
    {
        showCaption = GameObject.Find("ScreenText").GetComponent<ShowCaption>();
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
        StartHint();
    }

    private void Update()
    {
        // 加上背包判定
        // if (isFalling)
        // {
        //     ShowDeadCanvas();
        // }
    }

    //開門時觸發提示
    private void StartHint()
    {
        fallObjects[0].SetActive(true);
        // AudioManager.instance.PlayItemSound("rock slide");
        // StartCoroutine(FallObjects(0, 3));
        OpenScreenText();

    }

    //觸碰後開始事件
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FallObjects(1, 12));
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
}

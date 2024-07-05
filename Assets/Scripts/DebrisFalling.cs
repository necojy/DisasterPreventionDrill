using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DebrisFalling : MonoBehaviour
{
    private ShowCaption showCaption;
    private bool isFalling = false;
    public GameObject[] fallObjects;
    public SelectRodaHint selectRoadHint;
    private ShowDeadCanvas showDeadCanvas;

    #region 包包判定
    private BagLeftTop Bleft;
    private BagRightBottom Bright;
    #endregion
    private void Start()
    {
        showCaption = GameObject.Find("ScreenText").GetComponent<ShowCaption>();
        Bleft = GameObject.Find("Bleft").GetComponent<BagLeftTop>();
        Bright = GameObject.Find("Bright").GetComponent<BagRightBottom>();
        showDeadCanvas = GameObject.Find("player deadth control").GetComponent<ShowDeadCanvas>();
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
        // if (pulldoor.isOpening && !isHint)
        // {
        //     StartHint();
        // }

        // 加上背包判定
        if (isFalling && (!Bleft.isPuting || !Bright.isPuting))
        {
            showDeadCanvas.deadReason = "被掉落物砸死";
            StartCoroutine(showDeadCanvas.ShowDeadCanva());
        }
    }

    //開門時觸發提示
    public void StartHint()
    {
        fallObjects[0].SetActive(true);
        OpenScreenText();
    }

    //觸碰後開始事件
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFalling)
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

        for (int i = start_index; i < end_index; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.3f, 0.6f));
            fallObjects[i].GetComponent<Rigidbody>().useGravity = true;
        }

        yield return new WaitForSeconds(1f);
        isFalling = true;

        yield return new WaitForSeconds(4f);
        isFalling = false;

        if (!showDeadCanvas.isDead)
        {
            StartCoroutine(selectRoadHint.StartHint());
        }

    }

    private IEnumerator StartFalling()
    {
        yield return StartCoroutine(FallObjects(1, 7));
        // yield return new WaitForSeconds(1f);
        // Destroy(gameObject);
    }
}

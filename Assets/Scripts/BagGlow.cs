using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagGlow : MonoBehaviour
{
    public GameObject[] hint_bag;
    public int hint_start,hint_end;
    public GameObject glowbox;

    // private void start()
    // {
    //     Skode_Glinting skode_Glinting = hint_bag.GetComponent<Skode_Glinting>();
    //     skode_Glinting.StartGlinting();
    // }

    public void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player"))
        {
        StartCoroutine(StartHint());
        }
    }

    public IEnumerator StartHint(){
        Hint_Glow(true,hint_start,hint_end);
        yield return new WaitForSeconds(3f);
        Hint_Glow(false,hint_start,hint_end);
        glowbox.SetActive(false);
    }

    private void Hint_Glow(bool is_open,int start, int end)
    {
        for(int i=start; i<=end; i++)
        {
            if(hint_bag[i]!=null)
            {
                Skode_Glinting skode_Glinting = hint_bag[i].GetComponent<Skode_Glinting>();
                if (skode_Glinting != null){
                    if (is_open) skode_Glinting.StartGlinting();
                    
                    else skode_Glinting.StopGlinting();
                }
            }
        }
    }
 
}

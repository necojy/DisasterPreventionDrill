using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull_handle : MonoBehaviour
{
    public Animator anim;
    public GameObject cub;
    
    void Start()
    {
        anim = anim.GetComponent<Animator>();
    }

    public void Pullhandle(){
        anim.SetTrigger("open");
        cub.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull_handle : MonoBehaviour
{
    public Animator anim;
    public GameObject cub;
    // Start is called before the first frame update
    void Start()
    {
        anim = anim.GetComponent<Animator>();
    }

    public void Pullhandle(){
        anim.SetTrigger("open");
        cub.SetActive(false);
    }
}

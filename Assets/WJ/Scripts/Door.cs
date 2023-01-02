using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Open()
    {

        animator.SetBool("IsOpen",true);

    }
    public void Close()
    {
        animator.SetBool("IsOpen", false);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimations : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Car animations
        anim.SetFloat("verticalInput", Input.GetAxisRaw("Vertical"));
        if (Input.GetAxisRaw("Vertical") != 0) { }
        anim.SetFloat("horizontalInput", Input.GetAxisRaw("Horizontal"));
    }
}

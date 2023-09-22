using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            animator.SetBool("Scoped", true);
        }

        else
        {
            animator.SetBool("Scoped", false);
        }
    }


}

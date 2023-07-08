using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : ButtonExtension
{
    public float call_time;
    public CustomB customB;
    public Animator animator;

    public void Set_Custom(CustomB custom)
    {
        customB = custom;
    }
    // Update is called once per frame
    void Update()
    {
        if (call_time >=0f)
        {
            call_time -= Time.deltaTime;
        }
        else
        {
            animator.SetTrigger("fake");
        }
    }

    public void Set_Anger()
    {
        customB.Anger();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        animator.SetTrigger("hit");
    }

    public void Dsy()
    {
        Destroy(gameObject);
    }
}

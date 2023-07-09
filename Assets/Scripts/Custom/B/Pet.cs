using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : ButtonExtension
{
    public float call_time;
    public CustomB customB;
    public Animator animator;
    private bool if_called = false;

    public void Set_Custom(CustomB custom)
    {
        customB = custom;
    }
    // Update is called once per frame
    void Update()
    {
        if (if_called)
        {
            return;
        }
        if (call_time >=0f)
        {
            call_time -= Time.deltaTime;
            Check_Player();
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }

    public void Set_Anger()
    {
        customB.animator.SetBool("Left",false);
        customB.animator.SetBool("Right",false);
        customB.animator.SetTrigger("Idle");
        customB.animator.SetTrigger("Anger");
        if_called = true;
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

    private void Check_Player()
    {
        //ÕÊº“√ª”–∆€’©
        if (customB.player.LeftHandStatus == HandStatus.Nothing && customB.player.RightHandStatus == HandStatus.Nothing)
        {
            return;
        }
        Set_Anger();
    }
}

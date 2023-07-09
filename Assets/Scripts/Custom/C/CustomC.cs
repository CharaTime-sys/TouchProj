using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomC : Custom
{
    public float slow_speed;

    protected override void Start()
    {
        base.Start();
    }

    public void Set_Timer(bool slow)
    {
        if (slow)
        {
            MagicManager.instance.MagicTimer /= slow_speed;
        }
        else
        {
            MagicManager.instance.MagicTimer *= slow_speed;
        }
    }
}

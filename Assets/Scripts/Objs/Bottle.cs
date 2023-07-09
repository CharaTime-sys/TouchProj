using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : ButtonExtension
{
    public Transform target_pos;
    public bool enabled;
    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        if (LevelController.Instance.custom.State == Custom_State.Look)
        {
            transform.position = target_pos.localPosition - new Vector3(0, 0, 2f);
            enabled = true;
        }
        else
        {
            LevelController.Instance.custom.Anger();
        }
    }
}

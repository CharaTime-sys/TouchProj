using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ButtonExtension
{
    [Header("���һ�������")]
    public Look_Type type;
    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        LevelController.Instance.custom.Look(type);
    }
}

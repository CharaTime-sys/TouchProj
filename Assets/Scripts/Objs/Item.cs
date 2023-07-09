using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ButtonExtension
{
    [Header("◊Û”“ªıº‹¿‡–Õ")]
    public Look_Type type;
    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        if (LevelController.Instance.custom.State == Custom_State.Idle)
        {
            LevelController.Instance.custom.Look(type);
            transform.GetChild(0).gameObject.SetActive(true);
            Invoke(nameof(Set_Light), 3f);
        }
    }

    private void Set_Light()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}

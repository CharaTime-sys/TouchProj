using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab_Stone : ButtonExtension
{
    private bool is_Grab;
    private GameObject stone;

    protected override void Update()
    {
        base.Update();
        if (is_Grab)
        {
            Vector3 mousepos = Input.mousePosition;
            stone.transform.position = Camera.main.ScreenToWorldPoint(mousepos)+new Vector3(0,0, 9f);
        }
    }

    private void OnMouseDrag()
    {
        if (type == Button_Press_Type.Long && !is_Grab)
        {
            CursorController.instance.isCatchingStone = true;
            is_Grab = true;
            LevelController.Instance.custom.GetComponent<CustomC>().Set_Timer(false);
            GameObject.Find("Player").GetComponent<PlayerHandInfo>().isGrab = true;
            stone = Instantiate(gameObject);
            stone.GetComponent<Grab_Stone>().enabled = false;
        }
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        if (is_Grab)
        {
            LevelController.Instance.custom.GetComponent<CustomC>().Set_Timer(true);
            Destroy(stone);
        }
        GameObject.Find("Player").GetComponent<PlayerHandInfo>().isGrab = false;
        is_Grab = false;
    }
}

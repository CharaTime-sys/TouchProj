using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController instance;
    public bool isCatchingStone = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void FixAndPlay()
    {
        //固定到动画的那支手上的位置放一个动画
    }

    private void Update()
    {

    }
}

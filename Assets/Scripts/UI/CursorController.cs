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
        //�̶�����������֧���ϵ�λ�÷�һ������
    }

    private void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum HandStatus
{
    Nothing,
    PickStone,
    Magic
}

public class PlayerHandInfo : MonoBehaviour
{
    public GameObject LeftHandStone;
    public GameObject RightHandStone;

    public HandStatus LeftHandStatus = HandStatus.Nothing;
    public HandStatus RightHandStatus = HandStatus.Nothing;

    public void LeftHandPick(GameObject pickObj)
    {
        if (LeftHandStatus == HandStatus.Magic)
            return;
        else if (LeftHandStatus == HandStatus.PickStone)
        {
            if(pickObj.GetComponent<Stone>().stype == LeftHandStone.GetComponent<Stone>().stype)
            {
                //清空
                ClearLeftHand();
                
            }
            else
            {
                //清空
                ClearLeftHand();
                //模型添加
                AddLeftHand(pickObj);
            }
        }
        else if(LeftHandStatus == HandStatus.Nothing)
        {
            LeftHandStatus = HandStatus.PickStone;
            //模型添加
            AddLeftHand(pickObj);
        }
    }

    public void ClearLeftHand()
    {
        if (LeftHandStone == null)
            return;
        LeftHandStatus = HandStatus.Nothing;
        LeftHandStone.GetComponent<Stone>().isPicked = false;
        EventManager.instance.PlayerStoneOff(1);
        EventManager.instance.PlayerStoneOff(3);
        LeftHandStone = null;
    }

    public void AddLeftHand( GameObject addObj )
    {
        if (LeftHandStone != null)
            return;
        LeftHandStone = addObj;
        LeftHandStone.GetComponent<Stone>().isPicked = true;
        if(LeftHandStone.GetComponent<Stone>().stype == StoneType.L1 )
        {
            var go = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Props/Stone1.prefab", typeof(GameObject)) as GameObject;
            EventManager.instance.PlayerStoneOn(1, go);
            EventManager.instance.PlayerStoneOn(3, go);
        }
        else if(LeftHandStone.GetComponent<Stone>().stype == StoneType.L2)
        {
            var go = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Props/Stone2.prefab", typeof(GameObject)) as GameObject;
            EventManager.instance.PlayerStoneOn(1, go);
            EventManager.instance.PlayerStoneOn(3, go);
        }
    }
}

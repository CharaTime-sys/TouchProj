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

    public bool isKeyDDown = false;
    public bool isKeyJDown = false;

    public float LTimeStart = 0;
    public float LTimeEnd = 0;
    public float RTimeStart = 0;
    public float RTimeEnd = 0;

    public float longpresstime = 0.5f;

    public bool isLROn = false;

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

    public void RightHandPick(GameObject pickObj)
    {
        if (RightHandStatus == HandStatus.Magic)
            return;
        else if (RightHandStatus == HandStatus.PickStone)
        {
            if (pickObj.GetComponent<Stone>().stype == RightHandStone.GetComponent<Stone>().stype)
            {
                //清空
                ClearRightHand();

            }
            else
            {
                //清空
                ClearRightHand();
                //模型添加
                AddRightHand(pickObj);
            }
        }
        else if (RightHandStatus == HandStatus.Nothing)
        {
            RightHandStatus = HandStatus.PickStone;
            //模型添加
            AddRightHand(pickObj);
        }
    }

    public void ClearRightHand()
    {
        if (RightHandStone == null)
            return;
        RightHandStatus = HandStatus.Nothing;
        RightHandStone.GetComponent<Stone>().isPicked = false;
        EventManager.instance.PlayerStoneOff(1);
        EventManager.instance.PlayerStoneOff(3);
        RightHandStone = null;
    }

    public void AddRightHand(GameObject addObj)
    {
        if (RightHandStone != null)
            return;
        RightHandStone = addObj;
        RightHandStone.GetComponent<Stone>().isPicked = true;
        if (RightHandStone.GetComponent<Stone>().stype == StoneType.R1)
        {
            var go = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Props/Stone3.prefab", typeof(GameObject)) as GameObject;
            EventManager.instance.PlayerStoneOn(2, go);
            EventManager.instance.PlayerStoneOn(4, go);
        }
        else if (RightHandStone.GetComponent<Stone>().stype == StoneType.R2)
        {
            var go = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Props/Stone4s.prefab", typeof(GameObject)) as GameObject;
            EventManager.instance.PlayerStoneOn(2, go);
            EventManager.instance.PlayerStoneOn(4, go);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!isKeyDDown)
            {
                isKeyDDown = true;
                LTimeStart = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!isKeyJDown)
            {
                isKeyJDown = true;
                RTimeStart = Time.time;
            }
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            isKeyJDown = false;
            RTimeEnd = Time.time;
            if (RTimeEnd - RTimeStart > longpresstime)
            {
                LTimeStart = 0;
                LTimeEnd = 0;
                RTimeStart = 0;
                RTimeEnd = 0;
                StartCoroutine(LRFlag());
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            isKeyDDown = false;
            LTimeEnd = Time.time;
            if (LTimeEnd - LTimeStart > longpresstime)
            {
                LTimeStart = 0;
                LTimeEnd = 0;
                RTimeStart = 0;
                RTimeEnd = 0;
                StopAllCoroutines();
                StartCoroutine(LRFlag());
            }
        }
        if (!isLROn)
        {
            InputListener();
        }
    }

    IEnumerator LRFlag()
    {
        isLROn = true;        
        yield return new WaitForSecondsRealtime(1.0f);
        isLROn = false;
    }

    public void InputListener()
    {
        //短按D
        if ((LTimeEnd - LTimeStart) < longpresstime && (LTimeEnd > LTimeStart))
        {
            if(RTimeStart == 0)
                Debug.Log("D d");
            LTimeStart = 0;
            LTimeEnd = 0;
        }
        //短按J
        if ((RTimeEnd - RTimeStart) < longpresstime && (RTimeEnd > RTimeStart))
        {
            if (LTimeStart == 0)
                Debug.Log("R d");
            RTimeStart = 0;
            RTimeEnd = 0;
        }
        //长按D和J
        if(((Time.time - LTimeStart) > longpresstime) && ((Time.time - RTimeStart) > longpresstime)&&
            isKeyDDown && isKeyJDown)
        {
            Debug.Log("D R l");
        }
        //长按D
        if ((Time.time - LTimeStart) > longpresstime && !(RTimeStart > 0) &&
            isKeyDDown)
        {
            Debug.Log("D l");
        }
        //长按J
        if ((Time.time - RTimeStart) > longpresstime && !(LTimeStart > 0) && 
            isKeyJDown)
        {
            Debug.Log("R l");
        }

    }
}

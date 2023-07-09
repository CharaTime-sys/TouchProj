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
    public bool isMouse2Down = false;

    public float LTimeStart = 0;
    public float LTimeEnd = 0;
    public float RTimeStart = 0;
    public float RTimeEnd = 0;
    public float MTimeStart = 0;
    public float MTimeEnd = 0;

    public float longpresstime = 0.5f;

    public float MagicTimer = 3.0f;
    public float StartMagicTime = 0.0f;

    public bool isLROn = false;

    public bool isGrab = false;

    //药水相关
    public Bottle bottle;
    public bool if_create;
    public GameObject fx_preb;
    public GameObject fx_obj;
    public Transform fx_pos;
    public bool if_summon;

    private void Start()
    {
        fx_preb = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Objs/FX_Gem.prefab", typeof(GameObject)) as GameObject;
    }

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
        EventManager.instance.PlayerStoneOff(2);
        EventManager.instance.PlayerStoneOff(4);
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
            var go = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Props/Stone4.prefab", typeof(GameObject)) as GameObject;
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
        if (Input.GetMouseButtonDown(1) && CursorController.instance.isCatchingStone)
        {
            if(!isMouse2Down)
            {
                isMouse2Down = true;
                MTimeStart = Time.time;
            }
        }
        if ((Input.GetMouseButtonUp(1) && CursorController.instance.isCatchingStone) || !CursorController.instance.isCatchingStone)
        {
            isMouse2Down = false;
            MTimeEnd = Time.time;
            EndMagic();
        }

        if (!isLROn)
        {
            InputListener();
        }

        //合成
        if (bottle!=null)
        {
            if_create = bottle.enabled && (MagicManager.instance.Gem_Show != null);
        }
        if (if_create && Input.GetKey(KeyCode.S)&& Input.GetKey(KeyCode.K))
        {
            Debug.Log("合成！");
            LevelController.Instance.Game_Next();
        }
    }

    IEnumerator LRFlag()
    {
        isLROn = true;        
        yield return new WaitForSecondsRealtime(0.1f);
        isLROn = false;
        EndMagic();
    }

    public void InputListener()
    {
        //短按D
        if ((LTimeEnd - LTimeStart) < longpresstime && (LTimeEnd > LTimeStart))
        {
            if(RTimeStart == 0)
            {
                LeftHandPick(StoneManager.instance.s2);
            }
            LTimeStart = 0;
            LTimeEnd = 0;
        }
        //短按J
        if ((RTimeEnd - RTimeStart) < longpresstime && (RTimeEnd > RTimeStart))
        {
            if (LTimeStart == 0)
            {
                RightHandPick(StoneManager.instance.s3);
            }
            RTimeStart = 0;
            RTimeEnd = 0;
        }
        //长按D和J
        if(((Time.time - LTimeStart) > longpresstime) && ((Time.time - RTimeStart) > longpresstime)&&
            isKeyDDown && isKeyJDown)
        {
            if(LeftHandStatus == HandStatus.PickStone && RightHandStatus == HandStatus.PickStone)
                UseMagic(MagicType.Type3);
        }
        //长按D
        if ((Time.time - LTimeStart) > longpresstime && !(RTimeStart > 0) &&
            isKeyDDown)
        {
            if (LeftHandStatus == HandStatus.PickStone)
                UseMagic(MagicType.Type1);
        }
        //长按J
        if ((Time.time - RTimeStart) > longpresstime && !(LTimeStart > 0) && 
            isKeyJDown)
        {
            if (RightHandStatus == HandStatus.PickStone)
                UseMagic(MagicType.Type2);
        }

        //长按鼠标
        if(((Time.time - MTimeStart) > longpresstime) && isMouse2Down)
        {
            MagicType type = GameObject.Find("GuestBornPos").transform.GetChild(0).GetComponent<Custom>().gem_type;
            UseMagic2(type);
            LeftHandPick(StoneManager.instance.s2);
        }

    }

    //使用魔法
    public void UseMagic(MagicType mtIn)
    {
        if(transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("IDLE"))
        {
            transform.GetComponent<Animator>().SetTrigger("MakeTrigger");
            MagicManager.instance.MagicStart(mtIn);
            Invoke(nameof(Summon_Magic), MagicTimer - 2.5f);
        }
    }

    public void UseMagic2(MagicType mtIn)
    {
        if (transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("IDLE"))
        {
            transform.GetComponent<Animator>().SetTrigger("MakeTrigger2");
            CursorController.instance.FixAndPlay();
            MagicManager.instance.MagicStart(mtIn);
            if (!if_summon)
            {
                Invoke(nameof(Summon_Magic), MagicTimer - 2.5f);
                if_summon = true;
            }
        }
    }

    public void EndMagic()
    {
        transform.GetComponent<Animator>().SetTrigger("BackTrigger");
        MagicManager.instance.MagicStop();
    }

    public void Summon_Magic()
    {
        fx_obj = Instantiate(fx_preb, new Vector3(-0.42f,-1f,0f),Quaternion.identity);
        Destroy(fx_obj, 2.6f);
        Invoke(nameof(Set_Summon), 2.6f);
    }

    public void Set_Summon()
    {
        if_summon = false;
    }
}

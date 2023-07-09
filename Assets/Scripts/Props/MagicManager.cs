using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum MagicType
{
    Type1,
    Type2,
    Type3,
    Type4
};
public class MagicManager : MonoBehaviour
{
    public static MagicManager instance;

    public float MagicTimer = 3.0f;

    public bool MagicOn = false;

    public float MagicStartTime = 0;

    public GameObject Player;

    public MagicType mt = MagicType.Type1;

    public GameObject GemPos;
    public GameObject Gem_1;
    public GameObject Gem_2;
    public GameObject Gem_3;
    public GameObject Gem_4;

    public GameObject Gem_Show = null;

    public GameObject Gem_FX;

    public GameObject MagicProgressUI;
    public Image MagicProgress;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if(MagicOn)
        {
            MagicProgress.fillAmount = (Time.time - MagicStartTime) / MagicTimer;
            if(Time.time - MagicStartTime > MagicTimer)
            {
                Player.GetComponent<Animator>().SetTrigger("EndTrigger");
                MagicStop();
                SummonGem();

                if(Level1_1_Chat.instance != null)
                {
                    Level1_1_Chat.instance.TipsTools();
                }
            }
        }
    }

    public void MagicStart(MagicType mtIn)
    {
        MagicOn = true;
        MagicStartTime = Time.time;
        mt = mtIn;
        MagicProgressUI.SetActive(true);
        MagicProgress.fillAmount = 0;
    }

    public void MagicStop()
    {
        MagicOn = false;
        MagicStartTime = 0;
        Player.GetComponent<PlayerHandInfo>().ClearLeftHand();
        Player.GetComponent<PlayerHandInfo>().ClearRightHand();
        MagicProgressUI.SetActive(false);
        MagicProgress.fillAmount = 0;
    }

    public void SummonGem()
    {
        Instantiate(Gem_FX, GemPos.transform);
        if(mt == MagicType.Type1)
        {
            Destroy(Gem_Show);
            var go = Instantiate(Gem_1, GemPos.transform);
            Gem_Show = go;
        }
        else if(mt == MagicType.Type2)
        {
            Destroy(Gem_Show);
            var go = Instantiate(Gem_2, GemPos.transform);
            Gem_Show = go;
        }
        else if (mt == MagicType.Type3)
        {
            Destroy(Gem_Show);
            var go = Instantiate(Gem_3, GemPos.transform);
            Gem_Show = go;
        }
        else if (mt == MagicType.Type4)
        {
            Destroy(Gem_Show);
            var go = Instantiate(Gem_4, GemPos.transform);
            Gem_Show = go;
        }
    }

}

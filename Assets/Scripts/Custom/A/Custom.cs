using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Look_Type
{
    Left,
    Right,
}

public enum Custom_State
{
    Idle,
    Look,
}

public class Custom : MonoBehaviour
{
    #region ʱ�����
    [Header("���ĵ�ʱ��ͷ�Χ")]
    [SerializeField] float look_time = 3;
    [SerializeField] float look_time_range = 2;
    [Header("�ջ�ע������ʱ��ͷ�Χ")]
    [SerializeField] float wait_time = 4;
    [SerializeField] float wait_time_range = 1;
    public float look_timer;
    public float wait_timer;
    #endregion

    #region ���
    public Animator animator;
    Custom_State state;
    System.Action idle_action;
    System.Action look_action;
    public PlayerHandInfo player;
    GameObject cur_gem;
    #endregion

    [Header("��Ҫ�ı�ʯ")]
    public MagicType gem_type;
    [Header("�������")]
    public int require_num;

    public Custom_State State { get => state;}

    /// <summary>
    /// ������Ϊ
    /// </summary>
    /// <param name="idle_action"></param>
    /// <param name="look_action"></param>
    public void Set_Action(System.Action idle_action, System.Action look_action)
    {
        this.idle_action = idle_action;
        this.look_action = look_action;
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerHandInfo>();
        Set_LookTimer();
        Set_WaitTimer();
        Init_Gem();
    }

    // Update is called once per frame
    void Update()
    {
        Random_IdleAndLook();
        Check_Player();
        Check_Gem();
    }

    #region ��ʼ�����
    protected virtual void Init_Gem()
    {
        string path = "Assets/Prefabs/Props/Gem_";
        if (GameObject.Find("Level1_2_Mark"))
        {
            path += "4.prefab";
            gem_type = MagicType.Type4;
        }
        else
        {
            switch (Get_RandomGemType())
            {
                case MagicType.Type1:
                    path += "1.prefab";
                    gem_type = MagicType.Type1;
                    break;
                case MagicType.Type2:
                    path += "2.prefab";
                    gem_type = MagicType.Type2;
                    break;
                case MagicType.Type3:
                    path += "3.prefab";
                    gem_type = MagicType.Type3;
                    break;
                default:
                    break;
            }
        }
        GameObject target_obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
        target_obj = Instantiate(target_obj, transform.GetChild(0).transform);
        target_obj.transform.localScale = new Vector3(2.23f,2.23f,2.23f);
        target_obj.transform.localPosition = new Vector3(0.05f, 0.42f, 0);

        cur_gem = target_obj;
    }
    #endregion

    #region Update�¼�
    /// <summary>
    /// �����ɢע����
    /// </summary>
    protected virtual void Random_IdleAndLook()
    {
        switch (state)
        {
            case Custom_State.Idle:
                if (look_timer >=0f)
                {
                    look_timer -= Time.deltaTime;
                }
                else
                {
                    Look(Get_RandomLookType());
                }
                break;
            case Custom_State.Look:
                if (wait_timer >= 0f)
                {
                    wait_timer -= Time.deltaTime;
                }
                else
                {
                    Idle();
                }
                break;
            default:
                break;
        }
    }

    protected virtual void Check_Player()
    {
        //���û����թ
        if (player.LeftHandStatus == HandStatus.Nothing && player.RightHandStatus == HandStatus.Nothing)
        {
            return;
        }
        else
        {
            //�˿�û����
            if (state == Custom_State.Look)
            {
                return;
            }
            animator.SetTrigger("Anger");
        }
    }

    protected virtual void Check_Gem()
    {
        if (MagicManager.instance.Gem_Show==null)
        {
            return;
        }

        if (MagicManager.instance.mt == gem_type)
        {
            require_num--;
            Destroy(cur_gem);
            Init_Gem();
        }
        Destroy(MagicManager.instance.Gem_Show);
        if (require_num <=0)
        {
            LevelController.Instance.Game_Next();
        }
    }
    #endregion

    #region �������
    public Look_Type Get_RandomLookType()
    {
        Look_Type[] types = (Look_Type[])System.Enum.GetValues(typeof(Look_Type));
        return types[Random.Range(0, types.Length)];
    }
    public MagicType Get_RandomGemType()
    {
        MagicType[] types = (MagicType[])System.Enum.GetValues(typeof(MagicType));
        return types[Random.Range(0, types.Length - 1)];
    }
    #endregion

    #region ��ʱ���
    private void Set_LookTimer()
    {
        look_timer = look_time + Random.Range(-look_time_range, look_time_range);
    }

    private void Set_WaitTimer()
    {
        wait_timer = wait_time + Random.Range(-wait_time_range, wait_time_range);
    }
    #endregion

    #region �������
    public void Idle()
    {
        //���ö���״̬
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetTrigger("Idle");
        //���ÿͻ�״̬
        state = Custom_State.Idle;
        //����ʱ������Ӧ����
        Set_WaitTimer();
        if (idle_action == null)
        {
            return;
        }
        idle_action();
    }

    public void Anger()
    {
        //���ö���״̬
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetTrigger("Idle");
        animator.SetTrigger("Anger");
        //����
        LevelController.Instance.Dead();
    }

    public void Look(Look_Type type = Look_Type.Left)
    {
        switch (type)
        {
            case Look_Type.Left:
                animator.SetBool("Right", false);
                animator.SetBool("Left", true);
                break;
            case Look_Type.Right:
                animator.SetBool("Left", false);
                animator.SetBool("Right", true);
                break;
            default:
                break;
        }
        state = Custom_State.Look;
        Set_LookTimer();
        if (look_action == null)
        {
            return;
        }
        look_action();
    }
    #endregion
}

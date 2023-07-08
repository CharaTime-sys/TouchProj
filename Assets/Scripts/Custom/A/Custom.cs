using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #region 时间相关
    [Header("分心的时间和范围")]
    [SerializeField] float look_time = 3;
    [SerializeField] float look_time_range = 2;
    [Header("收回注意力的时间和范围")]
    [SerializeField] float wait_time = 4;
    [SerializeField] float wait_time_range = 1;
    float look_timer;
    float wait_timer;
    #endregion
    #region 组件
    Animator animator;
    Custom_State state;
    System.Action idle_action;
    System.Action look_action;
    #endregion

    /// <summary>
    /// 设置行为
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
        Set_LookTimer();
        Set_WaitTimer();
    }

    // Update is called once per frame
    void Update()
    {
        Random_IdleAndLook();
    }

    /// <summary>
    /// 随机分散注意力
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

    public Look_Type Get_RandomLookType()
    {
        Look_Type[] types = (Look_Type[])System.Enum.GetValues(typeof(Look_Type));
        return types[Random.Range(0, types.Length)];
    }

    #region 计时相关
    private void Set_LookTimer()
    {
        look_timer = look_time + Random.Range(-look_time_range, look_time_range);
    }

    private void Set_WaitTimer()
    {
        wait_timer = wait_time + Random.Range(-wait_time_range, wait_time_range);
    }
    #endregion

    #region 动画相关
    public void Idle()
    {
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetTrigger("Idle");
        state = Custom_State.Idle;
        Set_WaitTimer();
        idle_action();
    }

    public void Anger()
    {
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetTrigger("Idle");
        animator.SetTrigger("Anger");
        UIController.Instance.Dead();
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
        look_action();
    }
    #endregion
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum Button_Press_Type
{
    Short,
    Long,
}

public class ButtonExtension : MonoBehaviour
{
    public float pressmaxTime = 1;

    public bool isDown = false;
    public bool isExit = false;
    private float downTime = 0f;

    protected Button_Press_Type type;

    protected virtual void Update()
    {
        //按下之后开始计时
        if (isDown)
        {
            downTime += Time.deltaTime;
        }
        if (downTime >= pressmaxTime)
        {
            type = Button_Press_Type.Long;
        }
    }

    protected virtual void OnMouseDown()
    {
        //设置按下状态
        isDown = true;
        //设置点击状态
        type = Button_Press_Type.Short;
    }

    protected virtual void OnMouseUp()
    {
        //设置抬起状态
        isDown = false;
        Press_Finished();
        downTime = 0;
    }

    private void OnMouseExit()
    {
        //如果不是在按下的时候鼠标移出去的话就不进行判断
        if (!isDown)
        {
            return;
        }
        Press_Finished();
        //设置变量
        isDown = false;
        isExit = true;
    }
    /// <summary>
    /// 点击完成后
    /// </summary>
    private void Press_Finished()
    {
        if (isExit)
        {
            isExit = false;
            return;
        }
        if (type == Button_Press_Type.Long)
        {
            Debug.Log(type + downTime.ToString());
        }
        else
        {
            Debug.Log(type + downTime.ToString());
        }
    }
}

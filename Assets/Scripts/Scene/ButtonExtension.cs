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
        //����֮��ʼ��ʱ
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
        //���ð���״̬
        isDown = true;
        //���õ��״̬
        type = Button_Press_Type.Short;
    }

    protected virtual void OnMouseUp()
    {
        //����̧��״̬
        isDown = false;
        Press_Finished();
        downTime = 0;
    }

    private void OnMouseExit()
    {
        //��������ڰ��µ�ʱ������Ƴ�ȥ�Ļ��Ͳ������ж�
        if (!isDown)
        {
            return;
        }
        Press_Finished();
        //���ñ���
        isDown = false;
        isExit = true;
    }
    /// <summary>
    /// �����ɺ�
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

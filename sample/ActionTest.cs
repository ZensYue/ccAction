using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccAction;
using System.Reflection;
using UnityEngine.Events;

public class TestReflection
{
	public float fillAmount { get; set; }
}

public class ActionTest : MonoBehaviour {
	public Image image;
	public Image image2;
	public Text text;
	public Outline outline;

    public GameObject buttonCon;

    Button[] buttons;

	public int reflection_count = 100;

	int m_count = 0;

	void Start () {
		image = transform.Find("image").GetComponent<Image>();
		image2 = transform.Find("image_2").GetComponent<Image>();
		text = transform.Find("txt").GetComponent<Text>();
		outline = transform.Find("txt").GetComponent<Outline>();

		Reset();


        buttons = new Button[buttonCon.transform.childCount];
        for (int i = 0; i < buttonCon.transform.childCount; i++)
        {
            buttons[i] = buttonCon.transform.GetChild(i).GetComponent<Button>();
        }


        AddButtonClick(0, OnClick1);
        AddButtonClick(1, OnClick2);
        AddButtonClick(2, OnClick3);
        AddButtonClick(3, OnClick4);
        AddButtonClick(4, OnClick5);
        AddButtonClick(5, OnClick6);
        AddButtonClick(6, OnClick7);
        AddButtonClick(7, OnClick8);
        AddButtonClick(8, OnClick9);
        AddButtonClick(9, OnClick10);
        AddButtonClick(10, OnClick11);
        AddButtonClick(11, OnClick12);
        AddButtonClick(12, OnClick13);
        AddButtonClick(13, OnClick14);
        AddButtonClick(14, OnClick15);
        AddButtonClick(15, OnClick16);
        AddButtonClick(16, OnClick17);
        AddButtonClick(17, OnClick18);
        AddButtonClick(18, OnClick19);
        AddButtonClick(19, OnClick20);
    }

    public void AddButtonClick(uint index, System.Action<Button> action)
    {
        if(index>= buttons.Length)
        {
            Debug.LogError($"button is less");
            return;
        }
        var button = buttons[index];
        button.onClick.AddListener(()=> {
            Reset();
            action.Invoke(button);
        });
    }

	
	void Reset()
	{
		text.text = "点击按钮测试";
		CCActionManager.Instance.RemoveAllActionsFromTarget(transform);
		image.transform.localPosition = Vector3.zero;
		image.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		image.transform.localRotation = new Quaternion(0, 0, 0, 0);
        image.color = Color.white;


        image2.transform.localPosition = new Vector3(0, -100, 0);

		//text.transform.localPosition = new Vector3(0, 77, 0);
		//text.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		//text.transform.localRotation = new Quaternion(0, 0, 0, 0);
		//text.color = new Color(1f, 1f, 1f, 1f);

		outline.effectColor = new Color(0f, 0f, 0f, 1.0f);
	}

    void SetTile(string title)
    {
        text.text = title;
    }

    void SetButtonText(Button button,string txt)
    {
        var child = button.transform.Find("btn_txt");
        if (!child) return;
        var btnText = child.GetComponent<Text>();
        if (!btnText) return;
        btnText.text = txt;
    }

    void OnClick1(Button button)
    {
        SetTile("移动返回");
        var action = CCActionSequence.Create(
            image.transform.CCMoveTo(1f, new Vector3(200, 100, 0)),
            image.transform.CCMoveTo(1f, new Vector3(0, 0, 0)),
            CCActionDelay.Create(0f)
        );
        CCActionManager.Instance.AddAction(action, transform);
    }

    void OnClick2(Button button)
    {
        SetTile("缩放3倍再缩放到1倍");
        var action = CCActionSequence.Create(
            image.transform.CCScaleTo(1f, new Vector3(3, 3, 3)),
            image.transform.CCScaleTo(1f, new Vector3(1, 1, 1)),
            CCActionDelay.Create(0f)
        );
        CCActionManager.Instance.AddAction(action, transform);
    }

    void OnClick3(Button button)
    {
        SetTile("旋转");
        var action = CCActionSequence.Create(
            image.transform.CCAnglesTo(1f, new Vector3(0, 180, 0)),
            image.transform.CCAnglesTo(1f, new Vector3(0,360,0)),
            CCActionDelay.Create(0f)
        );
        CCActionManager.Instance.AddAction(action, transform);
    }

    void OnClick4(Button button)
    {
        SetTile("二阶贝塞尔");
        var action = CCActionSequence.Create(
            image.transform.CCQuadBezierTo(2.0f, new Vector3(-200, 100, 0), new Vector3(400, 300, 0)),
            image.transform.CCMoveTo(1f, new Vector3(0, 0, 0)),
            CCActionDelay.Create(0f)
        );
        CCActionManager.Instance.AddAction(action, transform);
    }

    void OnClick5(Button button)
    {
        SetTile("三阶贝塞尔");
        var action = CCActionSequence.Create(
            image.transform.CCCubicBezierTo(2.0f, new Vector3(-200, 100, 0), new Vector3(600, -300, 0), new Vector3(400, 300, 0)),
            image.transform.CCMoveTo(1f, new Vector3(0, 0, 0)),
            CCActionDelay.Create(0f)
        );
        CCActionManager.Instance.AddAction(action, transform);
    }

    void OnClick6(Button button)
    {
        SetTile("椭圆运动1.5圈,复位");
        var action = CCActionSequence.Create(
            image.transform.CCEllipse2D(3.0f, new Vector3(0, 0, 0), 250, 100,0,1.5f),
            image.transform.CCMoveTo(1f, new Vector3(0, 0, 0)),
            CCActionDelay.Create(0f)
        );
        CCActionManager.Instance.AddAction(action, transform);
    }

    void OnClick7(Button button)
    {
        SetTile("移动重复3次");
        var action = CCActionSequence.Create(
            image.transform.CCMoveTo(1f, new Vector3(200, 100, 0)),
            image.transform.CCMoveTo(1f, new Vector3(0, 0, 0)),
            CCActionDelay.Create(0f)
        );
        var repectAction = CCActionRepeat.Create(action,3);
        CCActionManager.Instance.AddAction(repectAction, transform);
    }

    void OnClick8(Button button)
    {
        SetTile("移动缩放并行");
        var action1 = CCActionSequence.Create(
            image.transform.CCMoveTo(1f, new Vector3(200, 100, 0)),
            image.transform.CCMoveTo(1f, new Vector3(0, 0, 0)),
            CCActionDelay.Create(0f)
        );
        var action2 = CCActionSequence.Create(
            image.transform.CCScaleTo(1f, new Vector3(2, 2, 2)),
            image.transform.CCScaleTo(1f, new Vector3(1, 1, 1)),
            CCActionDelay.Create(0f)
        );
        var action = CCActionSpawn.Create(action1, action2);
        CCActionManager.Instance.AddAction(action, transform);
    }

    void OnClick9(Button button)
    {
        SetTile("闪烁1秒5次");
        CCActionManager.Instance.AddAction(image.gameObject.CCBlink(1.0f,5), transform);
    }

    void OnClick10(Button button)
    {
        SetTile("fillAmount");
        var action = CCActionSequence.Create(
            image.CCFillAmout(1.0f,0),
            CCActionDelay.Create(0.5f),
            image.CCFillAmout(1.0f,1),
            CCActionDelay.Create(0f)
        );
        CCActionManager.Instance.AddAction(action, transform);
    }

    void OnClick11(Button button)
    {
        SetTile("变色、淡入、淡出");
        var action = CCActionSequence.Create(
            image.CCColorTo(1.0f, Color.green),
            CCActionDelay.Create(0.5f),
            image.CCColorTo(1.0f, Color.white),
            CCActionDelay.Create(0.5f),
            image.CCFadeIn(1.0f),
            image.CCFadeOut(1.0f),
            CCActionDelay.Create(0f)
        );
        CCActionManager.Instance.AddAction(action, transform);

    }
    void OnClick12(Button button)
    {
        SetTile("跳数字");
        var action = CCActionSequence.Create(
            CCActionDelay.Create(0.5f),
            text.CCNumberTo(1.0f, "数字跳动，0-100，当前：{0:F2}",0,100, "数字跳动，0-100，当前：{0:F0}"),
            CCActionDelay.Create(0f)
        );
        CCActionManager.Instance.AddAction(action, transform);
    }
    void OnClick13(Button button)
    {
        SetTile("打印机");
        var action = CCActionSequence.Create(
            CCActionDelay.Create(0.5f),
            text.CCTextPrint(1.0f,"0123456789零一二三四五六七八九"),
            CCActionDelay.Create(0f)
        );
        CCActionManager.Instance.AddAction(action, transform);

    }
    void OnClick14(Button button)
    {
        SetTile("未完待续");
    }
    void OnClick15(Button button)
    {
        SetTile("未完待续");

    }

    void OnClick16(Button button)
    {
        SetTile("未完待续");

    }
    void OnClick17(Button button)
    {
        SetTile("未完待续");

    }

    void OnClick18(Button button)
    {
        SetTile("未完待续");

    }

    void OnClick19(Button button)
    {
        SetTile("未完待续");

    }

    void OnClick20(Button button)
    {
        SetTile("未完待续");

    }
}

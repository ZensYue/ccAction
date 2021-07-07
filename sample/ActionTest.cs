using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccAction;
using System.Reflection;

public class TestReflection
{
	public float fillAmount { get; set; }
}

public class ActionTest : MonoBehaviour {
	public Image image;
	public Image image2;
	public Button button;
	public Text btn_text;
	public Text text;
	public Outline outline;

	public int reflection_count = 100;

	int m_count = 0;

	void Start () {
		image = transform.Find("image").GetComponent<Image>();
		image2 = transform.Find("image_2").GetComponent<Image>();
		button = transform.Find("btn").GetComponent<Button>();
		btn_text = transform.Find("btn/btn_txt").GetComponent<Text>();
		text = transform.Find("txt").GetComponent<Text>();
		outline = transform.Find("txt").GetComponent<Outline>();
		button.onClick.AddListener(OnClick);

		Reset();
	}

	void TestReflection()
	{
		var obj = new TestReflection();
		UnityEngine.Profiling.Profiler.BeginSample("Reflection1");
		for (int i = 0; i < reflection_count; i++)
		{
			obj.fillAmount = 0.1f;
			obj.fillAmount = 1f;
		}
		UnityEngine.Profiling.Profiler.EndSample();

		UnityEngine.Profiling.Profiler.BeginSample("Reflection2");
		var ts = obj.GetType();
		var fi = ts.GetMethod("set_fillAmount");
		for (int i = 0; i < reflection_count; i++)
		{
			fi.Invoke(obj, new object[] { 0.1f });
			fi.Invoke(obj, new object[] { 1f });
		}
		UnityEngine.Profiling.Profiler.EndSample();
	}

	void OnClick()
	{
		m_count++;
		if (m_count % 2 == 1)
			CallFuncTest();
		else
			Reset();
	}
	
	void Reset()
	{
		text.text = "Hello World!";
		btn_text.text = "点击开始";
		CCActionManager.Instance.RemoveAllActionsFromTarget(transform);
		image.transform.localPosition = Vector3.zero;
		image.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		image.transform.localRotation = new Quaternion(0, 0, 0, 0);

		image2.transform.localPosition = new Vector3(0, -100, 0);

		text.transform.localPosition = new Vector3(0, 77, 0);
		text.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		text.transform.localRotation = new Quaternion(0, 0, 0, 0);
		text.color = new Color(1f, 1f, 1f, 1f);

		outline.effectColor = new Color(0f, 0f, 0f, 1.0f);
	}

	void CallBackO(System.Object o)
	{
		text.text = o.ToString();
	}

	void CallBackEnd()
	{
		m_count++;
		Reset();
	}

	void CallFuncTest()
	{

		var action = CCActionSequence.Create(
			image.transform.CCMoveTo(1f, new Vector3(200, 100, 0)),
			image.gameObject.CCHide(),
			CCActionDelay.Create(.5f),
			image.gameObject.CCShow(),
			image.transform.CCMoveTo(1f, new Vector3(0, 0, 0)),

			image.transform.CCQuadBezierTo(1.0f, new Vector3(-200, 100, 0), new Vector3(400, 300, 0)),
			CCActionDelay.Create(0.5f),
			image.transform.CCMoveTo(1f, new Vector3(0, 0, 0)),

			image.transform.CCCubicBezierTo(1.0f, new Vector3(-200, 100, 0), new Vector3(600, -300, 0), new Vector3(400, 300, 0)),
			CCActionDelay.Create(0.5f),
			image.transform.CCMoveTo(1f, new Vector3(0, 0, 0)),

			image.transform.CCCatmullRoomTo(1.0f, new Vector3(-200, 100, 0), new Vector3(600, -300, 0), new Vector3(400, 300, 0)),
			CCActionDelay.Create(0.5f),
			image.transform.CCMoveTo(1f, new Vector3(0, 0, 0)),

			image.transform.CCEllipse(2.0f, new Vector3(0, 0, 0), new Vector3(1,0,0), new Vector3(0,1,0),200,100),
			CCActionDelay.Create(0.5f),
			image.transform.CCMoveTo(1f, new Vector3(0, 0, 0)),

			CCActionDelay.Create(0f)
			); ;
		CCActionManager.Instance.AddAction(action, transform);

		/*
		btn_text.text = "点击停止";
		var action = Sequence.Create(
			CallFunc.Create(CallBackO, "延时0.5秒"),
			DelayTime.Create(0.5f),
			CallFunc.Create(CallBackO, "图片移动"),
			MoveTo.Create(1f, 200, 100).InitSubjectTransform(image.transform),
			MoveTo.Create(1f, 0, 0).InitSubjectTransform(image.transform),
			CallFunc.Create(CallBackO, "颜色渐变"),
			ColorTo.Create(0.5f, 1, 0, 0, 1f).InitSubjectComponent(image),
			ColorTo.Create(0.5f, 0, 1, 0, 1f).InitSubjectComponent(image),
			ColorTo.Create(0.5f, 0, 0, 1, 1f).InitSubjectComponent(image),
			CallFunc.Create(CallBackO, "颜色reset"),
			ColorTo.Create(0.5f, 1, 1, 1, 1).InitSubjectComponent(image),
			CallFunc.Create(CallBackO, "渐隐"),
			FadeOut.Create(1f).InitSubjectComponent(image),
			CallFunc.Create(CallBackO, "渐出"),
			FadeIn.Create(1f).InitSubjectComponent(image),
			CallFunc.Create(CallBackO, "1秒闪烁5次"),
			Blink.Create(1f, 5),
			CallFunc.Create(CallBackO, "二阶贝塞尔曲线"),
			BezierTo.Create(2.0f, new Vector3(200, 100, 0), new Vector3(-100, 50, 0), new Vector3(100, 80, 0)).InitSubjectTransform(image.transform),
			CallFunc.Create(CallBackO, "移动回原点"),
			MoveTo.Create(1f, 0, 0).InitSubjectTransform(image.transform),
			CallFunc.Create(CallBackO, "边移动边放大"),
			Spawn.Create(MoveTo.Create(1.0f, 200, 100f), ScaleTo.Create(1.0f, 3.0f, 3.0f, 3.0f)),
			CallFunc.Create(CallBackO, "缩放位置回原位"),
			Spawn.Create(MoveTo.Create(1.0f, 0, 0), ScaleTo.Create(1.0f, 1f, 1f, 1f)),
			NumberBy.Create(2.0f, "数字跳跃，从100到200：{0:f2}", 100, 100).InitSubjectComponent(text),
			DelayTime.Create(0.5f),
			NumberTo.Create(2.0f, "数字跳跃，从200到0：{0:f2}", 200, 0).InitSubjectComponent(text),
			DelayTime.Create(0.5f),
			CallFunc.Create(CallBackO, "Image Filled"),
			FillAmountTo.Create(1.5f, 0f).InitSubjectComponent(image),
			FillAmountTo.Create(1.5f, 1.0f).InitSubjectComponent(image),
			CallFunc.Create(CallBackO, "reset"),
			CallFunc.Create(CallBackO, "旋转 放大2倍，复原。重复3次"),
			Repeat.Create(Spawn.Create(
				RotationBy.Create(1.0f, 0, 0, 360f),
				Sequence.Create(
					ScaleTo.Create(0.5f, 2.0f, 2.0f, 2.0f),
					ScaleTo.Create(0.5f, 1.0f, 1.0f, 1.0f))).InitSubjectTransform(image.transform), 3),
			ExtraAction.Create()
			);

		float toX = 300f;
		var easeAction = Sequence.Create(
			CallFunc.Create(CallBackO, "变速运动演示，图一正常，图二是变速运动"),
			DelayTime.Create(1f),
			CallFunc.Create(CallBackO, "当前变速是EaseIn"),
			DelayTime.Create(0.5f),
			Spawn.Create(
				MoveBy.Create(1.0f, toX, 0f, 0f).InitSubjectTransform(image.transform),
				EaseIn.Create(MoveBy.Create(1.0f, toX, 0f, 0f).InitSubjectTransform(image2.transform), 3f)),
			CallFunc.Create(CallBackO, "复位"),
			Spawn.Create(
				MoveBy.Create(1.0f, -toX, 0f, 0f).InitSubjectTransform(image.transform),
				EaseIn.Create(MoveBy.Create(1.0f, -toX, 0f, 0f).InitSubjectTransform(image2.transform), 3f)),

			CallFunc.Create(CallBackO, "当前变速是EaseOut"),
			DelayTime.Create(0.5f),
			Spawn.Create(
				MoveBy.Create(1.0f, toX, 0f, 0f).InitSubjectTransform(image.transform),
				EaseOut.Create(MoveBy.Create(1.0f, toX, 0f, 0f).InitSubjectTransform(image2.transform), 3f)),
			CallFunc.Create(CallBackO, "复位"),
			Spawn.Create(
				MoveBy.Create(1.0f, -toX, 0f, 0f).InitSubjectTransform(image.transform),
				EaseOut.Create(MoveBy.Create(1.0f, -toX, 0f, 0f).InitSubjectTransform(image2.transform), 3f)),

			CallFunc.Create(CallBackO, "当前变速是EaseInOut"),
			DelayTime.Create(0.5f),
			Spawn.Create(
				MoveBy.Create(1.0f, toX, 0f, 0f).InitSubjectTransform(image.transform),
				EaseInOut.Create(MoveBy.Create(1.0f, toX, 0f, 0f).InitSubjectTransform(image2.transform), 3f)),
			CallFunc.Create(CallBackO, "复位"),
			Spawn.Create(
				MoveBy.Create(1.0f, -toX, 0f, 0f).InitSubjectTransform(image.transform),
				EaseInOut.Create(MoveBy.Create(1.0f, -toX, 0f, 0f).InitSubjectTransform(image2.transform), 3f)),
			CallFunc.Create(CallBackO, "变速运动未完待续"),
			DelayTime.Create(1f),
			ExtraAction.Create()
			);

		CCActionManager.Instance.AddAction(Sequence.Create(action,easeAction, CallFunc.Create(CallBackEnd)), transform);
	*/
	}
}

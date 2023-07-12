using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ccAction;
using System.Reflection;
using UnityEngine.Events;
using System;

public class TestReflection
{
	public float fillAmount { get; set; }
}

public class ActionTest : MonoBehaviour {

    [Tooltip("测试单次缓动时间")]
    public float duration = 1.0f;


    public Image image;
	public Image image2;
	public Text text;
	public Outline outline;

    public GameObject buttonCon;
    Button[] buttons;
	void Start () {
		image = transform.Find("image").GetComponent<Image>();
		image2 = transform.Find("image_2").GetComponent<Image>();
		text = transform.Find("txt").GetComponent<Text>();
		outline = transform.Find("txt").GetComponent<Outline>();

		Reset(true);


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

        var button = buttons[buttonCon.transform.childCount-1];
        if(button)
        {
            SetButtonText(button, "执行全部");
            Color color;
            if(ColorUtility.TryParseHtmlString("#E26A00",out color))
            {
                button.GetComponent<Image>().color = color;
            }
            button.onClick.AddListener(() => {
                Reset(true);
                //ActionInterval action = GetAllActions(buttonCon.transform.childCount-1);
                //CCAction.Do(action, transform);
                StartCoroutine(TestCoroutine());
            });
        }
    }

    IEnumerator TestCoroutine()
    {
        ActionInterval action = GetAllActions(buttonCon.transform.childCount - 1);
        CCAction.Do(action, transform);

        yield return action;

        Debug.Log("TestCoroutine Finish !!!!!");
    }

    public void AddButtonClick(uint index, Func<ActionInterval> func)
    {
        if(index>= buttons.Length)
        {
            Debug.LogError($"button is less");
            return;
        }
        var button = buttons[index];
        button.onClick.AddListener(()=> {
            Reset(true);
            ActionInterval action = func();
            CCAction.Do(action, transform);
        });
    }

	
	void Reset(bool clearAction = false)
	{
        if(clearAction)
        {
            text.text = "点击按钮测试";
            CCActionManager.Instance.RemoveAllActionsFromTarget(transform);
        }
		
		image.transform.localPosition = Vector3.zero;
		image.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		image.transform.localRotation = new Quaternion(0, 0, 0, 0);
        image.color = Color.white;


        image2.transform.localPosition = new Vector3(0, -100, 0);
        image2.gameObject.SetActive(false);


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

    ActionInterval GetAllActions(int count)
    {
        ActionInterval action = null;
        for (int i = 0; i < count; i++)
        {
            Type tp = typeof(ActionTest);
            var methodInfo = tp.GetMethod($"OnClick{i + 1}");
            object o = methodInfo.Invoke(this, new System.Object[] { });
            if (o == null)
                break;
            var action2 = o as ActionInterval;
            if (action == null)
                action = action2;
            else
                action = CCAction.CCSequence(action, action2);
        }
        return action;
    }


    public ActionInterval OnClick1()
    {
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("移动返回");}),
            image.transform.CCMoveTo(duration*0.5f, new Vector3(200, 100, 0)),
            image.transform.CCMoveTo(duration*0.5f, new Vector3(0, 0, 0)),
            CCActionDelay.Create(0f)
        );
        return action;
    }

    public ActionInterval OnClick2()
    {
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("缩放3倍再缩放到1倍");}),
            image.transform.CCScaleTo(duration*0.5f, new Vector3(3, 3, 3)),
            image.transform.CCScaleTo(duration*0.5f, new Vector3(1, 1, 1)),
            CCActionDelay.Create(0f)
        );
        return action;
    }

    public ActionInterval OnClick3()
    {
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("旋转");}),
            image.transform.CCAnglesTo(duration*0.3f, new Vector3(0, 0, 180)),
            image.transform.CCAnglesTo(duration*0.3f, new Vector3(0, 0, 360)),
            CCActionDelay.Create(0f)
        );
        return action;
    }

    public ActionInterval OnClick4()
    {
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("二阶贝塞尔");}),
            image.transform.CCQuadBezierTo(duration*2, new Vector3(-200, 100, 0), new Vector3(400, 300, 0)),
            CCActionDelay.Create(.1f),
            image.transform.CCMoveTo(duration*0.3f, new Vector3(0, 0, 0)),
            CCActionDelay.Create(0f)
        );
        return action;
    }

    public ActionInterval OnClick5()
    {
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("三阶贝塞尔");}),
            image.transform.CCCubicBezierTo(duration*2, new Vector3(-200, 100, 0), new Vector3(600, -300, 0), new Vector3(400, 300, 0)),
            CCActionDelay.Create(0.1f),
            image.transform.CCMoveTo(duration*0.3f, new Vector3(0, 0, 0)),
            CCActionDelay.Create(0f)
        );
        return action;
    }

    public ActionInterval OnClick6()
    {
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("椭圆运动1.5圈,复位");}),
            image.transform.CCEllipse2D(duration*2, new Vector3(0, 0, 0), 180, 50,0,1.5f),
            CCActionDelay.Create(0.1f),
            image.transform.CCMoveTo(duration*0.3f, new Vector3(0, 0, 0)),
            CCActionDelay.Create(0f)
        );
        return action;
    }

    public ActionInterval OnClick7()
    {
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("重复测试：移动重复3次");}),
            image.transform.CCMoveTo(duration*0.7f, new Vector3(200, 100, 0)),
            image.transform.CCMoveTo(duration*0.7f, new Vector3(0, 0, 0)),
            CCActionDelay.Create(0f)
        );
        return CCAction.CCRepeat(action, 3);
    }

    public ActionInterval OnClick8()
    {
        var action1 = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("并行测试：移动缩放");}),
            image.transform.CCMoveTo(duration, new Vector3(200, 100, 0)),
            image.transform.CCMoveTo(duration, new Vector3(0, 0, 0)),
            CCActionDelay.Create(0f)
        );
        var action2 = CCActionSequence.Create(
            image.transform.CCScaleTo(duration, new Vector3(2, 2, 2)),
            image.transform.CCScaleTo(duration, new Vector3(1, 1, 1)),
            CCActionDelay.Create(0f)
        );
        var action = CCActionSpawn.Create(action1, action2);

        return action;
    }

    public ActionInterval OnClick9()
    {
        return CCAction.CCSequence(
            CCAction.CCDelay(0, () => { Reset(); SetTile("闪烁5次");}),
            image.gameObject.CCBlink(duration, 5));
    }

    public ActionInterval OnClick10()
    {
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("进度条");}),
            image.CCFillAmout(duration*0.7f, 0),
            CCActionDelay.Create(0.1f),
            image.CCFillAmout(duration*0.7f, 1),
            CCActionDelay.Create(0f)
        );
        return action;
    }

    public ActionInterval OnClick11()
    {
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("变色、淡入、淡出");}),
            image.CCColorTo(duration*0.5f, Color.red),
            CCActionDelay.Create(0.1f),
            image.CCColorTo(duration*0.5f, Color.white),
            CCActionDelay.Create(0.1f),
            image.CCFadeOut(duration*0.5f),
            image.CCFadeIn(duration*0.5f),
            CCActionDelay.Create(0f)
        );
        return action;

    }
    public ActionInterval OnClick12()
    {
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("跳数字");}),
            CCActionDelay.Create(0.2f),
            text.CCNumberTo(1.0f, "数字跳动，0-100，当前：{0:F2}",0,100, "数字跳动，0-100，当前：{0:F0}"),
            CCActionDelay.Create(0f)
        );
        return action;
    }
    public ActionInterval OnClick13()
    {
        
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("打印机");}),
            CCActionDelay.Create(0.2f),
            text.CCTextPrint(1.0f,"打印机：0123456789零一二三四五六七八九"),
            CCActionDelay.Create(0f)
        );
        return action;

    }
    public ActionInterval OnClick14()
    {

        var action = CCActionSequence.Create(
           CCAction.CCDelay(0, () => { Reset(); SetTile("变速Ease");}),
           image2.gameObject.CCShow(),
           CCAction.CCDelay(0, () => { SetTile("变速EaseIn 上图先慢后快，下图正常"); }),
           CCAction.CCSpawn(
               CCAction.CCElasticEaseIn(image.transform.CCMoveTo(duration*0.5f, new Vector3(200, 0, 0)), 0.3f),
               image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
               CCActionDelay.Create(0f)
           ),
           CCActionDelay.Create(0.1f),
           CCAction.CCSpawn(
               CCAction.CCElasticEaseIn(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0)), 0.3f),
               image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
               CCActionDelay.Create(0f)
           ),

           CCAction.CCDelay(0.1f, () => { SetTile("变速EaseOut 上图先快后慢，下图正常"); }),
           CCAction.CCSpawn(
               CCAction.CCElasticEaseOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0)), 0.3f),
               image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
               CCActionDelay.Create(0f)
           ),
           CCActionDelay.Create(0.1f),
           CCAction.CCSpawn(
               CCAction.CCElasticEaseOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0)), 0.3f),
               image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
               CCActionDelay.Create(0f)
           ),

           CCAction.CCDelay(0.1f, () => { SetTile("变速EaseInOut 上图前后慢中间快，下图正常"); }),
           CCAction.CCSpawn(
               CCAction.CCElasticEaseInOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0)), 0.3f),
               image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
               CCActionDelay.Create(0f)
           ),
           CCActionDelay.Create(0.1f),
           CCAction.CCSpawn(
               CCAction.CCElasticEaseInOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0)), 0.3f),
               image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
               CCActionDelay.Create(0f)
           ),
           CCActionDelay.Create(0f)
        );
        return action;



        // var action = CCActionSequence.Create(
        //     CCAction.CCDelay(0, () => { Reset(); SetTile("变速Ease"); }),
        //     image2.gameObject.CCShow(),
        //     CCAction.CCDelay(0, () => { SetTile("变速EaseIn 上图先慢后快，下图正常"); }),
        //     CCAction.CCSpawn(
        //         CCAction.CCQuadraticIn(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0))),
        //         image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
        //         CCActionDelay.Create(0f)
        //     ),
        //     CCActionDelay.Create(0.1f),
        //     CCAction.CCSpawn(
        //         CCAction.CCQuadraticIn(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0))),
        //         image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
        //         CCActionDelay.Create(0f)
        //     ),

        //     CCAction.CCDelay(0.1f, () => { SetTile("变速EaseOut 上图先快后慢，下图正常"); }),
        //     CCAction.CCSpawn(
        //         CCAction.CCQuadraticOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0))),
        //         image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
        //         CCActionDelay.Create(0f)
        //     ),
        //     CCActionDelay.Create(0.1f),
        //     CCAction.CCSpawn(
        //         CCAction.CCQuadraticOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0))),
        //         image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
        //         CCActionDelay.Create(0f)
        //     ),

        //     CCAction.CCDelay(0.1f, () => { SetTile("变速EaseInOut 上图前后慢中间快，下图正常"); }),
        //     CCAction.CCSpawn(
        //         CCAction.CCQuadraticInOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0))),
        //         image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
        //         CCActionDelay.Create(0f)
        //     ),
        //     CCActionDelay.Create(0.1f),
        //     CCAction.CCSpawn(
        //         CCAction.CCQuadraticInOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0))),
        //         image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
        //         CCActionDelay.Create(0f)
        //     ),
        //     CCActionDelay.Create(0f)
        // );
        // return action;
    }
    public ActionInterval OnClick15()
    {
        
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("变速Elastic");}),
            image2.gameObject.CCShow(),
            CCAction.CCDelay(0, () => { SetTile("变速ElasticIn 上图先慢后快，下图正常"); }),
            CCAction.CCSpawn(
                CCAction.CCElasticEaseIn(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0)), 10),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCActionDelay.Create(0.1f),
            CCAction.CCSpawn(
                CCAction.CCElasticEaseIn(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0)), 10),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
                CCActionDelay.Create(0f)
            ),

            CCAction.CCDelay(0.1f, () => { SetTile("变速ElasticOut 上图先快后慢，下图正常"); }),
            CCAction.CCSpawn(
                CCAction.CCElasticEaseOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0)), 10),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCActionDelay.Create(0.1f),
            CCAction.CCSpawn(
                CCAction.CCElasticEaseOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0)), 10),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
                CCActionDelay.Create(0f)
            ),

            CCAction.CCDelay(0.1f, () => { SetTile("变速ElasticInOut 上图前后慢中间快，下图正常"); }),
            CCAction.CCSpawn(
                CCAction.CCElasticEaseInOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0)), 10),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCActionDelay.Create(0.1f),
            CCAction.CCSpawn(
                CCAction.CCElasticEaseInOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0)), 10),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCAction.CCDelay(0, () => { SetTile("完成"); }),
            CCActionDelay.Create(0f)
        );
        return action;

    }

    public ActionInterval OnClick16()
    {
        
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("变速Quart");}),
            image2.gameObject.CCShow(),
            CCAction.CCDelay(0, () => { SetTile("变速QuartIn 上图先慢后快，下图正常"); }),
            CCAction.CCSpawn(
                CCAction.CCQuartEaseIn(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCActionDelay.Create(0.1f),
            CCAction.CCSpawn(
                CCAction.CCQuartEaseIn(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
                CCActionDelay.Create(0f)
            ),

            CCAction.CCDelay(0.1f, () => { SetTile("变速QuartOut 上图先快后慢，下图正常"); }),
            CCAction.CCSpawn(
                CCAction.CCQuartEaseOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCActionDelay.Create(0.1f),
            CCAction.CCSpawn(
                CCAction.CCQuartEaseOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
                CCActionDelay.Create(0f)
            ),

            CCAction.CCDelay(0.1f, () => { SetTile("变速QuartInOut 上图前后慢中间快，下图正常"); }),
            CCAction.CCSpawn(
                CCAction.CCBounceEaseInOut(image.transform.CCMoveTo(duration, new Vector3(200, 0, 0))),
                image2.transform.CCMoveTo(duration, new Vector3(200, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCActionDelay.Create(0.1f),
            CCAction.CCSpawn(
                CCAction.CCBounceEaseInOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCAction.CCDelay(0, () => { SetTile("完成"); }),
            CCActionDelay.Create(0f)
        );
        return action;

    }
    public ActionInterval OnClick17()
    {
        
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("变速Bounce");}),
            image2.gameObject.CCShow(),
            CCAction.CCDelay(0, () => { SetTile("变速BounceIn 上图先慢后快，下图正常"); }),
            CCAction.CCSpawn(
                CCAction.CCBounceEaseIn(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCActionDelay.Create(0.1f),
            CCAction.CCSpawn(
                CCAction.CCBounceEaseIn(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
                CCActionDelay.Create(0f)
            ),

            CCAction.CCDelay(0.1f, () => { SetTile("变速BounceOut 上图先快后慢，下图正常"); }),
            CCAction.CCSpawn(
                CCAction.CCBounceEaseOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCActionDelay.Create(0.1f),
            CCAction.CCSpawn(
                CCAction.CCBounceEaseOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
                CCActionDelay.Create(0f)
            ),

            CCAction.CCDelay(0.5f, () => { SetTile("变速BounceInOut 上图前后慢中间快，下图正常"); }),
            CCAction.CCSpawn(
                CCAction.CCBounceEaseInOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCActionDelay.Create(0.1f),
            CCAction.CCSpawn(
                CCAction.CCBounceEaseInOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCAction.CCDelay(0, () => { SetTile("完成"); }),
            CCActionDelay.Create(0f)
        );
        return action;
    }

    public ActionInterval OnClick18()
    {
        
        var action = CCActionSequence.Create(
            CCAction.CCDelay(0, () => { Reset(); SetTile("变速Back");}),
            image2.gameObject.CCShow(),
            CCAction.CCDelay(0, () => { SetTile("变速BackIn 上图先慢后快，下图正常"); }),
            CCAction.CCSpawn(
                CCAction.CCBackEaseIn(image.transform.CCMoveTo(duration * 0.5f * 0.5f, new Vector3(200, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f * 0.5f, new Vector3(200, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCActionDelay.Create(0.1f),
            CCAction.CCSpawn(
                CCAction.CCBackEaseIn(image.transform.CCMoveTo(duration * 0.5f * 0.5f, new Vector3(0, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f * 0.5f, new Vector3(0, -100, 0)),
                CCActionDelay.Create(0f)
            ),

            CCAction.CCDelay(0.1f, () => { SetTile("变速BackOut 上图先快后慢，下图正常"); }),
            CCAction.CCSpawn(
                CCAction.CCBackEaseOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCActionDelay.Create(0.1f),
            CCAction.CCSpawn(
                CCAction.CCBackEaseOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
                CCActionDelay.Create(0f)
            ),

            CCAction.CCDelay(0.1f, () => { SetTile("变速BackInOut 上图前后慢中间快，下图正常"); }),
            CCAction.CCSpawn(
                CCAction.CCBackEaseInOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(200, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(200, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCActionDelay.Create(0.1f),
            CCAction.CCSpawn(
                CCAction.CCBackEaseInOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0))),
                image2.transform.CCMoveTo(duration * 0.5f, new Vector3(0, -100, 0)),
                CCActionDelay.Create(0f)
            ),
            CCAction.CCDelay(0, () => { SetTile("完成"); }),
            CCActionDelay.Create(0f)
        );
        return action;

    }

    public ActionInterval OnClick19()
    {
        
        var action1 = CCAction.CCSequence(
            CCAction.CCDelay(0, () => { Reset(); SetTile("获得奖励效果");}),

            image.transform.CCMoveTo(0, new Vector3(0, 200, 0)),
            image.transform.CCMoveTo(duration*0.3f, new Vector3(0, 0, 0)),
            CCActionDelay.Create(0f)
        );
        var action2 = CCAction.CCSequence(
            image.transform.CCScaleTo(0, new Vector3(3, 3, 3)),
            image.transform.CCScaleTo(duration*0.3f, new Vector3(0.5f, 0.5f, 0.5f)),
            image.transform.CCScaleTo(duration*0.2f, new Vector3(1.3f, 1.3f, 1.3f)),
            image.transform.CCScaleTo(duration* 0.15f, new Vector3(1, 1, 1)),
            CCActionDelay.Create(0.2f),
            CCActionDelay.Create(0f)
        );
        var action = CCActionSpawn.Create(action1, action2);

        return CCAction.CCRepeat(action,3);
    }

    public ActionInterval OnClick20()
    {
        var action1 = CCAction.CCSequence(
            CCAction.CCDelay(0, () => { Reset(); SetTile("图一浮动、图二震动");}),
            image2.gameObject.CCShow(),
            CCAction.CCSineEaseIn(image.transform.CCMoveTo(duration*0.5f, new Vector3(0, 100, 0))),
            CCAction.CCSineEaseOut(image.transform.CCMoveTo(duration * 0.5f, new Vector3(0, 0, 0))),
            CCActionDelay.Create(0f)
        );
        var action2 = CCAction.CCSequence(
            image2.transform.CCShake(duration, new Vector3(20, 20,0)),
            image2.transform.CCMoveTo(0,new Vector3(0,-100,0)),
            CCActionDelay.Create(0f)
        );
        var action = CCActionSpawn.Create(action1, action2);
        return CCAction.CCRepeat(action,3);
    }

    public void D()
    {
        var action = CCAction.CCSequence(
            CCAction.CCDelay(0, () => { Debug.Log("HelloWorld"); }),    //回调
            gameObject.CCShow(),                                        //显示
            transform.CCMoveTo(1.0f, new Vector3(100, 100, 100)),       //移动
            CCAction.CCSineEaseIn(image.transform.CCMoveTo(1.0f, new Vector3(0, 0, 0))),    //变速(In先慢后快) 移动
            CCAction.CCSpawn(
                transform.CCAnglesTo(1.0f, new Vector3(0, 0, 360)),
                transform.CCScaleTo(0.8f, new Vector3(2, 2, 2))
            ),// 并行执行 旋转和缩放，取最长时间作为结束时间
            transform.CCScaleTo(1f, new Vector3(1, 1, 1)), //缩放
            CCActionDelay.Create(0f)
        );
        CCAction.Do(action, transform, 3);  //执行Action，并且循环3次，小于0无限循环

        CCAction.Kill(transform);
    }
}

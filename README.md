# ccAction
cocos2dx action 移植到unity。另外参考[MotionFramework](https://github.com/gmhevinci/MotionFramework)做了新一版本优化。**轻量**、**易扩展**，方便**导出至热更工程**。

# 环境
环境**unity2017** 、**.Net4.0**以上即可  



## 目录结构

- **base** 	基础代码
- **action**  具体的节点，分为以下几种：
  1. **CCActionContainer** 容器节点包括：**顺序、并行、重复、无限重复**
  2. **CCActionDelay** 延迟节点
  3. **CCActionEase** 变速相关
  4. **CCActionExecute** 执行单次节点
  5. **CCActionStruct** 基础类型缓动，包括：**Float、Vector2、Vector3、Vector4、Color、Quaternion**
- **extension**  可用**Unity对象扩展的**具体的缓动效果
- **CCAction.cs**  统一的接口，包括 **回调、延迟、延迟回调、变速**方法的具体实现



## 说明

1. 启动**CCActionManager**。

```c#
// 在Update调用
CCAction.Update(Time.deltaTime);
```

2. 添加新的缓动

```c#
var action = CCAction.CCSequence(
			CCaction.CCCall(() => { Debug.Log("HelloWorld");)			//回调
    		CCAction.CCDelay(0.5f }),									//间隔0.5秒
            CCAction.CCDelay(0.5f, () => { Debug.Log("HelloWorld"); }), //间隔0.5f秒，并且回调
            gameObject.CCShow(),                                        //显示
            transform.CCMoveTo(1.0f, new Vector3(100, 100, 100)),       //移动
            CCAction.CCSineEaseIn(image.transform.CCMoveTo(1.0f, new Vector3(0, 0, 0))),    //变速(In先慢后快) 移动
            CCAction.CCSpawn(
                transform.CCAnglesTo(1.0f, new Vector3(0, 0, 360)),
                transform.CCScaleTo(0.8f, new Vector3(2, 2, 2))
            ),// 并行执行 旋转和缩放，取最长时间作为结束时间
            transform.CCScaleTo(1f, new Vector3(1, 1, 1)) //缩放
        );
CCAction.Do(action, transform);  //执行Action
// CCAction.Do(action, transform, 3);  //执行Action，并且循环3次
// CCAction.Do(action, transform, -1);  //执行Action，小于0无限循环
```

3. 停止缓动

```c#
CCAction.Kill(transform); 	//移除transform(CCAction.Do(action, transform)同一个)所有的Action
```

4. 热更导出和扩展

- 热更导出，只需要导出**ActionInterval.cs**和**CCAction.cs**即可
- 扩展，把扩展的方法添加至**extension目录**，并在**CCAction.cs**添加对应的方法，方便统一导出。



## 截图

加载场景***ccAction/sample/SampleScene***运行截图

![image](https://github.com/ZensYue/ccAction/blob/master/Doc/Image/ActionTest.jpg)

![gif](https://github.com/ZensYue/ccAction/blob/master/Doc/Image/ActionTest.gif)

# 注意
**RepeatForever不能用Sequence或者Spawn装载**

# TODO
1. 添加编辑器运行时Debug



## 推荐

1. [MotionFramework](https://github.com/gmhevinci/MotionFramework) 开箱即用的轻量框架。
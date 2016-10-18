/**
 *          全局委托定义
 **/

public delegate void OnShowUIRoundStateOverDelegate();          //进入回合时显示回合信息UI动画结束

public delegate void OnPanelFadeAwayEndDelegate(float alpha);   //panel执行减退动画结束帧时的事件                                                          //panel执行减退动画结束帧时的事件
public delegate void OnPanelEndDelegate();                      //panel结束操作时的事件,比如按钮某个按钮，收到某个包等
using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour
{
    protected int m_PanelId;
    private static int m_PanelIdCounter = 0;

    protected event OnPanelFadeAwayEndDelegate m_OnFadeAwayEndDelegateCallback;
    public delegate void OnSceneUnloadDelegate();
    protected event OnSceneUnloadDelegate m_OnSceneUnloadDelegateCallback;

    protected void Awake()
    {
        m_PanelId = ++m_PanelIdCounter;

        m_OnFadeAwayEndDelegateCallback = null;
        m_OnSceneUnloadDelegateCallback = null;
    }

    // Use this for initialization
    protected void Start()
    {
        //将每一块panel注册到当前画布类中管理
        UIManager.Instance.MainCanvas.RegisterPanel(this);
    }

    // Update is called once per frame
    void Update() { }

    //整体面板消失动画
    protected IEnumerator FadeAway()
    {
        Animator animator = GetComponent<Animator>();

        if (null != animator)
        {
            animator.enabled = true;
            //animator.Play(AnimatorConfig.STATE_FADEAWAY_ALPHA);
        }

        yield return new WaitForEndOfFrame();
    }

    //FadeAway动画结束事件
    public void FadeAwayEnd(float alpha)
    {
        Animator animator = GetComponent<Animator>();

        if (null != animator) animator.enabled = false;

        if (null != m_OnFadeAwayEndDelegateCallback) m_OnFadeAwayEndDelegateCallback(alpha);
    }

    //场景切换
    protected IEnumerator SwitchScene(float Delay, string sceneName)
    {
        yield return new WaitForSeconds(Delay);

        //GameManager.Instance.LoadScene(sceneName);

        if (null != m_OnSceneUnloadDelegateCallback) m_OnSceneUnloadDelegateCallback();
    }

    void OnDestroy()
    {
        //UIManager.Instance.MainCanvas.RemovePanel(m_PanelId);
    }

    #region set get
    public int PanelId
    {
        get { return m_PanelId; }
        set { m_PanelId = value; }
    }

    public event OnPanelFadeAwayEndDelegate OnPanelFadeAwayEndDelegateCallback
    {
        add { m_OnFadeAwayEndDelegateCallback += value; }
        remove { m_OnFadeAwayEndDelegateCallback -= value; }
    }
    #endregion  
}


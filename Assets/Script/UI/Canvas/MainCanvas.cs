using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *      游戏主界面画布,UI通过以Panel的形式加载到RootPanel下
 */

public class MainCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject m_RootPanel;

    //记录Panel，id是跟着panel的生成递增的,panel的id是唯一的，但是由于panel是动态生成的，所以id不是固定的，比如某个界面的的某个panel，两次进入的id会不同，但是对应的string值来是固定的
    private Dictionary<int, BasePanel> m_UIPanels;

    private Dictionary<string, int> m_UIPanelIds; //通过string保存对应Panel的Id，因为panel的id不是固定的，string值代表一个特定UI的panel

    void Awake()
    {
        m_UIPanels = new Dictionary<int, BasePanel>();
        m_UIPanelIds = new Dictionary<string, int>();

    }

    void Start()
    {
        LoadCursor();
    }

    void Update() { }

    public void RegisterPanel(BasePanel panel)
    {
        if (null == panel) return;

        if (!m_UIPanels.ContainsValue(panel))
        {
            m_UIPanels.Add(panel.PanelId, panel);
        }
    }

    //设置UI层次（影响渲染，触摸顺序）
    public void SetPanelLayer(GameObject uiLayer, int layerIndex)
    {
        int _index = Mathf.Clamp(layerIndex, 0, m_RootPanel.transform.childCount - 1);

        uiLayer.transform.SetSiblingIndex(_index);

        //Debug.Log("SetPanelLayer index:" + _index);
    }

    public void LoadCursor()
    {
        Cursor.visible = false;
        GameObject cursorGO = ResourceManager.Instance.LoadFullPathResource("Prefab/UI/Cursor");
        if (null != cursorGO)
        {
            cursorGO.transform.SetParent(m_RootPanel.transform);
            cursorGO.transform.localScale = Vector3.one;
            cursorGO.transform.localPosition = Vector3.zero;

            GameManager.Instance.GameInput.MouseGO = cursorGO;
        }
    }

    #region UI资源管理
    //path: 资源位于Resources下的路径}
    public T AddPanel<T>(string path, Vector3 pos) where T : BasePanel
    {
        T ret = null;

        Object obj = Resources.Load(path) as Object;
        if (null != obj)
        {
            GameObject go = Instantiate(obj) as GameObject;
            ret = go.GetComponent<T>();
            if (null != ret && !m_UIPanels.ContainsKey(ret.PanelId))
            {
                go.transform.SetParent(m_RootPanel.transform);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = pos;
                //Debug.Log("AddPanel id:" + ret.PanelId + " name:" + ret.name);
                m_UIPanels.Add(ret.PanelId, ret);
            }
            else
            {
                Destroy(go);
            }
        }

        return ret;
    }

    public GameObject AddPanel(string path, Vector3 pos)
    {
        GameObject ret = null;

        Object obj = Resources.Load(path) as Object;

        if (null != obj)
        {
            GameObject go = Instantiate(obj) as GameObject;

            go.transform.SetParent(m_RootPanel.transform);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = pos;
        }

        return ret;
    }

    //更新对应panel的id
    public void UpdatePanelId(string panelName, int panelId)
    {
        if (m_UIPanelIds.ContainsKey(panelName))
        {
            m_UIPanelIds[panelName] = panelId;
        }
    }

    //通过panel对应的string得到panel
    public BasePanel GetPanelByName(string name)
    {
        if (m_UIPanelIds.ContainsKey(name))
        {
            if (m_UIPanels.ContainsKey(m_UIPanelIds[name]))
            {
                return m_UIPanels[m_UIPanelIds[name]];
            }
        }

        return null;
    }

    //通过id得到panel
    public BasePanel GetPanelById(int panelId)
    {
        if (m_UIPanels.ContainsKey(panelId))
        {
            return m_UIPanels[panelId];
        }
        return null;
    }

    //通过类型得到panel
    public T GetPanelByType<T>() where T : BasePanel
    {
        T ret = default(T);

        List<int> keys = new List<int>(m_UIPanels.Keys);

        for (int i = 0; i < keys.Count; ++i)
        {
            if (m_UIPanels[keys[i]] is T)
            {
                ret = m_UIPanels[keys[i]] as T;
                break;
            }
        }

        return ret;
    }

    public void RemovePanel(int panelId)
    {
        if (!m_UIPanels.ContainsKey(panelId)) return;

        //StartCoroutine(DestroyPanel(m_UIPanels[panelId]));
        if (null != m_UIPanels[panelId]) Destroy(m_UIPanels[panelId].gameObject);

        m_UIPanels.Remove(panelId);
    }

    IEnumerator DestroyPanel(BasePanel panel)
    {
        yield return new WaitForEndOfFrame();

        if (null != panel.gameObject && null != panel) { Destroy(panel.gameObject); }
    }

    public void RemoveAllPanel()
    {
        m_UIPanels.Clear();
        for (int i = 0; i < m_RootPanel.transform.childCount; ++i)
        {
            Destroy(m_RootPanel.transform.GetChild(i).gameObject);
        }
    }

    public int GetCanvasChildCount()
    {
        int ret = -1;

        if (null != m_RootPanel) ret = m_RootPanel.transform.childCount;

        return ret;
    }
    #endregion

    #region get / set
    public GameObject RootPanel
    {
        get { return m_RootPanel; }
    }
    #endregion
}

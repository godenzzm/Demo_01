using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private static UIManager m_Instance = null;
    public static UIManager Instance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;
            }
            return m_Instance;
        }
    }

    //画布的层次关系是通过对应的Camera的Depth来调整的,Depth值越大，显示在上层
    [SerializeField]
    private MainCanvas m_MainCanvas;            //主画布
    [SerializeField]
    private PostEffectCanvas m_PostEffectCanvas;        //特效画布


    void Awake()
    {
        DontDestroyOnLoad(gameObject); //始终存在于游戏生命周期中
    }

    void Start() { }

    void Update()
    {
        TouchListener();
    }

    void TouchListener()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (EventSystem.current.IsPointerOverGameObject())
            {
                //Debug.Log ("TouchListener !!!");
                if (null != EventSystem.current.currentSelectedGameObject)
                {
                    //Debug.Log ("UIManager touching currentSelectedGameObject:" + EventSystem.current.currentSelectedGameObject.name);
                }

                if (null != EventSystem.current.firstSelectedGameObject)
                {
                    //Debug.Log ("UIManager touching firstSelectedGameObject:" + EventSystem.current.firstSelectedGameObject);
                }

                if (null == EventSystem.current.currentSelectedGameObject && null == EventSystem.current.firstSelectedGameObject)
                {
                    //Debug.Log ("UIManager touching Null !!!");
                }
            }
        }
    }

    //调整画布深度
    public void SetCanvasDepth(GameObject canvasGO, int depth)
    {
        if (null == canvasGO) return;

        Camera camera = canvasGO.GetComponentInChildren<Camera>();

        if (null != camera)
        {
            camera.depth = depth;
        }
    }

    //自适应屏幕
    public void AdaptUI(GameObject go)
    {
        UIAdapter[] adapters = go.GetComponentsInChildren<UIAdapter>(true);

        for (int i = 0; i < adapters.Length; ++i)
        {
            adapters[i].AdaptUI();
        }
    }

    #region 通用Panel使用
    /*
    public LoadingPanel createLoadingPanel()
    {
        LoadingPanel ret = null;

        GameObject panelGO = ResourceManager.Instance.LoadFullPathResource(Config.UI_PANEL_LOADING_PATH);

        if (null != panelGO)
        {
            panelGO.transform.SetParent(m_MainCanvas.RootPanel.transform);
            panelGO.transform.localScale = Vector3.one;
            panelGO.transform.localPosition = Vector3.zero;

            ret = panelGO.GetComponent<LoadingPanel>();
        }

        return ret;
    }
    */
    #endregion

    #region set/get 
    public MainCanvas MainCanvas
    {
        get
        {
            return m_MainCanvas;
        }
    }

    public PostEffectCanvas PostEffectCanvas
    {
        get
        {
            return m_PostEffectCanvas;
        }
    }
    #endregion
}

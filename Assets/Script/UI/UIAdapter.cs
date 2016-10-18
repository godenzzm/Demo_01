using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIAdapter : MonoBehaviour
{
    public static float ConfigReferenceWidth = 1920;
    public static float ConfigReferenceHeight = 1080;

    void Awake()
    {
        AdaptUI();
    }

    //自适应屏幕
    public void AdaptUI()
    {
        RectTransform rt = GetComponent<RectTransform>() as RectTransform;

        if (null != rt)
        {
            //Debug.Log (name + " UIAdapater AdaptUI offsetMax:" + rt.offsetMax + " offsetMin:" + rt.offsetMin);
            //Debug.Log (name + " UIAdapater AdaptUI CanvasHeight:" + GameSystemData.Instance.CanvasHeight + " ReferenceResolutionHeight:" + Config.ReferenceResolutionHeight);
            float canvasWidth = ConfigReferenceWidth;
            float canvasHeight = ConfigReferenceWidth / (Screen.width * 1.0f / Screen.height); //根据实际屏幕分辨率换算画布高度

            rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y * (canvasHeight / ConfigReferenceHeight));
            rt.offsetMin = new Vector2(rt.offsetMin.x, rt.offsetMin.y * (canvasHeight / ConfigReferenceHeight));
        }
        //         Image img = GetComponent<Image>();
        //         if (null != img && img.type == Image.Type.Simple)
        //             img.SetNativeSize(    
    }

    public void AdaptUIScrollView()
    {
        //调整ScrollView控件布局
        GridLayoutGroup glg = GetComponentInChildren<GridLayoutGroup>();
        if (null != glg)
        {
            //Debug.Log ("AdaptUI GridLayoutGroup");
            glg.CalculateLayoutInputVertical();
            glg.SetLayoutVertical();
        }
    }
}

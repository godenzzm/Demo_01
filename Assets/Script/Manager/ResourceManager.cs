using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LitJson;

public class ResourceManager : MonoBehaviour 
{
	private static ResourceManager m_Instance = null;
	public static ResourceManager Instance
	{
		get 
		{
			if (null == m_Instance)
			{
				m_Instance = GameObject.FindObjectOfType (typeof(ResourceManager)) as ResourceManager;
			}
			return m_Instance;
		}
	}

	public const string												PREFAB_PATH = "Prefab/";//预件路径
	public const string												RES_PATH = "LocalRes/";//本地资源路径

	public const string 											UI = PREFAB_PATH + "UI/";//UI预件
	public const string 											SkillEditor = PREFAB_PATH + "UI/skillEditor/";//技能编辑器 UI预件
    public const string                                             UI_Component_Path = UI + "Component/";
    public const string                                             UI_Panel_Path = UI + "Panel/";

	#region Xml路径
    public const string 											XML_PATH = RES_PATH + "XML/";//xml 文件
	public const string 											SkillXML_PATH =  XML_PATH+"Skill/";//技能 xml 文件夹
	public const string 											SkillLibXML_Path =SkillXML_PATH+"skillLib.xml";//技能库 xml 文件
	public const string 											RoleListXML_Path =SkillXML_PATH+"roleInfoList.xml";//角色信息 xml 文件
    public const string                                             SKILL_XML_PATH = "Skill/Skill";//Skill.xml
    public const string                                             SKILLFX_XML_PATH = "Skill/SkillFX";
	public const string                                             BUFF_XML_PATH = "Buff/Buff";//Buff.xml
    #endregion
	#region json路径
	public const string 											JSON_PATH = RES_PATH + "json/";//json 文件
	public const string 											FUBEN_PATH = JSON_PATH + "fuben/";//json 文件
	public const string 											MAP_PATH = JSON_PATH + "map/";//json 文件
	#endregion
	

	public const string 											MODEL = PREFAB_PATH + "Model/";//模型预件

    public const string                                             AUDIO_PATH = RES_PATH + "AudioClip/";//本地音频资源
	public const string 											FX = PREFAB_PATH + "FX/";//本地特效资源
               
    #region Resoures本地预件加载
    //无任何路径前缀,完整路径
    public GameObject LoadFullPathResource(string name)
    {
        GameObject ret = null;

        Object obj = Resources.Load(name) as Object;

        if (null != obj)
        {
            GameObject go = GameObject.Instantiate(obj) as GameObject;

            ret = go;
        }

        return ret;
    }

    //加载UI控件
    public GameObject LoadUIComponentResource(string name)
    {
        GameObject ret = null;
        
        Object obj = Resources.Load(UI_Component_Path + name) as Object;

        if (null != obj)
        {
            ret = GameObject.Instantiate(obj) as GameObject;
        }

        return ret;
    }

    //加载UI面板
    public Object LoadUIPanelResource(string name) 
    {
        return Resources.Load(UI_Panel_Path + name) as Object;
    }

    //加载本地Xml文件
    public Object LoadXMLResource(string name)
    {
        return Resources.Load(XML_PATH + name) as Object;
    }

    //加载本地音频文件
	public AudioClip LoadAudioResource (string name)
	{
		return Resources.Load (AUDIO_PATH + name, typeof(AudioClip)) as AudioClip;
	}

    //加载特效预件
	public GameObject LoadFXResource (string path, Transform parent)
	{
		return LoadFXResource (path, parent, Vector3.zero);
	}

    //加载特效预件
	public GameObject LoadFXResource (string name, Transform parent, Vector3 pos)
	{
		GameObject ret = null;

		Object fx = Resources.Load (FX + name) as Object;

		if (null != fx)
		{
			ret = Instantiate (fx) as GameObject;
			if (null != parent) ret.transform.SetParent (parent);
			ret.transform.localScale = Vector3.one;
			ret.transform.position = pos;
		}

		return ret;
	}
 
   
//从json加载地图的一块
	private GameObject LoadMapPart (JsonData jsonData,int partIndex)
	{
		string bundleName = (string)(jsonData[partIndex]["bundleName"]);
		string assetName = (string)(jsonData[partIndex]["assetName"]);
		float pos_x = float.Parse((string)(jsonData[partIndex]["Pos_x"]));
		float pos_y = float.Parse((string)(jsonData[partIndex]["Pos_y"]));
		float pos_z = float.Parse((string)(jsonData[partIndex]["Pos_z"]));
		float rot_x = float.Parse((string)(jsonData[partIndex]["Rot_x"]));
		float rot_y = float.Parse((string)(jsonData[partIndex]["Rot_y"]));
		float rot_z = float.Parse((string)(jsonData[partIndex]["Rot_z"]));
		//Debug.Log("bundleName:" + bundleName + " assetName:" + assetName + " pos_x:" + pos_x + " pos_y:" + pos_y + " pos_z:" + pos_z + " rot_x:" + rot_x + " rot_y:" + rot_y + " rot_z:" + rot_z);
		
		//这里也可以使用AssetBundleManager下载
		//loadingPanel.AddDownloadingTask("", bundleName, assetName, null);
		Object obj = Resources.Load(bundleName + assetName);
		if (null != obj)
		{
			GameObject go = GameObject.Instantiate(obj) as GameObject;
			go.transform.localScale = Vector3.one;
			go.transform.position = new Vector3(pos_x, pos_y, pos_z);
			go.transform.rotation = Quaternion.Euler(new Vector3(rot_x, rot_y, rot_z));
			return go;
		}
		return null;
	}

    #endregion
}

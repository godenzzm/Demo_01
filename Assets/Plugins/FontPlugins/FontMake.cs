using UnityEngine;  
using System.Collections;
  #if UNITY_EDITOR
using UnityEditor;  
#endif
public class FontMake : MonoBehaviour   
{  
	public Font m_myFont;  
	public TextAsset m_data;  
	private BMFont mbFont = new BMFont();  
	public Material m_mat;

	void Start ()   
	{  
		BMFontReader.Load(mbFont, m_data.name, m_data.bytes);  // 借用NGUI封装的读取类
		CharacterInfo[] characterInfo = new CharacterInfo[mbFont.glyphs.Count];  
		for (int i = 0; i < mbFont.glyphs.Count; i++)  
		{  
			BMGlyph bmInfo = mbFont.glyphs[i];  
			CharacterInfo info = new CharacterInfo();  

			info.index = bmInfo.index;  
			info.width = (float)bmInfo.advance;
			Rect r = new Rect();

			r.x = (float)bmInfo.x / (float)mbFont.texWidth;
			r.y = (float)bmInfo.y / (float)mbFont.texHeight;
			r.width = (float)bmInfo.width  / (float)mbFont.texWidth;
			r.height = (float)bmInfo.height / (float)mbFont.texHeight;
			r.y = 1f - r.y - r.height;

			info.uv = r;

			r = new Rect ();

			r.x = (float)bmInfo.offsetX;
			r.y = (float)bmInfo.offsetY;
			r.width = (float) bmInfo.width;
			r.height = (float) (float)bmInfo.height;
			r.y = -r.y;
			r.height = -r.height;
			info.vert = r;

			characterInfo[i] = info;  
			/*
			Debug.Log ("bmInfo.x:" + bmInfo.x + " mbFont.texWidth:" + mbFont.texWidth);
			Debug.Log ("bmInfo.y:" + bmInfo.y + " mbFont.texHeight:" + mbFont.texHeight);
			Debug.Log ("bmInfo.width:" + bmInfo.width + " mbFont.texWidth:" + mbFont.texWidth);
			Debug.Log ("bmInfo.height:" + bmInfo.height + " mbFont.texHeight:" + mbFont.texHeight);
			Debug.Log ("bmInfo.offsetX:" + bmInfo.offsetX);
			Debug.Log ("bmInfo.advance:" + bmInfo.advance);*/
		} 

		m_myFont.characterInfo = characterInfo;  
		m_myFont.material = m_mat;
	} 
		/*
	void Start ()   
	{  
		BMFontReader.Load(mbFont, m_data.name, m_data.bytes);  // 借用NGUI封装的读取类
		CharacterInfo[] characterInfo = new CharacterInfo[mbFont.glyphs.Count];  
		for (int i = 0; i < mbFont.glyphs.Count; i++)  
		{  


			BMGlyph bmInfo = mbFont.glyphs[i];  
			CharacterInfo info = new CharacterInfo();  
			info.index = bmInfo.index;  
			info.uv.x = (float)bmInfo.x / (float)mbFont.texWidth;  
			info.uv.y = 1-(float)bmInfo.y / (float)mbFont.texHeight;  
			info.uv.width = (float)bmInfo.width / (float)mbFont.texWidth;  
			info.uv.height = -1f * (float)bmInfo.height / (float)mbFont.texHeight;  
			info.vert.x = (float)bmInfo.offsetX;  
			//info.vert.y = (float)bmInfo.offsetY;  
			info.vert.y = 0f;//自定义字库UV从下往上，所以这里不需要偏移，填0即可。 
			info.vert.width = (float)bmInfo.width;  
			info.vert.height = (float)bmInfo.height;  
			info.width = (float)bmInfo.advance;  
			characterInfo[i] = info;  

			Debug.Log ("bmInfo.x:" + bmInfo.x + " mbFont.texWidth:" + mbFont.texWidth);
			Debug.Log ("bmInfo.y:" + bmInfo.y + " mbFont.texHeight:" + mbFont.texHeight);
			Debug.Log ("bmInfo.width:" + bmInfo.width + " mbFont.texWidth:" + mbFont.texWidth);
			Debug.Log ("bmInfo.height:" + bmInfo.height + " mbFont.texHeight:" + mbFont.texHeight);
			Debug.Log ("bmInfo.offsetX:" + bmInfo.offsetX);
			Debug.Log ("bmInfo.advance:" + bmInfo.advance);
		}  
		m_myFont.characterInfo = characterInfo;  
	} */
}  

using UnityEditor;
using UnityEngine;
using System.Collections;

//[CustomEditor(typeof(Hexagon))]
[CanEditMultipleObjects]
public class HexagonEditor : Editor 
{
	Hexagon SelfHex;

	void OnEnable ()
	{
		SelfHex = target as Hexagon;

		//很奇怪，这里似乎无法通过对Hexagon的变量改变来区分已在场景中的hexagon和新拖入的
		//if (!SelfHex.IsEditorFinish) 
		//	AutoEmbedInEditor ();
		//SelfHex.InitNeighbourPosition ();
		//一旦这里修改，所有hexagon实例都会修改，照理添加了[CanEditMultipleObjects]不会又这个问题
		//之后通过hexagon的另一个变量NeighbourList是否保存有邻居对象来区分，是可以的
		//SelfHex.IsEditorFinish = true;
	}

	void AutoEmbedInEditor () 
	{
		Debug.Log (SelfHex.name + " HexagonEditor NeighbourList:" + SelfHex.NeighbourList.Count);

		if (SelfHex.NeighbourList.Count > 0)
			return;

		Hexagon[] hexagons = FindObjectsOfType<Hexagon>();
		//Debug.Log ("sssddd  " + hexagons.Length + " " + SelfHex.gameObject.name);
		if (hexagons.Length <= 1) 
		{
			SelfHex.transform.position = Vector3.zero;
			return;
		}

		float shortDis_1 = 99999;
		int index = 0;

		for (int i = 0; i < hexagons.Length; ++i) 
		{
			if (hexagons [i].Equals (SelfHex)) 
			{
				continue;
			}

			float dis = Vector3.Distance(SelfHex.transform.position, hexagons[i].transform.position);

			if (shortDis_1 > dis && hexagons[i].IsNeighbourAvailable()) 
			{ 
				shortDis_1 = dis;
				index = i;
			}
		}

		if (index >= 0 && index < hexagons.Length) 
		{
			EmbedAsNeighbour (hexagons [index]);
		}
	}

	//嵌入最近空位
	// param TargetHex:嵌入成为目标对象的邻居
	void EmbedAsNeighbour (Hexagon TargetHex) 
	{
		Debug.Log (SelfHex.gameObject.name + " EmbedAsNeighbour " + TargetHex.name);

		bool isOcuppied = false;
		float shortestDis = 9999;
		int index = 0; //目标Hex的6个邻居位置下标

		//遍历目标Hex的6个邻居位置
		for (int i = 0; i < TargetHex.NeighbourPositions.Length; ++i) 
		{
			//检测是否该位置已被占据
			foreach (Hexagon hex in TargetHex.NeighbourList) 
			{
				if (Vector3.Distance (hex.transform.position, TargetHex.NeighbourPositions[i]) < 1) //容差值1
				{
					isOcuppied = true;
					break;
				}
			}

			if (!isOcuppied) 
			{
				//找6个邻居位置中最近的位置
				float dis = Vector3.Distance (SelfHex.transform.position, TargetHex.NeighbourPositions[i]); 

				if (shortestDis < dis) 
				{
					shortestDis = dis;
					index = i;
				}
			} 
		}
		Debug.Log (SelfHex.gameObject.name + " EmbedAsNeighbour index:" + index + " position:" + TargetHex.NeighbourPositions[index]);
		//找到位置
		if (index < TargetHex.NeighbourPositions.Length && index >= 0) 
		{
			SelfHex.transform.position = TargetHex.NeighbourPositions[index];
			TargetHex.NeighbourList.Add (SelfHex);
			SelfHex.NeighbourList.Add (TargetHex);
		}
	}

	void OnDestroy()
	{
		Debug.Log (SelfHex.name + " OnDestroy !!!");

		Hexagon[] hexagons = FindObjectsOfType<Hexagon> ();

		for (int i = 0; i < hexagons.Length; ++i) 
		{
			if (hexagons [i].Equals (SelfHex)) continue;

			foreach (Hexagon hex in hexagons[i].NeighbourList) 
			{
				if (hex.Equals(SelfHex))
				{
					hexagons[i].NeighbourList.Remove (hex);
					break;		
				}
			}
		}
	}
}


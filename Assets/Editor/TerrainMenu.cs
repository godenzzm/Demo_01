using UnityEngine;
using UnityEditor;
using System.Collections;

public class TerrainMenu 
{
	const string HexagonPrefab_Path = "Assets/Prefab/Hexagon.prefab";
	const int HEX_ROW = 8; // 行
	const int HEX_COLUMN = 8; // 列
	const float HEX_RADIUS = 1.25f;

	[MenuItem("Terrain/Generate Terrain")]
	static void GenerateTerrain()
	{
		UnityEngine.Object Object = AssetDatabase.LoadAssetAtPath (HexagonPrefab_Path, typeof(UnityEngine.Object));

		GameObject Terrain = new GameObject ("Terrain");
		Terrain.transform.position = Vector3.zero;

		float x = 0;
		float y = 0;
		float z = 0;
		for (int row = 0; row < HEX_ROW; ++row) 
		{
			for (int column = 0; column < HEX_COLUMN; ++column) 
			{
				GameObject go = GameObject.Instantiate (Object) as GameObject;
				go.transform.SetParent (Terrain.transform);

				x = 1.5f * HEX_RADIUS * column;
				z = Mathf.Sqrt(3) * HEX_RADIUS * row + (column % 2 == 0 ? 0 : 1) * Mathf.Sqrt(3) * HEX_RADIUS * 0.5f;

				go.transform.position = new Vector3 (x, y, z);
				go.name = "Hexagon R" + row + "C" + column;

				Hexagon hexagon = go.GetComponent<Hexagon> () as Hexagon;
				if (null != hexagon) 
				{
					hexagon.RandomColor ();
				}
			}
		}
	}
}

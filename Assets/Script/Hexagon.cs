using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
//[InitializeOnLoad]
public class Hexagon : MonoBehaviour 
{
    public List<Hexagon> NeighbourList;    

    [SerializeField]
    private float Radius = 50/40f; 

    private bool IsAutoEmbedInEditor = true;
    public Vector3[] m_NeighbourPositions;

	public bool IsEditorFinish = false;



    void Awake() 
    {
		Debug.Log ("Hexagon Awake !!!");
        InitMember();
    }

	// Use this for initialization
	void Start () 
    {
		Debug.Log ("Hexagon Start !!!");
        RandomColor();
        //InitNeighbourPosition();
	}

	void OnEnable()
	{
		Debug.Log ("Hexagon OnEnabled !!!");
	}

	// Update is called once per frame
	void Update () 
    {
		Debug.Log ("Hexagon Update !!!");
		//AdjustDragPositionInEdit ();
	}

    void InitMember() 
    {
        NeighbourList = new List<Hexagon>();
    }

    void RandomColor() 
    {
		if (IsAutoEmbedInEditor) return;

        Renderer renderer = GetComponent<Renderer>();

        if (null != renderer && null != renderer.material) 
        {
            float randomR = Random.Range(0, 256);
            float randomG = Random.Range(0, 256);
            float randomB = Random.Range(0, 256);

            renderer.material.SetColor("_Color", new Color(randomR/255.0f, randomG/255.0f, randomB/255.0f));
        }
    }

    //检测是否周围有邻居
    public bool IsNeighbourAvailable() 
    {
        bool ret = false;

		if (null != NeighbourList && NeighbourList.Count < 6) ret = true;

        return ret;
    }

    public void InitNeighbourPosition() 
    {
        float x = transform.position.x;
        float y = transform.position.y;

        m_NeighbourPositions = new Vector3[6];

        m_NeighbourPositions[0] = new Vector3(x, 				 0, y + Mathf.Sqrt(3f) * Radius);
        m_NeighbourPositions[1] = new Vector3(x + 1.5f * Radius, 0, y + Mathf.Sqrt(3f) / 2f * Radius);
        m_NeighbourPositions[2] = new Vector3(x + 1.5f * Radius, 0, y - Mathf.Sqrt(3f) / 2f * Radius);
        m_NeighbourPositions[3] = new Vector3(x, 				 0, y - Mathf.Sqrt(3f) * Radius);
        m_NeighbourPositions[4] = new Vector3(x - 1.5f * Radius, 0, y - Mathf.Sqrt(3f) / 2f * Radius);
        m_NeighbourPositions[5] = new Vector3(x - 1.5f * Radius, 0, y + Mathf.Sqrt(3f) / 2f * Radius);

		Debug.Log ("Hexagon InitNeighbourPosition !!!");
    }

	void AdjustDragPositionInEdit ()
	{
		if (!IsAutoEmbedInEditor) return;

		transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
	}

	#region set/get
	public Vector3[] NeighbourPositions
	{
		get
		{ 
			return m_NeighbourPositions;
		}
	}
		
	#endregion

	void OnDestory()
	{
		Debug.Log ("Hexagon OnDestroy !!!");

		Hexagon[] hexagons = FindObjectsOfType<Hexagon> ();

		for (int i = 0; i < hexagons.Length; ++i) 
		{
			if (hexagons [i].Equals (this)) continue;

			foreach (Hexagon hex in hexagons[i].NeighbourList) 
			{
				if (hex.Equals(this))
				{
					hexagons[i].NeighbourList.Remove (hex);
					break;		
				}
			}
		}
	}
}

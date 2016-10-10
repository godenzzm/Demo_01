using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Hexagon : MonoBehaviour 
{
    public List<Hexagon> NeighbourList;    

    [SerializeField]
    private float Radius = 50; 

    private bool IsAutoEmbedInEditor = true;
    private Vector3[] m_NeighbourPositions;

    void Awake() 
    {
        InitMember();
    }

	// Use this for initialization
	void Start () 
    {
        RandomColor();
        AutoEmbedInEditor();
        InitNeighbourPosition();
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    void InitMember() 
    {
        NeighbourList = new List<Hexagon>();
    }

    void AutoEmbedInEditor () 
    {
        if (!IsAutoEmbedInEditor) return;

        Hexagon[] hexagons = FindObjectsOfType<Hexagon>();

        float shortDis_1 = 99999;
        int index = 0;

        for (int i = 0; i < hexagons.Length; ++i) 
        {
            float dis = Vector3.Distance(transform.position, hexagons[i].transform.position);

            if (shortDis_1 > dis && hexagons[i].IsNeighbourAvailable()) 
            { 
                shortDis_1 = dis;
                index = i;
            }
        }

        if (index < hexagons.Length)
        {
            EmbedAsNeighbour(hexagons[index]);
        }
    }

    void RandomColor() 
    {
        Renderer renderer = GetComponent<Renderer>();

        if (null != renderer && null != renderer.material) 
        {
            float randomR = Random.Range(0, 256);
            float randomG = Random.Range(0, 256);
            float randomB = Random.Range(0, 256);

            renderer.material.SetColor("_Color", new Color(randomR/255.0f, randomG/255.0f, randomB/255.0f));
        }
    }

    void EmbedAsNeighbour (Hexagon neighbour) 
    {
        Hexagon[] hexagons = FindObjectsOfType<Hexagon>();
        
    }

    //检测是否周围有邻居
    bool IsNeighbourAvailable() 
    {
        bool ret = false;

        if (null != m_NeighbourPositions && m_NeighbourPositions.Length < 6) ret = true;

        return ret;
    }

    void InitNeighbourPosition() 
    {
        float x = transform.position.x;
        float y = transform.position.y;

        m_NeighbourPositions = new Vector3[6];

        m_NeighbourPositions[0] = new Vector3(x, y + Mathf.Sqrt(3f) * Radius);
        m_NeighbourPositions[1] = new Vector3(x + 1.5f * Radius, y + Mathf.Sqrt(3f) / 2f * Radius);
        m_NeighbourPositions[2] = new Vector3(x + 1.5f * Radius, y - Mathf.Sqrt(3f) / 2f * Radius);
        m_NeighbourPositions[3] = new Vector3(x, y - Mathf.Sqrt(3f) * Radius);
        m_NeighbourPositions[4] = new Vector3(x - 1.5f * Radius, y - Mathf.Sqrt(3f) / 2f * Radius);
        m_NeighbourPositions[5] = new Vector3(x - 1.5f * Radius, y + Mathf.Sqrt(3f) / 2f * Radius);
    }
}

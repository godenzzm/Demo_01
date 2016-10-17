using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    private static GameManager m_Instance = null;

    [SerializeField]
    private MainCamera m_MainCamera;

	void Awake ()
	{
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static GameManager Instance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = GameObject.FindObjectOfType<GameManager>() as GameManager;
            }
            return m_Instance;
        }
    }

    public MainCamera MainCamera
    {
        get { return m_MainCamera; }
    }
}

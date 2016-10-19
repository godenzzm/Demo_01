using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    private static GameManager m_Instance = null;

    [SerializeField]
    private MainCamera m_MainCamera;

    private IGameMode m_GameMode;
    private GameInput m_GameInput;

	void Awake ()
	{
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start ()
    {
        m_GameMode = new GameModeAlpha();
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameRounding();
	}

    //游戏主回合轮询
    void GameRounding()
    {
        m_GameMode.Update();
    }

    #region get set
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

    public GameInput GameInput
    {
        get { return m_GameInput; }
        set { m_GameInput = value; }
    }
    #endregion
}

using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour 
{
	public Vector3 InitRotation = new Vector3 (45, 0, 0);
    [SerializeField]
	public float InitHeight;
    [SerializeField]
    public float MoveSpeed;
    [SerializeField]
    public float ZoomSpeed;

    private Camera m_Camera;

    void Awake()
    {
        InitHeight = 5;
        MoveSpeed = 10;
        ZoomSpeed = 10;
    }

	// Use this for initialization
	void Start () 
	{
        m_Camera = GetComponent<Camera>();
		transform.rotation = Quaternion.Euler (InitRotation);
		transform.position = new Vector3 (transform.position.x, InitHeight, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Move (Vector3 dir)
    {
        transform.position += dir * MoveSpeed;
    }

    public void ZoomInOut(float delta)
    {
        Debug.Log("sseee " + delta + " dsazz " + ZoomSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y + delta * ZoomSpeed, transform.position.z);
    }

    #region set get
    public Camera Camera
    {
        get { return m_Camera; }
    }
    #endregion
}

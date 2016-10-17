using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour 
{
	public Vector3 InitRotation = new Vector3 (45, 0, 0);
	public float InitHeight = 5;
    public float MoveSpeed;

    private Camera m_Camera;

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

    #region set get
    public Camera Camera
    {
        get { return m_Camera; }
    }
    #endregion
}

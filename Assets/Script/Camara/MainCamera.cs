using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour 
{
	public Vector3 InitRotation = new Vector3 (45, 0, 0);
	public float InitHeight = 10;
    public float MoveSpeed = 10;

	// Use this for initialization
	void Start () 
	{
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
}

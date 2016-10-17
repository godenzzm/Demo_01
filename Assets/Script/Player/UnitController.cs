using UnityEngine;
using System.Collections;

public class UnitController : Controller
{
    private Animator m_Animator;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Attack()
    {
        if (null != m_Animator)
        {
            m_Animator.SetTrigger ("Attack_01");
        }
    }
}

using UnityEngine;
using System.Collections;

public interface ICommand
{
    void UpdateCommand();
}

public class PlayerController : Controller 
{
	private Animator m_Animator;

	// Use this for initialization
	void Start () 
	{
		m_Animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AnimAttack ()
	{
		m_Animator.SetTrigger ("Attack_01");
	}

	public void AnimMove ()
	{
		m_Animator.SetFloat ("Speed", 1);
	}

	public void AnimStopMove ()
	{
		m_Animator.SetFloat ("Speed", 0);
	}
}

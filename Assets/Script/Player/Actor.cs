using UnityEngine;
using System.Collections;

/**
 *      接受处理场景中所有可操作物体事件，比如点击，碰撞等
 * 
 **/

public class Actor : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    #region Monobehaviour event
    void OnMouseEnter() { }

    void OnMouseOver() { }

    void OnMouseExit() { }

    void OnMouseDrag() { }

    void OnTriggerEnter() { }

    void OnTriggerStay() { }

    void OnTriggerExit() { }
    #endregion
}

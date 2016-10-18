using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    private UnitMovementData m_MovementData;
    // Use this for initialization
    void Start ()
    {
        m_MovementData = new UnitMovementData();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    #region get set
    public UnitMovementData MovementData
    {
        get { return m_MovementData; }
    }
    #endregion
}

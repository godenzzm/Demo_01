using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    #region physical data
    protected Quaternion m_SlerpRotationTarget; //平滑转向目标
    protected bool m_IsSlerpRotation = false; //是否进行平滑转向
    protected int m_SlerpRotationCount = 0;

    protected bool m_IsMoveToTarget = false;
    protected Vector3 m_TargetPosition;
    protected float m_MoveSpeed = 10;
    #endregion

    #region gameplay data

    #endregion

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	protected void Update ()
    {
        SlerpRotation();
        MovingToPosition();
    }

    public virtual void BeginSlerpRotation(Quaternion targetQuaternion, Vector3 originalForward)
    {
        //if (null == targetQuaternion || targetQuaternion == transform.rotation) return; //空值或已同向，返回

        transform.forward = originalForward;

        m_SlerpRotationTarget = targetQuaternion;
        m_IsSlerpRotation = true;
        m_SlerpRotationCount = 0;
    }

    void SlerpRotation()
    {
        if (!m_IsSlerpRotation) return;

        //if (m_SlerpRotationCount > 5) m_IsSlerpRotation = false;
        
        ++m_SlerpRotationCount;
        transform.rotation = Quaternion.Slerp(transform.rotation, m_SlerpRotationTarget, m_SlerpRotationCount / 10f );
        if (m_SlerpRotationCount >= 10) m_IsSlerpRotation = false;
    }

    public virtual void MoveTo(Vector3 TargetPosition)
    {
        m_TargetPosition = TargetPosition;
        m_IsMoveToTarget = true;
    }

    void MovingToPosition()
    {
        if (!m_IsMoveToTarget) return;

        if (Vector3.Distance(transform.position, m_TargetPosition) < 0.1f)
        {
            m_IsMoveToTarget = false;
        }

        Vector3 dir = (m_TargetPosition - transform.position).normalized;
        transform.position += dir * m_MoveSpeed * Time.deltaTime;
    }

    #region get set
    
    #endregion
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameInput : MonoBehaviour 
{
	public Image MouseGO;
    public float MoveEditPercent = 0.95f;
    public GameObject Prefab_FX_Click;

    public Controller TargetUnit;

    private int LayerMaskFloor;

    void Awake ()
	{
        LayerMaskFloor = LayerMask.NameToLayer("Floor");
    }

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update ()
    {
        InputMouseMove();
        InputMouseClick();
	}

    void InputMouseMove()
    {
        //Debug.Log("ScreenSize:" + Screen.width + " " + Screen.height + " Mouse Position:" + Input.mousePosition.x + " " + Input.mousePosition.y);
        //Debug.Log (MouseGO.transform.localPosition);

        float ScreenPercentX = Input.mousePosition.x / Screen.width;
        float ScreenPercentY = Input.mousePosition.y / Screen.height;
        float MouseX = (ScreenPercentX - 0.5f) * 1920f;
        float MouseY = (ScreenPercentY - 0.5f) * 1080f;

        //Debug.Log (ScreenPercentX + " " + ScreenPercentY);

        int ScrollX = 0;
        int ScrollY = 0;

        if (ScreenPercentX > MoveEditPercent)
        {
            MouseX = (MoveEditPercent - 0.5f) * 1920;
            ScrollX = 1;
        }
        else if (ScreenPercentX < (1 - MoveEditPercent))
        {
            MouseX = ((1 - MoveEditPercent) - 0.5f) * 1920;
            ScrollX = -1;
        }
        else { }

        if (ScreenPercentY > MoveEditPercent)
        {
            MouseY = (MoveEditPercent - 0.5f) * 1080;
            ScrollY = 1;
        }
        else if (ScreenPercentY < (1 - MoveEditPercent))
        {
            MouseY = ((1 - MoveEditPercent) - 0.5f) * 1080;
            ScrollY = -1;
        }
        else { }

        MouseGO.transform.localPosition = new Vector3(MouseX, MouseY, MouseGO.transform.localPosition.z);

        if (0 != ScrollX || 0 != ScrollY)
        {
            GameManager.Instance.MainCamera.Move(Time.deltaTime * new Vector3(ScrollX, 0, ScrollY));
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            GameManager.Instance.MainCamera.ZoomInOut(Time.deltaTime * Input.mouseScrollDelta.y);
        }
    }

    void InputMouseClick()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = GameManager.Instance.MainCamera.Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit, 100, 1 << LayerMaskFloor))
            {
                if (null != Prefab_FX_Click)
                {
                    Instantiate(Prefab_FX_Click, raycastHit.point, new Quaternion());

                    if (null != TargetUnit)
                    {
                        Vector3 origin_euler = transform.eulerAngles;
                        Vector3 origin_forward = transform.forward;
                        TargetUnit.transform.forward = new Vector3(raycastHit.point.x - TargetUnit.transform.position.x, 0, raycastHit.point.z - TargetUnit.transform.position.z);
                        TargetUnit.BeginSlerpRotation(Quaternion.Euler(origin_euler.x, TargetUnit.transform.rotation.eulerAngles.y, origin_euler.z), origin_forward);
                        TargetUnit.MoveTo(raycastHit.point);
                    }
                }
            }
        }
    }
}

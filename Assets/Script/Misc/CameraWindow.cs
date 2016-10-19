﻿using UnityEngine;
using System.Collections;

public class CameraWindow : MonoBehaviour
{
    Camera m_Camera;

    public GameObject m_TargetGO;
    public GameObject WindowSpace;

	// Use this for initialization
	void Start ()
    {
        GameObject GO = new GameObject("Camera Window");
        GO.AddComponent<Camera>();
        m_Camera = GO.GetComponent<Camera>();
        m_Camera.cullingMask = 1 << LayerMask.NameToLayer("Window");
        m_Camera.rect = new Rect(0, 0, 0.15f, 0.2f);
        m_Camera.clearFlags = CameraClearFlags.SolidColor;
        GO.transform.SetParent(WindowSpace.transform);
        GO.transform.localPosition = Vector3.zero;
        GO.transform.localScale = Vector3.one;

        m_TargetGO.transform.SetParent(GO.transform);
        m_TargetGO.transform.localPosition = new Vector3(0, -0.5f, 1);
        //m_TargetGO.transform.Rotate(new Vector3(0, 180, 0));
        m_TargetGO.transform.localScale = Vector3.one;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log("aaa " + transform.localPosition.y);
        UpdateCameraViewportRect();
    }

    void UpdateCameraViewportRect()
    {
        float localXInScreen = transform.localPosition.x;
        float localYInScreen = transform.localPosition.y;

        Transform parentTransform = transform.parent;
        while (parentTransform != null && parentTransform != UIManager.Instance.MainCanvas.RootPanel.transform)
        {
            localXInScreen += transform.parent.localPosition.x;
            localYInScreen += transform.parent.localPosition.y;

            parentTransform = parentTransform.parent;
        }

        Debug.Log(localXInScreen + " " + localYInScreen);

        float screenWidthPixel = 1920;
        float screenHeightPixel = 1080;

        RectTransform rt = GetComponent<RectTransform>();
        float rectX = (localXInScreen + screenWidthPixel * 0.5f) / screenWidthPixel;
        float rectY = (localYInScreen + screenHeightPixel * 0.5f) / screenHeightPixel;
        float rectW = rt.sizeDelta.x / screenWidthPixel;
        float rectH = rt.sizeDelta.y / screenHeightPixel;
        m_Camera.rect = new Rect(rectX, rectY, rectW, rectH);
    }
}
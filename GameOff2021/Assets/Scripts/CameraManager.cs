using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float offset = 2f;
    public CinemachineDollyCart dollyCart;
    public CinemachineVirtualCamera virtualCameraFocus;
    private CinemachineTrackedDolly trackedDolly;

    private float lastCheckpoint;

    private void Start()
    {
        trackedDolly = virtualCameraFocus.GetCinemachineComponent<CinemachineTrackedDolly>();
    }
    // Update is called once per frame
    void Update()
    {
        if(trackedDolly.m_PathPosition > dollyCart.m_Position)
        {
            ReFocus();
        }
    }
    public void ReFocus()
    {
        dollyCart.m_Position = trackedDolly.m_PathPosition;
    }
    public void SetPathCheckpoint()
    {
        lastCheckpoint = dollyCart.m_Position;
    }
    public void LoadPathCheckpoint()
    {
        dollyCart.m_Position = lastCheckpoint;
    }
}

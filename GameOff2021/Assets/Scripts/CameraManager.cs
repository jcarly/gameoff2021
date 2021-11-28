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

    public float lastCheckpoint;

    private void Start()
    {
        trackedDolly = virtualCameraFocus.GetCinemachineComponent<CinemachineTrackedDolly>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(trackedDolly.m_PathPosition > dollyCart.m_Position)
        {
            ReFocus();
        }
    }
    public void ReFocus() // Si la camera qui suit le joueur est plus loin sur le path que le cart, le cart se met à la bonne position pour continuer de suivre le joueur
    {
        dollyCart.m_Position = trackedDolly.m_PathPosition;
    }
    public void SetPathCheckpoint()
    {
        lastCheckpoint = dollyCart.m_Position;
    }
    public IEnumerator LoadPathCheckpoint()
    {
        yield return new WaitForSeconds(0.1f); // Alors là... si la fonction est pas une coroutine, ça se fait trop tôt donc ReFocus se fait avant que trackedDolly n'ait suivi le changement de position du player (enfin je crois)
        dollyCart.m_Position = lastCheckpoint;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float offset = 2f;
    public float cameraSpeed = 0.02f;

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.right * cameraSpeed * Time.timeScale;
    }
    public void ReFocus(float playerPosX)
    {
        this.transform.position = new Vector3(playerPosX + offset, transform.position.y, transform.position.z);
    }
}

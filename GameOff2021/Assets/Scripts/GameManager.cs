using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CameraManager cameraManager;
    public Vector2 lastCheckpoint;
    private static GameManager gameManagerInstance;

    public void Start()
    {
        Time.timeScale = 1f;
        DontDestroyOnLoad(this);

        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        cameraManager = FindObjectOfType<CameraManager>();
    }
    public void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        cameraManager = FindObjectOfType<CameraManager>();
    }
    public void Restart()
    {
        lastCheckpoint = Vector2.zero;
        PlayerDeath();
    }
    public void InvertView()
    {
        cameraManager.gameObject.transform.Rotate(cameraManager.gameObject.transform.forward, 180f);
    }

}

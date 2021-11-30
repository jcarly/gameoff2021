using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<EnemyController> enemies;
    [SerializeField]
    private CameraManager cameraManager;

    public void SetPlayerCheckpoint()
    {
        cameraManager.SetPathCheckpoint();
    }
    public void PlayerDeath()
    {
        StartCoroutine(cameraManager.LoadPathCheckpoint());
        foreach (EnemyController enemy in enemies)
        {
            enemy.Revive();
        }
    }
    public void InvertView()
    {
        cameraManager.gameObject.transform.Rotate(cameraManager.gameObject.transform.forward, 180f);
    }

}

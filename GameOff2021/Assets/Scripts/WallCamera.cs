using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCamera : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "player" :
                other.GetComponent<Player>().Death();
                break;
            case "projectile":
                Destroy(other.gameObject);
                break;
            case "enemy":
                other.GetComponent<EnemyController>().Death();
                break;
            case "activatable":
                other.GetComponent<EnemyController>().Activate();
                break;
            default:
                break;
        }
    }
}

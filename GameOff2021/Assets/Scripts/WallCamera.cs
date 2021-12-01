using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCamera : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        switch (other.gameObject.tag)
        {
            case "activatable":
                //other.GetComponent<EnemyController>().Activate();
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "player":
                other.GetComponent<Player>().Death();
                break;
            case "projectile":
                Destroy(other.gameObject);
                break;
            case "enemy":
                other.GetComponent<EnemyController>().Death();
                break;
            default:
                break;
        }
    }
}

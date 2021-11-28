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
            case "enemy":
                Destroy(other.gameObject);
                break;
            default:
                break;
        }
    }
}

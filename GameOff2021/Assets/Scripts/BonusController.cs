using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col) {
        Debug.Log("col bonus");
        switch(this.gameObject.name){
            case "Acceleration":
                col.gameObject.GetComponent<Player>().Accelerate();
                Destroy(this.gameObject);
                break;
            case "Deceleration":
                col.gameObject.GetComponent<Player>().Decelerate();
                Destroy(this.gameObject);
                break;
            case "Growth":
                col.gameObject.GetComponent<Player>().GetFat();
                Destroy(this.gameObject);
                break;
            case "Minimize":
                col.gameObject.GetComponent<Player>().GetThin();
                Destroy(this.gameObject);
                break;
            case "GravityChange":
                col.gameObject.GetComponent<Player>().ChangeGravity();
                Destroy(this.gameObject);
                break;
            case "ViewInversion":
                col.gameObject.GetComponent<Player>().InvertView();
                Destroy(this.gameObject);
                break;
            case "Freeze":
                col.gameObject.GetComponent<Player>().Freeze();
                Destroy(this.gameObject);
                break;
            case "TimeSlow":
                col.gameObject.GetComponent<Player>().SlowTime();
                Destroy(this.gameObject);
                break;
            case "TimeFast":
                col.gameObject.GetComponent<Player>().QuickenTime();
                Destroy(this.gameObject);
                break;
            case "LessAttSpeed":
                col.gameObject.GetComponent<Player>().ReduceAttackSpeed();
                Destroy(this.gameObject);
                break;
            case "MoreAttSpeed":
                col.gameObject.GetComponent<Player>().IncreaseAttackSpeed();
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}

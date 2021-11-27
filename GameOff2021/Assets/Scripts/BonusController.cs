using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BonusType {Acceleration,
                       Deceleration,
                       Freeze,
                       GravityChange,
                       LessAttackSpeed,
                       MoreAttackSpeed,
                       Growth,
                       Minimize,
                       AccTime,
                       DecTime,
                       ViewInversion};


public class BonusController : MonoBehaviour
{

    public BonusType type; 

    // Start is called before the first frame update
    void Start()
    {
        //Put splashArt here with switchcase
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col) {
        Debug.Log("col bonus");
        switch(this.type){
            case BonusType.Acceleration:
                col.gameObject.GetComponent<Player>().Accelerate();
                Destroy(this.gameObject);
                break;
            case BonusType.Deceleration:
                col.gameObject.GetComponent<Player>().Decelerate();
                Destroy(this.gameObject);
                break;
            case BonusType.Growth:
                col.gameObject.GetComponent<Player>().GetFat();
                Destroy(this.gameObject);
                break;
            case BonusType.Minimize:
                col.gameObject.GetComponent<Player>().GetThin();
                Destroy(this.gameObject);
                break;
            case BonusType.GravityChange:
                col.gameObject.GetComponent<Player>().ChangeGravity();
                Destroy(this.gameObject);
                break;
            case BonusType.ViewInversion:
                col.gameObject.GetComponent<Player>().InvertView();
                Destroy(this.gameObject);
                break;
            case BonusType.Freeze:
                col.gameObject.GetComponent<Player>().Freeze();
                Destroy(this.gameObject);
                break;
            case BonusType.DecTime:
                col.gameObject.GetComponent<Player>().SlowTime();
                Destroy(this.gameObject);
                break;
            case BonusType.AccTime:
                col.gameObject.GetComponent<Player>().QuickenTime();
                Destroy(this.gameObject);
                break;
            case BonusType.LessAttackSpeed:
                col.gameObject.GetComponent<Player>().ReduceAttackSpeed();
                Destroy(this.gameObject);
                break;
            case BonusType.MoreAttackSpeed:
                col.gameObject.GetComponent<Player>().IncreaseAttackSpeed();
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}

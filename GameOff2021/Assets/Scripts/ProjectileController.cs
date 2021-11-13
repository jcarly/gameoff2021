using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private GameObject shooter = null;
    // Start is called before the first frame update

    public void setShooter(GameObject shooter){
        this.shooter = shooter;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col) {
        if(!col.gameObject.Equals(shooter)) {
            GameObject.Destroy(this.gameObject);
        }
    }
}
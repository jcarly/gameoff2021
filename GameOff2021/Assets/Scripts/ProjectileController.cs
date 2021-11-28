using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject shooter = null;
    public Vector2 direction;
    public float speed;
    // Start is called before the first frame update

    public void setShooter(GameObject shooter){
        this.shooter = shooter;
        Physics2D.IgnoreCollision(shooter.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(speed * direction.x, speed * direction.y, 0);

    }

    private void OnCollisionEnter2D(Collision2D col) {
        GameObject.Destroy(this.gameObject);
    }
}

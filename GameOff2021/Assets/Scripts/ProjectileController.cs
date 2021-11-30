using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject shooter = null;
    public Vector2 direction;
    public float speed;
    public float range = 20f;
    private float origin;
    // Start is called before the first frame update

    public void setShooter(GameObject shooter){
        this.shooter = shooter;
        Physics2D.IgnoreCollision(shooter.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }


    void Start()
    {
        origin = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(speed * direction.x, speed * direction.y, 0);
        if(Mathf.Abs(transform.position.x - origin) > range)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        Destroy(this.gameObject);
    }
}

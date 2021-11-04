using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    Transform tr;
    public KeyCode jump;
    //Transform transform;
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        tr = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(jump))
        {
            rb.AddForce(Vector3.up * 25f);
            tr.up = Vector3.Lerp(transform.up, Vector3.up, 0.2f);
        }
        else
        {
            //rb.velocity = new Vector3(0, -2f);
        }
    }
}

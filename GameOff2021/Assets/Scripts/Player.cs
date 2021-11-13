using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private Transform tr;

    public KeyCode jumpKey;
    public float jumpForce = 25f;
    public float speed = 5f;
    [SerializeField]
    private CameraManager cameraManager;

    public float outOfView;

    //Transform transform;
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        tr = this.gameObject.GetComponent<Transform>();
        cameraManager = FindObjectOfType<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(jumpKey))
        {
            rb.AddForce(Vector3.up * jumpForce + Vector3.right * speed);

            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, 0f, 2f), rb.velocity.y, rb.velocity.z);
            tr.up = Vector3.Lerp(transform.up, Vector3.up, 0.2f);
        }
        if (cameraManager.transform.position.x - cameraManager.offset < this.transform.position.x)
        {
            cameraManager.ReFocus(this.transform.position.x);
        }
        if(cameraManager.transform.position.x - outOfView > this.transform.position.x)
        {
            Debug.Log("Perdu");
            Destroy(this.gameObject);
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision col) {
        Rigidbody rbody = this.GetComponent<Rigidbody>();
        switch(col.gameObject.tag){ 
            case "deadly":
                GameObject.Destroy(this.gameObject);
                break;
            case "bouncy":
                Vector3 velocity = rbody.velocity; //Vitesse du player
                rbody.velocity = new Vector3(0, 0, 0);//Reset velocity
                rbody.AddForce(Vector3.Reflect(velocity, col.contacts[0].normal * (float)Math.Sqrt(velocity.magnitude)), ForceMode.Impulse);
                Debug.Log("BOUCY !!");
                break;        
            case "projectile":
                break;
            default:
                break;
        }
    }
}

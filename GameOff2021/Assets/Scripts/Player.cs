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
}

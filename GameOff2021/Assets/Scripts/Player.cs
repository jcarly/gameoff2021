using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Transform tr;

    public KeyCode jumpKey;
    [SerializeField]
    float jumpForce = 25f;
    [SerializeField]
    float speedLimiter = 2f;
    [SerializeField]
    float speedMin = 1f;
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float acceleration = 3f;
    [SerializeField]
    float nbBurger = 1.5f; // How much the size augment, and the mass
    private static CameraManager cameraManager;
    [SerializeField]
    float outOfView;

    [SerializeField]
    Transform projectileContainer;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    float projectileSpeed = 10f;
    [SerializeField]
    GameObject firingPoint;
    [SerializeField]
    float attackSpeed;

    [SerializeField]
    float bouncyness = 3f;

    [SerializeField]
    Transform lastCheckpoint;

    private static Player playerInstance;

    private Vector3 newScale;


    //Transform transform;
    void Start()
    {
        DontDestroyOnLoad(this);

        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        tr = this.gameObject.GetComponent<Transform>();
        cameraManager = FindObjectOfType<CameraManager>();
        newScale = tr.localScale;

        StartCoroutine(AutoAttack());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, speedMin, speedLimiter * speed), rb.velocity.y);
        if(this.newScale != tr.localScale)
        {
            tr.localScale = Vector3.Lerp(tr.localScale, this.newScale, 10 * Time.deltaTime);
        }
        Vector3.Lerp(tr.localScale, tr.localScale / nbBurger, 10 * Time.deltaTime);
        if (Input.GetKey(jumpKey))
        {
            //rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
            //rb.velocity = new Vector3(0, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce + Vector3.right * speed,ForceMode2D.Force);
            //tr.up = Vector3.Lerp(transform.up, Vector3.up, 0.2f);
        }
        if (cameraManager.transform.position.x - cameraManager.offset < this.transform.position.x)
        {
            cameraManager.ReFocus(this.transform.position.x);
        }
        if(cameraManager.transform.position.x - outOfView > this.transform.position.x)
        {
            Death();
        }

        // TODO erase these function call when bonuses are reel
        if (Input.GetKeyDown(KeyCode.D))
        {
            Death();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Accelerate();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Decelerate();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetFat();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            GetThin();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeGravity();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            InvertView();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(FreezePosition());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SlowTime();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuickenTime();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Checkpoint(this.transform);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            IncreaseAttackSpeed();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReduceAttackSpeed();
        }
    }
    public void Accelerate()
    {
        this.speed *= acceleration;
    }
    public void Decelerate()
    {
        this.speed /= acceleration;
    }
    public void GetFat()
    {
        rb.mass *= nbBurger;
        this.newScale = tr.localScale * nbBurger;
    }
    public void GetThin()
    {
        rb.mass /= nbBurger;
        this.newScale = tr.localScale / nbBurger;
    }
    public void ChangeGravity()
    {
        Physics.gravity *= -1;
        jumpForce *= -1;
    }
    public void InvertView()
    {
        cameraManager.gameObject.transform.Rotate(cameraManager.gameObject.transform.forward, 180f);
    }
    private IEnumerator FreezePosition()
    {
        GameObject freezedObject = Instantiate(this.gameObject, transform.position, Quaternion.identity, this.transform.parent);
        Destroy(freezedObject.GetComponent<Player>());
        Destroy(freezedObject.GetComponent<Rigidbody2D>());

        MeshRenderer meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;

        Collider collider = this.GetComponent<Collider>();
        collider.isTrigger = true;

        float freezeDuration = 1f;
        yield return new WaitForSeconds(freezeDuration);

        collider.isTrigger = false;
        meshRenderer.enabled = true;
        Destroy(freezedObject);
    }

    public void Freeze(){
        StartCoroutine(FreezePosition());
    } 

    // If too slow, the game is frame by frame
    public void SlowTime()
    {
        Time.timeScale /= 2f;
    }
    public void QuickenTime()
    {
        Time.timeScale *= 2f;
    }
    // Maybe change to Vector2
    public void Checkpoint(Transform checkpoint)
    {
        lastCheckpoint = checkpoint;
    }
    public IEnumerator AutoAttack()
    {
        while (attackSpeed > 0)
        {
            Attack();
            yield return new WaitForSeconds(1 / attackSpeed);
        }
    }
    public void Attack()
    {
        GameObject launchedProjectile = Instantiate(projectile,firingPoint.transform.position,projectile.transform.rotation, projectileContainer);
        launchedProjectile.GetComponent<Rigidbody2D>().AddForce(Vector3.right*projectileSpeed, ForceMode2D.Impulse);
    }
    public void IncreaseAttackSpeed()
    {
        attackSpeed += 1f;
    }
    public void ReduceAttackSpeed()
    {
        if(attackSpeed > 1f)
            attackSpeed -= 1f;
    }
    public void Death()
    {
        if (lastCheckpoint != null)
        {
            this.transform.position = lastCheckpoint.position;// And move the camera there, and the camera stop moving, and start when the player moves
            cameraManager.ReFocus(this.transform.position.x);
        }
        else {
            Destroy(this.gameObject);
            Destroy(this);
            // LoadScene(sceneMenu) or lastCheckpoint = startPosition
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Perdu");
    }

    private void OnCollisionEnter2D(Collision2D col) {
        Rigidbody2D rbody = this.GetComponent<Rigidbody2D>();
        Debug.Log(col.gameObject.tag);
        switch(col.gameObject.tag){ 
            case "deadly":
                Death();
                break;
            case "bouncy":
                Vector3 velocity = rbody.velocity; //Vitesse du player
                rbody.velocity = new Vector3(0, 0, 0);//Reset velocity
                rbody.AddForce(bouncyness * Vector3.Reflect(velocity, col.contacts[0].normal * (float)Math.Sqrt(velocity.magnitude)), ForceMode2D.Impulse);
                Debug.Log("BOUCY !!");
                break;
            case "projectile":
                Death();
                break;
            case "checkpoint":
                Checkpoint(col.transform);
                break;
            default:
                break;
        }
    }
}

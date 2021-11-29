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
    [SerializeField]
    float jumpForce = 25f;
    [SerializeField]
    float speedLimiter = 2f;
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float acceleration = 3f;
    [SerializeField]
    float nbBurger = 1.5f; // How much the size augment, and the mass
    private GameManager gameManager;
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

    Vector3 lastCheckpoint;
    

    //Transform transform;
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        tr = this.gameObject.GetComponent<Transform>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.SetPlayerCheckpoint();

        StartCoroutine(AutoAttack());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(jumpKey))
        {
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, 0f, speedLimiter* speed), rb.velocity.y, rb.velocity.z);
            //rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
            //rb.velocity = new Vector3(0, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce + Vector3.right * speed,ForceMode.Force);
            //tr.up = Vector3.Lerp(transform.up, Vector3.up, 0.2f);
        }

        // TODO erase these function call when bonuses are reel
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
            Checkpoint(this.transform.position);
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
        tr.localScale *= nbBurger;
    }
    public void GetThin()
    {
        rb.mass /= nbBurger;
        tr.localScale /= nbBurger;
    }
    public void ChangeGravity()
    {
        Physics.gravity *= -1;
        jumpForce *= -1;
    }
    public void InvertView()
    {
        gameManager.InvertView();
    }
    private IEnumerator FreezePosition()
    {
        GameObject freezedObject = Instantiate(this.gameObject, transform.position, Quaternion.identity, this.transform.parent);
        Destroy(freezedObject.GetComponent<Player>());
        Destroy(freezedObject.GetComponent<Rigidbody>());

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
    public void Checkpoint(Vector3 position)
    {
        lastCheckpoint = position;
        gameManager.SetPlayerCheckpoint();
    }
    public IEnumerator AutoAttack()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(1 / attackSpeed);
        }
    }
    public void Attack()
    {
        GameObject launchedProjectile = Instantiate(projectile,firingPoint.transform.position,projectile.transform.rotation, projectileContainer);
        launchedProjectile.GetComponent<Rigidbody>().AddForce(Vector3.right*projectileSpeed, ForceMode.Impulse);
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
            this.transform.position = lastCheckpoint;// And move the camera there, and the camera stop moving, and start when the player moves
            rb.velocity = Vector3.zero;
            gameManager.PlayerDeath();
        }
        else
        {
            Destroy(this.gameObject);
            Destroy(this);
            // LoadScene(sceneMenu) or lastCheckpoint = startPosition
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        switch(col.gameObject.tag){ 
            case "deadly":
                //GameObject.Destroy(this.gameObject);
                Death();
                break;
            case "bouncy":
                Vector3 velocity = rb.velocity; //Vitesse du player
                rb.velocity = new Vector3(0, 0, 0);//Reset velocity
                rb.AddForce(Vector3.Reflect(velocity, col.contacts[0].normal * (float)Math.Sqrt(velocity.magnitude)), ForceMode.Impulse);
                Debug.Log("BOUCY !!");
                break;        
            case "projectile":
                Death();
                break;
            default:
                break;
        }
    }
}

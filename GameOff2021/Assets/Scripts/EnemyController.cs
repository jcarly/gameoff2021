using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    /*------Attributes------*/
    public int baseHp = 3;//Nombre de coups nécessaire pour tuer l'ennemi
    private int hp = 3; // Vie actuelle de l'ennemi
    [SerializeField] private float attackSpeed = 0.75f; //Nombre d'attaques par seconde
    private float fireRateDelay; //Délai avant prochaine attaque

    public GameObject projectile = null;
    private GameObject player;

    private bool activated;

    /*------Internal functions------*/
    //Fonction tirant un projectile sur le joueur
    void Fire(){
        //Cas Joueur non trouvé
        if (player == null){ 
            Debug.Log("no target");
            return;
        }

        Vector3 firePoint = this.gameObject.transform.GetChild(0).gameObject.transform.position; //Point d'où est tiré le projectile (sinon spawn à l'interieur)

        Vector3 targetDirection = (player.transform.position - firePoint); //Direction du tir
        float module = Mathf.Sqrt(targetDirection.x*targetDirection.x + targetDirection.y*targetDirection.y + targetDirection.z*targetDirection.z);
        targetDirection *= 1/module*0.5f; //Pour avoir une vitesse de projectile constante
    
        //Tir
        GameObject launchedProjectile = Instantiate(projectile, firePoint, this.transform.rotation);
        launchedProjectile.GetComponent<ProjectileController>().setShooter(this.gameObject);
        launchedProjectile.GetComponent<ProjectileController>().direction = Vector2.right;
        launchedProjectile.GetComponent<ProjectileController>().speed = attackSpeed;
    }

    public IEnumerator AutoAttack()
    {
        while (attackSpeed > 0)
        {
            Fire();
            yield return new WaitForSeconds(1 / attackSpeed);
        }
    }


    /*------Unity Functions------*/
    // Start is called before the first frame update
    public void Activate()
    {
        activated = true;
        this.gameObject.tag = "enemy";
        player = GameObject.FindWithTag("player");
        hp = baseHp;
        StopAllCoroutines();
        StartCoroutine(AutoAttack()); //Commencement de l'auto-attaque
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (player == null)
            {
                Debug.Log("no target");
            }
            else
            {
                transform.LookAt(player.transform); //on tourne vers le joueur
                //transform.rotation = Quaternion.LookRotation(player.transform.position - this.transform.position); //on tourne vers le joueur
            }
        }
    }

    //Actions to do on destroy
    public void Death()
    {
        Destroy(this.gameObject);
    }
    public void Revive()
    {
        StopAllCoroutines();
        activated = false;
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<Collider>().enabled = true;
        this.gameObject.tag = "activatable";
    }

    private void OnCollisionEnter2D(Collision2D col) {
        Debug.Log(col.gameObject.tag);
        switch(col.gameObject.tag){ //Cas ou reception d'un tir
            case "projectile" :
                hp -= 1;
                if (hp <= 0)
                {
                    Death();
                }
                break;

            case "deadly": //collision avec quelque chose de mortel
                Death();
                break;

            default: //Mur normal
                break;
        }
    }
}

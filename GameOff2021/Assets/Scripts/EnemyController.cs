using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

/*------Attributes------*/
    public int hp = 3; //Nombre de coups nécessaire pour tuer l'ennemi
    [SerializeField] private float attackSpeed = 0.75f; //Nombre d'attaques par seconde
    private float fireRateDelay; //Délai avant prochaine attaque

    public GameObject projectile = null;
    private GameObject player;


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
    GameObject launched = Instantiate(projectile, firePoint, this.transform.rotation);
    launched.GetComponent<ProjectileController>().setShooter(this.gameObject);
    launched.GetComponent<Rigidbody>().AddForce(targetDirection, ForceMode.Impulse);
}

public IEnumerator AutoAttack()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(1 / attackSpeed);
        }
    }


/*------Unity Functions------*/
    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindWithTag("player");
        StartCoroutine(AutoAttack()); //Commencement de l'auto-attaque
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(player.transform.position - this.transform.position); //on tourne vers le joueur
    }

    //Actions to do on destroy
    private void OnDestroy() {
        
    }

    private void OnCollisionEnter(Collision col) {
        switch(col.gameObject.tag){ //Cas ou reception d'un tir
            case "projectile" :
                hp -= 1;
                if (hp <=0) {
                    Destroy(this.gameObject);
                } 
                break;

            case "deadly": //collision avec quelque chose de mortel
                Destroy(this.gameObject);
                break;

            default : //Mur normal
                break;
        }
    }
}

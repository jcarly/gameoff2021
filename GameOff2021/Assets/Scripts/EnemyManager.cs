using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int hp = 3; //Nombre de coups nécessaire pour tuer l'ennemi
    [SerializeField] private float fireRate = 0.75f;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Actions to do on destroy
    private void OnDestroy() {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    ////This code checks for a collision with an object named "Zombie" 
    //and if it collides, it calls the TakeDamage function from the EnemyAi script and passes in 5 as the damage value.
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Zombie")
        {
            col.gameObject.GetComponent<EnemyAi>().TakeDamage(5);
        }
    }
    
}

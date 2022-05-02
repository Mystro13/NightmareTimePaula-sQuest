using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackScript : MonoBehaviour
{
   public int healthDamage = 1;
   private void OnCollisionEnter(Collision collision)
   {
      if(collision.gameObject.CompareTag(Tags.enemies.ToString()))
      {
         EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();
         if(enemyHealth)
         {
            //enemyHealth.DepleteHealth(healthDamage);
            enemyHealth.GetKilled();
         }
      }
   }
}

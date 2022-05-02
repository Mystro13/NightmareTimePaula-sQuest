using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
   public int healthDamage = 1;
   private void OnTriggerEnter(Collider other)
   {
      if(other.gameObject.CompareTag(Tags.enemies.ToString()))
      {
         EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
         if(enemyHealth)
         {
            enemyHealth.DepleteHealth(healthDamage);
         }
      }
   }
}

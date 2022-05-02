using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
   [SerializeField]
   private int damageToPlayer;
 
   void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag(Tags.Player.ToString()))
      {
         PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
         playerHealth.TrapDamage(damageToPlayer);

         GameObject trapParent = gameObject.transform.parent.gameObject;
         CompositeInteractionsScript compositeInteractionsScript = trapParent.GetComponentInChildren<CompositeInteractionsScript>();
         if(compositeInteractionsScript)
         {
            compositeInteractionsScript.Execute();
         }
      }
   }
}

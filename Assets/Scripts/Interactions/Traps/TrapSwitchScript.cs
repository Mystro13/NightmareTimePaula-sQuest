using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSwitchScript : MonoBehaviour
{
   public GameObject targetTrap;

   public void Execute()
   {
      DesActivate();
   }

   private void OnCollisionEnter(Collision collision)
   {
      if(collision.gameObject.CompareTag(Tags.Projectile.ToString()))
      {
         DesActivate();
      }
   }

   void DesActivate()
   {
      if (targetTrap)
      {
         targetTrap.SetActive(false);
      }
   }
}

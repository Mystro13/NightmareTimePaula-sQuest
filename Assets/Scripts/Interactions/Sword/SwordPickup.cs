using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickup : MonoBehaviour
{
   public PlayerHealth playerHealth;
   private GameObject sword;
   public void Execute()
   {
      sword = GameObject.Find("Sword");
      if (sword)
      {
         sword.SetActive(false);
      }
   }
}

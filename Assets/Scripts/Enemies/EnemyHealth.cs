using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
   [SerializeField]
   public Image healthBar;
   [SerializeField]
   float health = 10f;
   float maximumHealth;
   [SerializeField]
   EnemyStockUp enemyStockUp;

   private void Awake()
   {
      maximumHealth = health;
      enemyStockUp = gameObject.GetComponent<EnemyStockUp>();
      enemyStockUp.StockUp();
   }
   void Update()
   {
      UpdateHUD();

      if (health <= 0)
      {
         Death();
      }
   }
   void UpdateHUD()
   {
      if (healthBar)
      {
         healthBar.fillAmount = health / maximumHealth;
      }
   }

   public void DepleteHealth(int healthDamage)
   {
      health -= healthDamage;
   }

   void Death()
   {
      Debug.Log($"Death {enemyStockUp.name}");
      enemyStockUp.DropItems();
      Destroy(gameObject);
   }

   public void GetKilled()
   {
      Death();
   }
}

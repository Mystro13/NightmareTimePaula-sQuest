using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
   public Image healthBar;
   public int maximumHealth = 1000;
   public int health;
   GameObject battleSword;
   Animator animator;
   int isDeadHash;
   public float deathAnimationTime = 2.6f;
   bool isDying = false;
   float deathTimer = 0f;
   bool isDeathTimerReached = false;
   // Start is called before the first frame update
   void Start()
   {
      animator = GetComponent<Animator>();
      isDeadHash = Animator.StringToHash("Death");
      health = maximumHealth;
      HideSword();
      //SceneLoader.instance.AssignPlayer(gameObject);
      AudioManager.instance.Play(GameSounds.SceneBackground);
      if (SceneLoader.instance.playerHasPickedSword)
      {
         ShowSword();
      }
   }

   //Update is called once per frame
   void Update()
   {
      if (Time.timeScale == 0f)
      {
         return;
      }
      UpdateHUD();

      if (health <= 0)
      {
         GetKilled();
      }
   }
   void UpdateHUD()
   {
      if (healthBar)
      {
         healthBar.fillAmount = (float)health / (float)maximumHealth;
      }
   }

   public void DepleteHealth()
   {
      health -= 1;
      if (health <= -1)
      {
         health = 0;
      }
      Debug.Log("PLAYER IS DAMAGED");
   }

   public void TrapDamage(int blow)
   {
      health -= blow;
      if (health < 0)
      {
         health = 0;
      }
   }
   public void GetKilled()
   {
      if(!isDying)
      {
         deathTimer = 0f;
         Debug.Log("PLAYER IS KILLED");
         animator.SetTrigger(isDeadHash);
         isDying = true;
      }
      else
      {
         if (!isDeathTimerReached)
         {
            deathTimer += Time.deltaTime;
         }

         if (!isDeathTimerReached && deathTimer > (deathAnimationTime+1f))
         {
            Debug.Log("Done waiting death");
            isDeathTimerReached = true;
            GameObject.Destroy(gameObject);
            SceneLoader.instance.Load(SceneLoader.SceneType.Cemetery);
         }
      }
      
   }

   public void BoostHealth()
   {
      health += 1;
   }

   void HideSword()
   {
      battleSword = GameObject.Find("BattleSword");
      if (battleSword)
      {
         battleSword.SetActive(false);
      }
   }
   public void ShowSword()
   {
      if (battleSword)
      {
         battleSword.SetActive(true);
      }
   }
}
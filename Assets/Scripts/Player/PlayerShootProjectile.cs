using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootProjectile : MonoBehaviour
{
   public float projectileSpeed = 30;
   public GameObject projectile;
   float nextShot = 0.0f;
   float timeBetweenShots = 0.2f;
   float projectileLifetime = 2f;


   private void Awake()
   {
      GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
   }
   void OnDestroy()
   {
      GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
   }
   private void OnGameStateChanged(GameState newGameState)
   {
      enabled = (newGameState == GameState.GamePlay);
   }
   void Fire()
   {
      if (!SceneLoader.instance.playerHasPickedSword)
      {
         return;
      }
      PlayerInteraction playerInteraction = gameObject.GetComponent<PlayerInteraction>();
      if (playerInteraction)
      {
         //if (playerInteraction.manaSlot.slotItem)  
         {

            Vector3 barrelEnd = transform.position + transform.forward * 1f + new Vector3(0, 2f, 0);
            foreach (Transform child in gameObject.transform.GetComponentsInChildren<Transform>())
            {
               if (child.name == "MagicPoint")
               {
                  //magicPoint = child;
                  barrelEnd = child.position;
                  break;
               }
            }
            //if (magicPoint.name == "MagicPoint")
            //{
            //   barrelEnd = magicPoint.position;
            //}
            //GameObject projectileClone = Instantiate(projectile, swordGrip.transform.position, swordGrip.transform.rotation);
            GameObject projectileClone = Instantiate(projectile, barrelEnd, transform.rotation);
            //projectileClone.transform.position = new Vector3(projectileClone.transform.position.x, projectileClone.transform.position.y * 0.1f, projectileClone.transform.position.z);
            //projectileClone.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
            //Set speed
            projectileClone.GetComponent<Projectile>().projectileSpeed = projectileSpeed;
            //playerInteraction.manaSlot.RemoveItem();
            //coolDown = Time.time + projectileSpeed;
            Destroy(projectileClone, projectileLifetime);
         }
      }
   }

   void Update()
   {
      if (Time.timeScale == 0f)
      {
         return;
      }
      Keyboard keyboard = Keyboard.current;
      Mouse mouse = Mouse.current;
      if (Time.time > nextShot)
      {
         if (mouse.leftButton.ReadValue() > 0f)
         {
            nextShot = Time.time + timeBetweenShots;
            Fire();
         }
      }
   }
}

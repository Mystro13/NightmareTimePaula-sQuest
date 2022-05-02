using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInteraction : MonoBehaviour
{
   public GameObject currentInteractionObject = null;
   public InteractionObject currentInteractionScript = null;
   [SerializeField] private GameObject interactionIcon;
   public ItemSlot keySlot;
   public ItemSlot manaSlot;
   //public ItemSlot ammoSlot;
   public ItemSlot healthPotionSlot;
   public ItemSlot permaHealthSlot;
   public int boostHealth = 1;
   void Start()
   {
   }

   void Update()
   {
      Keyboard keyboard = Keyboard.current;
      Mouse mouse = Mouse.current;
      if (currentInteractionScript && currentInteractionObject)
      {
         bool automaticPickup = (currentInteractionScript.interactionType == InteractionType.Dropping);
         if (automaticPickup || (keyboard.eKey.wasPressedThisFrame) && currentInteractionObject)
         {
            bool playAdditionalInteractions = false;
            switch (currentInteractionScript.interactionType)
            {
               case InteractionType.PickingSword:
                  {
                     DoPickSword(currentInteractionObject);
                     playAdditionalInteractions = true;
                     break;
                  }

               case InteractionType.Picking:
               case InteractionType.Dropping:
                  {
                     currentInteractionObject.SetActive(false);
                     if (!TryAddItem(currentInteractionObject))
                     {
                        currentInteractionScript.DoFlashAndDie();
                     }

                     var bCol = currentInteractionObject.AddComponent<BoxCollider>();
                     bCol.enabled = false;
                     bCol.isTrigger = false;

                     var comp = currentInteractionObject.GetComponent<InteractionObject>();
                     if(comp != null)
                     {
                        comp.enabled = false;
                     }
                     currentInteractionScript = null;
                     break;
                  }
               case InteractionType.Entry:
                  {
                     if (currentInteractionScript.entryIsLocked)
                     {
                        DoUnlockEntry(currentInteractionScript);
                        playAdditionalInteractions = true;
                     }
                     else if (!currentInteractionScript.entryIsOpened)
                     {
                        DoOpenEntry(currentInteractionScript);
                        playAdditionalInteractions = true;
                     }

                     break;
                  }
               case InteractionType.Dialog:
                  {
                     playAdditionalInteractions = true;
                     break;
                  }
               case InteractionType.Trap:
                  {
                     DoDesactivateTrap();
                     break;
                  }
            }
            if (playAdditionalInteractions)
            {
               if (currentInteractionScript)
               {
                  currentInteractionScript.DoAdditionalInteraction();
               }
            }
         }
      }
      if (keyboard.digit1Key.wasPressedThisFrame)
      {
         DoBoostHealth();
      }
      else if (keyboard.digit2Key.wasPressedThisFrame)
      {

      }
   }

   private void DoDesactivateTrap()
   {
      TrapSwitchScript trapSwitchScript = currentInteractionObject.GetComponent<TrapSwitchScript>();
      if(trapSwitchScript)
      {
         trapSwitchScript.Execute();
      }
   }

   private void DoBoostHealth()
   {
      //Search the interaction inventory for the item needed - of found unlock object
      if (healthPotionSlot.slotItem != null)
      {
         healthPotionSlot.RemoveItem();
         PlayerHealth playerHealth = gameObject.GetComponent<PlayerHealth>();
         if(playerHealth)
         {
            playerHealth.BoostHealth();
         }
      }
   }

   private void DoOpenEntry(InteractionObject currentInteractionScript)
   {
      currentInteractionScript.entryIsOpened = true;
      currentInteractionScript.OpenEntry();
   }

   private void DoUnlockEntry(InteractionObject currentInteractionScript)
   {
      //check to see if we have the object needed to unlock the interactable that is locked
      if (currentInteractionScript.neededInteractionObject != null)
      {
         //Search the interaction inventory for the item needed - of found unlock object
         StockItem keyItem = keySlot.GetComponent<StockItem>();
         if (keyItem.quantity > 0)
         {
            //item needed is found
            currentInteractionScript.entryIsLocked = false;
            Debug.Log($"{currentInteractionObject.name} was unlocked!");
            keySlot.RemoveItem();
         }
         else
         {
            Debug.Log($"{currentInteractionObject.name} cannot be unlocked!");
         }
      }
      else
      {
         //no item is needed to interact with this object.
         currentInteractionScript.entryIsLocked = false;
         Debug.Log($"{currentInteractionObject.name} was unlocked!");
      }
   }

   void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag(Tags.interactionObject.ToString()))
      {
         Debug.Log($"Player detect interaction object {other.name}");
         currentInteractionObject = other.gameObject;
         currentInteractionScript = other.gameObject.GetComponent<InteractionObject>();
         Debug.Log($"interaction object type {currentInteractionScript.interactionType}");
         if (currentInteractionScript)
         {
            if(currentInteractionScript.interactionFeedback)
            {
               if(!string.IsNullOrEmpty(currentInteractionScript.interactionDialog))
               {
                  currentInteractionScript.interactionFeedback.text = currentInteractionScript.interactionDialog;
               }
            }
         }
      }
   }
   void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag(Tags.Traps.ToString()))
      {
         Debug.Log("Traps");
      }
   }
   void OnTriggerExit(Collider other)
   {
      if (other.CompareTag(Tags.interactionObject.ToString()))
      {
         Debug.Log($"");
         if (other.gameObject == currentInteractionObject)
         {
            Debug.Log($"Player is out of range from object {other.name}");
            currentInteractionObject = null;
         }
      }
   }

   bool TryAddItem(GameObject itemObject)
   {
      return AddToSlot(keySlot, itemObject) ||
      AddToSlot(manaSlot, itemObject) ||
      //AddToSlot(ammoSlot, itemObject) ||
      AddToSlot(healthPotionSlot, itemObject) ||
      AddToSlot(permaHealthSlot, itemObject);
   }

   bool AddToSlot(ItemSlot slot, GameObject itemObject)
   {
      if (slot)
      {
         return slot.AddItem(itemObject);
      }
      return false;
   }

   void DoPickSword(GameObject itemObject)
   {
      if(!itemObject)
      {
         currentInteractionScript = null;
         return;
      }
      SwordPickup swordPickup = itemObject.GetComponent<SwordPickup>();
      if (swordPickup)
      {
         Debug.Log("Sword pick up");
         if (swordPickup.playerHealth)
         {
            SceneLoader.instance.playerHasPickedSword = true;
            swordPickup.Execute();
            swordPickup.playerHealth.ShowSword();
         }
         Debug.Log("Spawning");

         GameObject enemies = GameObject.Find("Enemies");
         if (enemies)
         {
            Debug.Log("Enemies");
            foreach (Transform child in enemies.transform.GetComponentsInChildren<Transform>())
            {
               Debug.Log($"\t{child.name}");
               if (child.gameObject.CompareTag(Tags.enemies.ToString()))
               {
                  Debug.Log($"\tTag \t{child.name}");
                  EnemyMovementAI enemyMovementAI = child.gameObject.GetComponent<EnemyMovementAI>();
                  if(enemyMovementAI)
                  {
                     Debug.Log($"\tgolem");
                     enemyMovementAI.AwakeTheGolem();
                  }
               }
            }
         }
      }
      
   }
}
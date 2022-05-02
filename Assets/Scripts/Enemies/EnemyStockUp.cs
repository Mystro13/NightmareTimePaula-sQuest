using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStockUp : MonoBehaviour
{
   [SerializeField] private List<GameObject> stockItems;
   [SerializeField] private GameObject keyObject;
   [SerializeField] private GameObject healthPotionObject;
   public bool hasStockedUp = false;
   public void StockUp()
   {
      hasStockedUp = true;
      stockItems = new List<GameObject>();
      ExecuteStockUpOn(healthPotionObject);
   }
   public void StockUpKey()
   {
      if (keyObject)
      {
         ExecuteStockUpOn(keyObject);
      }
   }
   public void DropItems()
   {
      hasStockedUp = false;
      Debug.Log($"Drop items {stockItems.Count}");
      foreach (GameObject dropping in stockItems)
      {
         Debug.Log($"Drop- {dropping.tag}");
         dropping.transform.position = GeneratedPosition(gameObject.transform.position);
         dropping.transform.rotation = gameObject.transform.rotation;
         dropping.transform.parent = null;
         dropping.SetActive(true);

         var bCol = dropping.AddComponent<BoxCollider>();
         bCol.isTrigger = true;

         var rb = dropping.AddComponent<Rigidbody>();
         //rb.isKinematic = true;
         rb.useGravity = true;
         dropping.AddComponent<InteractionObject>();

         InteractionObject interactionObject = dropping.GetComponent<InteractionObject>();
         if (interactionObject != null)
         {
            Debug.Log("Drop interaction object script");
            if (dropping.name.ToUpper().Contains("KEY"))
            {
               interactionObject.interactionType = InteractionType.Picking;
            }
            else
            {
               interactionObject.interactionType = InteractionType.Dropping;
            }
         }
      }
      stockItems = new List<GameObject>();
   }
   void ExecuteStockUpOn(GameObject stockItemObject)
   {
      Debug.Log($"Stock {stockItems.Count}");
      GameObject clone = Instantiate(stockItemObject, transform.position, transform.rotation, gameObject.transform);
      StockItem stockItem = clone.GetComponent<StockItem>();
      if (stockItem)
      {
         clone.SetActive(false);
         stockItem.quantity = 1;
         stockItems.Add(clone);
      }
   }

   Vector3 GeneratedPosition(Vector3 centralPosition)
   {
      float x, y, z;
      x = UnityEngine.Random.Range(-3f, 3f);
      y = 0f;
      z = UnityEngine.Random.Range(-3f, 3f);
      return new Vector3(x, y, z) + centralPosition;
   }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemiesToPoints : MonoBehaviour
{
   public GameObject objectToSpawn;
   public GameObject objectParent;
   public Transform[] spawningPoints;
   bool enableSpawning;
   // Start is called before the first frame update
   void Awake()
   {
      List<GameObject> spawnees = new List<GameObject>();
      Vector3 enemyScale = GetEnemyScale();

      foreach (Transform spawnPoint in spawningPoints)
      {
         GameObject enemyClone = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation, objectParent.transform);
         enemyClone.transform.localScale = enemyScale;
         spawnees.Add(enemyClone);
      }


      if (SceneLoader.instance.currentSceneType == SceneLoader.SceneType.ThirdFloor)
      {
         Random.InitState((int)DateTime.Now.Ticks);
         int selection = Random.Range(0, spawnees.Count);
         EnemyStockUp enemyStockUp = spawnees[selection].GetComponent<EnemyStockUp>();
         if (enemyStockUp != null)
         {
            enemyStockUp.StockUpKey();
         }
      }
   }


   Vector3 GetEnemyScale()
   {
      switch (SceneLoader.instance.currentSceneType)
      {
         case SceneLoader.SceneType.FirstFloor: return new Vector3(.7f, .7f, .7f);
         case SceneLoader.SceneType.ThirdFloor: return new Vector3(2f, 2f, 2f);
         case SceneLoader.SceneType.Throne: return new Vector3(2f, 2f, 2f);
      }
      return new Vector3(.7f, .7f, .7f);
   }
}
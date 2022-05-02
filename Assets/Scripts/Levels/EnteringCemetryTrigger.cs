using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteringCemetryTrigger : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag(Tags.Player.ToString()))
      {
         //save prefs to be retrieve on the other side
         SceneLoader.instance.Load(SceneLoader.SceneType.Cemetery);
      }
   }
}

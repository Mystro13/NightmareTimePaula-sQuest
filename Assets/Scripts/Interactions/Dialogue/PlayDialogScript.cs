using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayDialogScript : MonoBehaviour
{
   public Text bannerUI;
   public string[] textsToShow;
   public float intervalTime;
   public void Execute()
   {
      if (bannerUI)
      {
         foreach (string text in textsToShow)
         {
            if (!string.IsNullOrEmpty(text))
            {
               executeWait(intervalTime);
               bannerUI.text = text;
            }
         }
      }
   }
   void executeWait(float aux)
   {
      StartCoroutine(Wait(aux));
   }

   IEnumerator Wait(float seconds)
   {
      yield return new WaitForSeconds(seconds);
   }
}
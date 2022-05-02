using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationScript : MonoBehaviour
{
   public Animator animator;
   public string[] animationScripts;
   public float intervalTime;
   public void Execute()
   {
      if(animator)
      {
         foreach (string script in animationScripts)
         {
            if (!string.IsNullOrEmpty(script))
            {
               executeWait(intervalTime);
               animator.Play(script);
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

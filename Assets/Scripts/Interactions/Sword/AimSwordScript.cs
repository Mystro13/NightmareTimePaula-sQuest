using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimSwordScript : MonoBehaviour
{
   public bool isAiming;
   public float aimSpeed=5f;
   private bool interactionEnabled;
    // Start is called before the first frame update
    void Start()
    {
      isAiming = false;
      interactionEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
      if (interactionEnabled)
      {
         if (transform.parent.name.Equals("SwordGrip"))
         {
            Keyboard keyboard = Keyboard.current;
            Mouse mouse = Mouse.current;

            if (mouse.leftButton.ReadValue() > 0f)
            {
               AimSword(true);
            }
            if (mouse.rightButton.ReadValue() > 0f)
            {
               AimSword(false);
            }
         }
      }
   }

   void AimSword(bool isAimingCommand)
   {
      isAiming = isAimingCommand;
      //we need to change the position of the sword grip and the direction where to point the sword
      if (isAiming)
      {
         //new.position = Vector3.Lerp(old.position, aiming.position, Time.deltaTime * aimSpeed);
      }
      else
      {
         //new.position = Vector3.Lerp(old.position, hip.position, Time.deltaTime * aimSpeed);
      }
   }

   public void EnableSword()
   {
      interactionEnabled = true;
   }
}

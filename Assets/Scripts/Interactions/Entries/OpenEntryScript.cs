using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEntryScript : MonoBehaviour
{
   public Vector3 openPosition;
   public Vector3 closedPosition;
   bool interactionEnabled = false;
   private bool open = false;
   public float actionSpeed = 5f;
   public bool Open { get => open; set => open = value; }
   // Start is called before the first frame update
   void Start()
   {
      //intereaction is disabled by default and should only be turned on via the interaction logic.
      interactionEnabled = false;
   }

   // Update is called once per frame
   void Update()
   {
      if (interactionEnabled && Vector3.Distance(openPosition, closedPosition) > 0.1f)
      {
         // the script can open or close the door when interaction is activated.
         // the door is setup on an initial position which could be open or closed.
         // both open and closed position are recorded via the inspector.
         // 
         Vector3 finalPosition = Open ? openPosition : closedPosition;
         transform.position = Vector3.Lerp(transform.position, finalPosition, actionSpeed * Time.deltaTime);
         interactionEnabled = Vector3.Distance(transform.position, finalPosition) < 0.1f;
      }
   }
   public void Execute()
   {
      GetComponent<Rigidbody>().useGravity = false;
      interactionEnabled = true;
   }
}

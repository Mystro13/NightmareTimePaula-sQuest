using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeScript : MonoBehaviour
{
   [Header("Assign which color to apply to material.")]
   public Color newColor;
   MeshRenderer rendererContext;

   // Start is called before the first frame update
   void Start()
   {
      rendererContext = GetComponent<MeshRenderer>();
   }

   public void ChangeColor()
   {
      if (rendererContext)
      {
         rendererContext.material.color = newColor;
      }
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate1 : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player.ToString()))
        {
            myAnimationController.SetBool("playTrigger 1", true);
        }
    }
}

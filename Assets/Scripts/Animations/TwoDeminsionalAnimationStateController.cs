using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TwoDeminsionalAnimationStateController : MonoBehaviour
{
   Animator animator;
   AudioSource audioSource;
   public AudioClip footstepClip;
   float velocityZ = 0.0f;
   float velocityX = 0.0f;
   public float acceleration = .5f;
   public float deceleration = .5f;
   public float maxJogVelocity = 2f;
   float tolerance = 0.001f;
   int VelocityZHash;
   int VelocityXHash;
   int isStandingJumpingHash;
   int isRunningJumpingHash;
   int isAttackingHash;
   int isGettingUpHash;
   int isFallingHash;
   int isCrashLandingHash;
   private void Awake()
   {
      animator = GetComponent<Animator>();
      audioSource = GetComponent<AudioSource>();
      VelocityZHash = Animator.StringToHash("ZVelocity");
      VelocityXHash = Animator.StringToHash("XVelocity");
      isStandingJumpingHash = Animator.StringToHash("IsStandingJumping");
      isRunningJumpingHash = Animator.StringToHash("IsRunningJumping");
      isAttackingHash = Animator.StringToHash("IsAttacking");
      isGettingUpHash = Animator.StringToHash("IsGettingUp");
      isFallingHash = Animator.StringToHash("IsFalling");
      isCrashLandingHash = Animator.StringToHash("IsCrashLanding");

      CouncilRoomEntry();
   }
   // Start is called before the first frame update
   void Start()
   {

   }
   void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool backPressed)
   {
      if (!leftPressed && !rightPressed && !ValueIsCloserTo(velocityX, 0f))
      {
         int oppositeSign = velocityX <= 0f ? 1 : -1;
         velocityX += Time.deltaTime * deceleration * oppositeSign;
         if (Math.Abs(velocityX) <= tolerance)
         {
            velocityX = 0f;
         }
         Debug.Log("reset");
      }
      if (!forwardPressed && !backPressed && !ValueIsCloserTo(velocityZ, 0f))
      {
         int oppositeSign = velocityZ <= 0f ? 1 : -1;
         velocityZ += Time.deltaTime * deceleration * oppositeSign;
         if (Math.Abs(velocityZ) <= tolerance)
         {
            velocityZ = 0f;
         }
         Debug.Log("reset");
      }

      if (velocityX >= maxJogVelocity)
      {
         velocityX = maxJogVelocity;
      }
      if (velocityZ >= maxJogVelocity)
      {
         velocityZ = maxJogVelocity;
      }
      //if (forwardPressed && velocityZ > maxJogVelocity)
      //{
      //   velocityZ = maxJogVelocity;
      //}
      //else if (forwardPressed && velocityZ < maxJogVelocity && velocityZ > (maxJogVelocity - 0.05f))
      //{
      //   velocityZ = maxJogVelocity;
      //}
   }

   bool ValueIsCloserTo(float value, float comparedTo)
   {
      return (value >= comparedTo - tolerance && value <= comparedTo + tolerance);
   }
   void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool backPressed)
   {
      if (forwardPressed)
      {
         if (velocityZ < maxJogVelocity)
         {
            velocityZ += Time.deltaTime * acceleration;
         }
      }

      if (leftPressed)
      {
         if (velocityX > -maxJogVelocity)
         {
            velocityX -= Time.deltaTime * acceleration;
         }
      }

      if (backPressed)
      {
         if (velocityZ > -maxJogVelocity)
         {
            velocityZ -= Time.deltaTime * acceleration;
         }
      }

      if (rightPressed)
      {
         if (velocityX < maxJogVelocity)
         {
            velocityX += Time.deltaTime * acceleration;
         }
      }
   }
   // Update is called once per frame
   void Update()
   {
      Keyboard keyboard = Keyboard.current;
      bool forwardPressed = keyboard.wKey.isPressed;
      bool backPressed = keyboard.sKey.isPressed;
      bool rightPressed = keyboard.dKey.isPressed;
      bool leftPressed = keyboard.aKey.isPressed;
      bool leftMouseClick = Mouse.current.leftButton.isPressed;
      bool spacePressed = keyboard.spaceKey.isPressed;

      Attacking(leftMouseClick);
      ChangeVelocity(forwardPressed, leftPressed, rightPressed, backPressed);

      animator.SetFloat(VelocityZHash, velocityZ);
      animator.SetFloat(VelocityXHash, velocityX);
      if (spacePressed)
      {
         AnimateJump(forwardPressed, leftPressed, rightPressed, backPressed);
      }
      LockOrResetVelocity(forwardPressed, leftPressed, rightPressed, backPressed);
      animator.SetFloat(VelocityZHash, velocityZ);
      animator.SetFloat(VelocityXHash, velocityX);
   }

   private void AnimateJump(bool forwardPressed, bool leftPressed, bool rightPressed, bool backPressed)
   {
      bool isIdle = !forwardPressed && !leftPressed && !rightPressed && !backPressed;
      if (isIdle)
      {
         animator.SetTrigger(isStandingJumpingHash);
      }

      else
      {
         animator.SetTrigger(isRunningJumpingHash);
      }
   }
   public void Attacking(bool leftMouseClick)
   {
      if (leftMouseClick)
      {
         animator.SetBool(isAttackingHash, true);
      }
      if (!leftMouseClick)
      {
         animator.SetBool(isAttackingHash, false);
      }

   }
   public void CouncilRoomEntry()
   {
      if (SceneLoader.instance.currentSceneType == SceneLoader.SceneType.CouncilRoom)
      {
         //animator.SetTrigger(isFallingHash);
         //animator.SetTrigger(isGettingUpHash);
         //animator.SetTrigger(isCrashLandingHash);

      }
   }
   //public void FootStep(int stepindex)
   //{
   //   audioSource.PlayOneShot(footstepClip);
   //   Debug.Log($"Step {stepindex}");
   //}
}

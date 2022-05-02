using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementAI : MonoBehaviour
{
   private MovementCategoryOptions movementCategory = MovementCategoryOptions.StayIdle;
   //wandering 
   public float wanderRadius;
   public float wanderTimer;

   private Transform target;
   private NavMeshAgent navigationAgent;
   private float movementTimer;
   bool isAwaken;
   //
   public Transform[] Patrolpoints;
   public int curPatrolpoint = 0;
   bool ReversePath = false;
   Vector3 AIDestination;
   float distanceToDestination;
   //staying idle
   public float stayingIdleTimer = 20;

   void Awake()
   {
      GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
      navigationAgent = GetComponent<NavMeshAgent>();
      movementCategory = MovementCategoryOptions.AttackingAPlayer;
      isAwaken = false;
   }
   void OnDestroy()
   {
      GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
   }
   private void OnGameStateChanged(GameState newGameState)
   {
      enabled = (newGameState == GameState.GamePlay);
   }
   void Update()
   {
      if (!isAwaken)
      {
         return;
      }
      switch (movementCategory)
      {
         case MovementCategoryOptions.StayIdle:
            {
               StayIdelAI();
               break;
            }
         case MovementCategoryOptions.FleeingFromAPlayer:
            {
               FleeingFromDangerAI();
               break;
            }
         case MovementCategoryOptions.AttackingAPlayer:
            {
               AttackingTargetAI();
               break;
            }
         case MovementCategoryOptions.PatrollingAnAreaPath:
            {
               PatrolAnAreaAI();
               break;
            }
         case MovementCategoryOptions.WanderRandomly:
            {
               WanderRandomlyAI();
               break;
            }
         case MovementCategoryOptions.SearchingForAPlayer:
            {
               SearchTargetAI();
               break;
            }
         case MovementCategoryOptions.WithinDistance:
            {
               WithinDistanceAI();
               break;
            }
         case MovementCategoryOptions.CanSeeObject:
            {
               CanSeeObjectAI();
               break;
            }
         case MovementCategoryOptions.CanHearTarget:
            {
               CanHearTargetAI();
               break;
            }
         case MovementCategoryOptions.FlockTogether:
            {
               FlockTogetherAI();
               break;
            }
         case MovementCategoryOptions.FollowALeader:
            {
               FollowLeaderAI();
               break;
            }
         case MovementCategoryOptions.TakePlaceInQueue:
            {
               TakePlaceInQueueAI();
               break;
            }
      }
   }

   void OnCollisionEnter(Collision collision)
   {
      if (isAwaken && collision.gameObject.CompareTag(Tags.Player.ToString()))
      {
         PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
         if (playerHealth)
         {
            playerHealth.DepleteHealth();
            AudioManager.instance.Play(GameSounds.PlayerIsHit);
         }

         EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();
         if (enemyHealth)
         {
            enemyHealth.GetKilled();
            AudioManager.instance.Play(GameSounds.EnemyIsHit);
         }
      }
   }

   void OnTriggerEnter(Collider other)
   {
      if (isAwaken && other.gameObject.CompareTag(Tags.Player.ToString()))
      {
         PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
         if (playerHealth)
         {
            playerHealth.GetKilled();
            AudioManager.instance.Play(GameSounds.PlayerIsHit);
         }

         EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();
         if (enemyHealth)
         {
            enemyHealth.GetKilled();
            AudioManager.instance.Play(GameSounds.EnemyIsHit);
         }
      }
   }

   private void FleeingFromDangerAI()
   {
      for (int fleePoint = 0; fleePoint < Patrolpoints.Length; fleePoint++)
      {
         distanceToDestination = Vector3.Distance(gameObject.transform.position, Patrolpoints[fleePoint].position);
         if (distanceToDestination > 10.00f)
         {
            AIDestination = Patrolpoints[curPatrolpoint].position;
            navigationAgent.SetDestination(AIDestination);
            break;
         }
         else if (distanceToDestination < 2.00f)
         {
            ChangeMovementCategoryOption(MovementCategoryOptions.StayIdle);
         }
      }
   }

   private void AttackingTargetAI()
   {
      GameObject player = GameObject.FindGameObjectWithTag(Tags.Player.ToString());
      if (player)
      {
         Debug.Log("attacking player");
         AIDestination = player.transform.position - new Vector3(0f, -20f, 0f); ;
         navigationAgent.SetDestination(AIDestination);
         distanceToDestination = Vector3.Distance(gameObject.transform.position, AIDestination);
      }
   }

   private void PatrolAnAreaAI()
   {
      distanceToDestination = Vector3.Distance(gameObject.transform.position, Patrolpoints[curPatrolpoint].position);
      if (distanceToDestination > 2.00f)
      {
         AIDestination = Patrolpoints[curPatrolpoint].position;
         navigationAgent.SetDestination(AIDestination);
      }
      else
      {
         if (ReversePath)
         {
            if (curPatrolpoint <= 0)
            {
               ReversePath = false;
            }
            else
            {
               curPatrolpoint--;
               AIDestination = Patrolpoints[curPatrolpoint].position;
            }
         }
         else
         {
            if (curPatrolpoint >= Patrolpoints.Length - 1)
            {
               ReversePath = true;
            }
            else
            {
               curPatrolpoint++;
               AIDestination = Patrolpoints[curPatrolpoint].position;
            }
         }
      }
   }

   private void SearchTargetAI()
   {
      AIDestination = GameObject.FindGameObjectWithTag("Player").transform.position;
      navigationAgent.SetDestination(AIDestination);
      distanceToDestination = Vector3.Distance(gameObject.transform.position, AIDestination);
      if (distanceToDestination < 10)
      {
         //change the mode into interaction or combat
         movementCategory = MovementCategoryOptions.FleeingFromAPlayer;
         movementTimer = 0;
      }
   }

   private void WanderRandomlyAI()
   {
      movementTimer += Time.deltaTime;

      if (movementTimer >= wanderTimer)
      {
         Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
         navigationAgent.SetDestination(newPos);
         movementTimer = 0;
      }
   }

   private void WithinDistanceAI()
   {
      throw new NotImplementedException();
   }

   private void CanSeeObjectAI()
   {
      throw new NotImplementedException();
   }

   private void CanHearTargetAI()
   {
      throw new NotImplementedException();
   }

   private void FlockTogetherAI()
   {
      throw new NotImplementedException();
   }

   private void FollowLeaderAI()
   {
      throw new NotImplementedException();
   }

   private void TakePlaceInQueueAI()
   {
      throw new NotImplementedException();
   }

   private void StayIdelAI()
   {
      movementTimer += Time.deltaTime;

      if (movementTimer >= stayingIdleTimer)
      {
         movementTimer = 0;
         movementCategory = MovementCategoryOptions.SearchingForAPlayer;
      }
   }

   private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
   {
      Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

      randDirection += origin;

      NavMeshHit navHit;

      NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

      return navHit.position;
   }
   public void ChangeMovementCategoryOption(MovementCategoryOptions changeToMovementCategory)
   {
      switch (changeToMovementCategory)
      {
         case MovementCategoryOptions.StayIdle:
            {
               movementCategory = changeToMovementCategory;
               movementTimer = stayingIdleTimer;
               break;
            }
         case MovementCategoryOptions.MoveTowardsTarget:
            {
               break;
            }
         case MovementCategoryOptions.RotateTowardsTarget:
            {
               break;
            }
         case MovementCategoryOptions.SeekTarget:
            {
               break;
            }
         case MovementCategoryOptions.FleeingFromAPlayer:
            {
               break;
            }
         case MovementCategoryOptions.AttackingAPlayer:
            {
               movementCategory = changeToMovementCategory;
               break;
            }
         case MovementCategoryOptions.PursueingAPlayer:
            {
               break;
            }
         case MovementCategoryOptions.FollowTarget:
            {
               break;
            }
         case MovementCategoryOptions.PatrollingAnAreaPath:
            {
               break;
            }
         case MovementCategoryOptions.FindingCover:
            {
               break;
            }
         case MovementCategoryOptions.WanderRandomly:
            {
               break;
            }
         case MovementCategoryOptions.SearchingForAPlayer:
            {
               break;
            }
         case MovementCategoryOptions.WithinDistance:
            {
               break;
            }
         case MovementCategoryOptions.CanSeeObject:
            {
               break;
            }
         case MovementCategoryOptions.CanHearTarget:
            {
               break;
            }
         case MovementCategoryOptions.FlockTogether:
            {
               break;
            }
         case MovementCategoryOptions.FollowALeader:
            {
               break;
            }
         case MovementCategoryOptions.TakePlaceInQueue:
            {
               break;
            }
      }
   }
   public void AwakeTheGolem()
   {
      isAwaken = true;
      EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();
      if (enemyHealth)
      {
         Debug.Log("Awaken!!");
         //enemyHealth.isAwaken = isAwaken;
      }
   }
}

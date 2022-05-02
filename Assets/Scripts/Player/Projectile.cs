using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   [SerializeField]
   float lifetime;
   [SerializeField]
   float force;
   public int healthDamage = 1;
   public Rigidbody rigidBody;
   public float projectileSpeed=0f;
   float time =0f; 
   //Cinemachine.CinemachineImpulseSource source;

   void Start()
   {
      rigidBody = GetComponent<Rigidbody>();
      rigidBody.centerOfMass = transform.position;
      rigidBody.isKinematic = true;
      time = 0f;
   }
   void Update()
   {
      transform.position += transform.forward * projectileSpeed * Time.deltaTime;
      time += Time.deltaTime;
      if(time>=lifetime)
      {
         Destroy(gameObject);
      }
   }
   void OnTriggerEnter(Collider other)
   {
      Debug.Log($"collision {other.gameObject.name}");
      if (other.gameObject.CompareTag(Tags.enemies.ToString()))
      {
         Debug.Log($"enemy");
         EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
		 
         enemyHealth.DepleteHealth(healthDamage);
         //enemyHealth.GetKilled();
         Destroy(gameObject);
         AudioManager.instance.Play(GameSounds.EnemyIsHit);
      }
   }

   void OnCollisionEnter(Collision collision)
   {
      Debug.Log($"collision {collision.gameObject.name}");
      if (collision.gameObject.CompareTag(Tags.enemies.ToString()))
      {
         Debug.Log($"enemy");
         EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
         enemyHealth.DepleteHealth(healthDamage);
         //enemyHealth.GetKilled();
         Destroy(gameObject);
         AudioManager.instance.Play(GameSounds.EnemyIsHit);
      }
   }
}


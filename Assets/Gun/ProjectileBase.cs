using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float speed = 50f;

   public float timeToDestroy = 3f;

   public int damageAmount = 5;

   public List<string> tagsToHit;

   private void Awake(){
    Destroy(gameObject, timeToDestroy);

   }

   private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision){
        foreach(var t in tagsToHit){

            if(collision.transform.tag == t){

                var damageable = collision.transform.GetComponent<IDamageable>();

                if(damageable != null){

                Vector3 dir = collision.transform.position - transform.position;
                dir = -dir.normalized;
                dir.y = 0;

                damageable.Damage(damageAmount, dir);
                }

                break; 
            }

        }
        
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float speed = 50f;

   public float timeToDestroy = 3f;

   public int damageAmount = 5;

   private void Awake(){
    Destroy(gameObject, timeToDestroy);

   }

   private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision){
        var damageable = collision.transform.GetComponent<IDamageable>();

        if(damageable != null) damageable.Damage(damageAmount);
        Destroy(gameObject);
    }
}

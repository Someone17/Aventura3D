using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase Projectile;

    public Transform positionToShoot;

    public float timeBetweenShoot = .3f;

    private Coroutine _currentCoroutine;

    public float speed = 50f;

    public AudioSource soundSource;

    /*private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
           _currentCoroutine = StartCoroutine(StartShoot());
           PlaySoundSource();
        }
        if(Input.GetKeyUp(KeyCode.M)){
            if(_currentCoroutine != null) 
                StopCoroutine(_currentCoroutine);
        }      
    }*/

    protected virtual IEnumerator ShootCoroutine(){
        while(true){
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }
    // Update is called once per frame
    public virtual void Shoot()
    {
        var projectile = Instantiate(Projectile);
        projectile.transform.position = positionToShoot.position; 
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;

        //Shaker.Instance.Shake();
    }

    private void PlaySoundSource(){
        if(soundSource != null) soundSource.Play();
    }

    public void StartShoot(){
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }
    
    public void StopShoot(){
        if(_currentCoroutine != null) 
            StopCoroutine(_currentCoroutine);
    }
}

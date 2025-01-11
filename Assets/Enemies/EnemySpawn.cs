using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemyPos;
    private float repeatRate = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        Invoke("EnemySpawner", 10f);
        Destroy(gameObject, 70);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void EnemySpawner(){
        Instantiate(enemy, enemyPos.position, enemyPos.rotation);
    }
}

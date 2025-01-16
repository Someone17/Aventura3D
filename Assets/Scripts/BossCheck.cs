using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheck : MonoBehaviour
{
    public string tagToCheck = "Player";

    public GameObject bossCamera;

    private void OnTriggerEnter(Collider other){
        if(other.transform.tag == tagToCheck){
            TurnCameraOn();
        }
    }

    private void TurnCameraOn(){
        bossCamera.SetActive(true);
    }
    
    private void Awake()
    {
        bossCamera.SetActive(false);
    }


    private void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
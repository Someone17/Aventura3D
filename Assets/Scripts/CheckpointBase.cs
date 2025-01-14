using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    
    public int key = 01; 

    private string CheckpointKey = "CheckpointKey";

    private bool checkpointActive = false;


    private void OnTriggerEnter(Collider other){
        if(!checkpointActive && other.transform.tag == "Player"){
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint(){
        TurnItOn();
        SaveCheckpoint();
    }

    [NaughtyAttributes.Button]
    private void TurnItOn(){
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
        SaveCheckpoint();
    }
    
    [NaughtyAttributes.Button]
    private void TurnItOff(){
        meshRenderer.material.SetColor("_EmissionColor", Color.black);
    }

    private void SaveCheckpoint(){
       /* if(PlayerPrefs.GetInt(CheckpointKey, 0) > key)
            PlayerPrefs.SetInt(CheckpointKey, key);*/

        CheckpointManager.Instance.SaveCheckpoint(key);

        checkpointActive = true;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

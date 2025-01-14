using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = 0;

    public List<CheckpointBase> checkpoints;

    public bool HasCheckpoint(){
        return lastCheckpointKey > 0;
    }

    public void SaveCheckpoint(int i){
        if(i > lastCheckpointKey){
            lastCheckpointKey = i;
        }
    }

    public Vector3 GetPositionFromLastCheckpoint(){
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

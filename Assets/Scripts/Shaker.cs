using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;
using Cinemachine;

public class Shaker : Singleton<Shaker>
{
    public CinemachineVirtualCamera virtualCamera;

    public float shakeTime;

    [Header("ShakeValues")]
    public float amplitude = 3f;
    public float frequency = 3f;
    public float time = 0.2f;

    [NaughtyAttributes.Button]
    public void Shake(){
        Shake(amplitude, frequency, time);
    }

    public void Shake(float amplitude, float frequency, float time){
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
        
        shakeTime = time;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(shakeTime > 0){
            shakeTime -= Time.deltaTime;
        }
        else
        {
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
        }
    }
}

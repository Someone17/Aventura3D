using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GunShootLimit : GunBase
{
    public List<UIGunUpdater> uiGunUpdaters;

    public float maxshoot = 5f;
    public float timeToRecharge = 1f;

    private bool _recharging = false;

    private float _currentShoots;

    protected override IEnumerator ShootCoroutine(){
        if(_recharging) yield break;

        while(true){
            if(_currentShoots < maxshoot){
            Shoot();
            _currentShoots++;
            CheckRecharge();
            UpdateUI();
            yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void Awake(){
        GetAllUIs();
    }

    private void CheckRecharge(){
        if(_currentShoots >= maxshoot) {
            StopShoot();
            StartRecharing();
        }
    }

    private void StartRecharing(){
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine(){
        float time = 0;
        while(time < timeToRecharge){
            time += Time.deltaTime;
            Debug.Log("Recharging: " + time);
            uiGunUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _recharging = false;
    }

    private void UpdateUI(){
        uiGunUpdaters.ForEach(i => i.UpdateValue(maxshoot, _currentShoots));
    }

    private void GetAllUIs(){
        uiGunUpdaters = GameObject.FindObjectsOfType<UIGunUpdater>().ToList();
    }
}
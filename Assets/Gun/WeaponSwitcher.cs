using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            switchWeapons();
        }

    }

    void switchWeapons() { 
        foreach(Transform weapon in transform)
        {
            weapon.gameObject.SetActive(!weapon.gameObject.activeSelf);
        }
    }
}
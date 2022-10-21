using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    WeaponBase[] availableWeapon;
    WeaponBase currentWeapon;
    Animator animator;
    [SerializeField]int currentWeaponIndex = 0;
    // Start is called before the first frame update

    private void Awake() {
        currentWeapon = GetComponentInChildren<WeaponBase>();
        animator = GetComponentInChildren<Animator>();
        availableWeapon = GetComponentsInChildren<WeaponBase>(true);//el true hace que coja los activos e inactivos

    }
    void Start()
    {
        SelectedCurrentWeapon(currentWeaponIndex);
    }

    private void SelectedCurrentWeapon(int index)
    {
        if(index < 0){
            currentWeaponIndex = availableWeapon.Length-1;
        }else if (index >= availableWeapon.Length){
            currentWeaponIndex = 0;
        }
        currentWeapon?.StopShooting();
        currentWeapon = availableWeapon[currentWeaponIndex];
        foreach(WeaponBase w in availableWeapon)
        {
            w.gameObject.SetActive(w == currentWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Mouse0)){
        
		animator.SetTrigger("Shoot");
		currentWeapon.Shoot();
        currentWeapon.StartShooting();
	    }

        if(Input.GetKeyUp(KeyCode.Mouse0)){
            currentWeapon.StopShooting();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            currentWeaponIndex --;
            SelectedCurrentWeapon(currentWeaponIndex);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            currentWeaponIndex ++;
            SelectedCurrentWeapon(currentWeaponIndex);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            currentWeapon.Swing();
        }

        
        

    }
}

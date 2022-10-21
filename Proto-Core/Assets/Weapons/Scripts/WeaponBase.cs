using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
	public enum WeaponUseType
	{
		Swing,
		Shoot,
		ContinousShoot,
		undefined
	}

	public virtual WeaponUseType GetUseType() {return WeaponUseType.undefined;}
	void Start()
	{
	}


	void Update()
	{
	}

	public virtual void Swing(){
		
	}

	public virtual void Shoot()
	{
		Debug.Log("Shoot");
	}

	public virtual void StartShooting()
	{
		Debug.Log("Start Shooting");
	}

	public virtual void StopShooting()
	{
		Debug.Log("Stop Shooting");
	}
}

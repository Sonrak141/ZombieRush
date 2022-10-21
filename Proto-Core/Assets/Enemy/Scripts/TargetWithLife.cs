using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CapsuleCollider))]
public class TargetWithLife : TargetBase
{
	[SerializeField] protected float life = 1f;
	[SerializeField] float ShootDamage = 0.2f;
	[SerializeField] float SwingDamage = 0.4f;
	[SerializeField] float ParasiteDamage = 0.2f;
	[SerializeField] float MutantDamage = 0.5f;
	[SerializeField] float WlakerDamage = 0.1f;

	public override void NotifyShot()
	{
		life -= ShootDamage;
		Debug.Log("Me quedan " + life + " de vida");
		CheckStillAlive();
	}

    

    public override void NotifySwing()
	{
		life -= SwingDamage;
		Debug.Log("Me quedan " + life + " de vida");
		CheckStillAlive();
	}

    public override void NotifyParasiteAttactk()
    {
		life -= ParasiteDamage;
		Debug.Log("Al player le queda " + life);
		CheckStillAlive();
    }

    public override void NotifyMutantAttack()
    {
		life -= MutantDamage;
		Debug.Log("Al player le queda " + life);
		CheckStillAlive();
    }

    public override void NotifyWalkerAttack()
    {
		life -= WlakerDamage;
		Debug.Log("Al player le queda " + life);
		CheckStillAlive();
    }

    protected virtual void CheckStillAlive()
    {
        if (life <= 0f)
		{
			Destroy(gameObject);
		}
    }
	
}

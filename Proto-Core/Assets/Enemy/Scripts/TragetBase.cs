using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBase : MonoBehaviour
{
	public virtual void NotifyShot() //el virtual me permite sobre escribir la funcion en otro script
	{
		Debug.Log("Me han dado");
	}

	public virtual void NotifySwing()
	{
		
	}

	public virtual void NotifyExplosion()
	{
		
	}
	public virtual void NotifyParasiteAttactk()
    {

    }
	public virtual void NotifyMutantAttack()
    {

    }
	public virtual void NotifyWalkerAttack()
    {

    }
}

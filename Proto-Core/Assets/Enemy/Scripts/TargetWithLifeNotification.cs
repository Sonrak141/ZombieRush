using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWithLifeNotification : TargetWithLife
{
    public interface IDeathNotifiable
    {
        public void NotifyDead();
    }
    // Start is called before the first frame update
    protected override void CheckStillAlive()
    {
        if (life <= 0f){
            GetComponent<IDeathNotifiable>()?.NotifyDead();
        }
    }
}

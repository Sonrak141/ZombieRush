using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerAttack : WeaponBase
{
    [SerializeField] float forwardRange;
    [SerializeField] float horizontalRange;
    [SerializeField] float vertcialRange;
    [SerializeField] Transform hitPoint;



    // Start is called before the first frame update
    public override void Swing()
    {

        Vector3 halfExtends = new Vector3(horizontalRange / 2f, vertcialRange / 2f, forwardRange / 2f);
        Collider[] colliders = Physics.OverlapBox(transform.position, halfExtends, transform.rotation);
        foreach (Collider c in colliders)
        {
            TargetBase target = c.GetComponent<TargetBase>();

            target?.NotifyWalkerAttack();
        }
    }
}

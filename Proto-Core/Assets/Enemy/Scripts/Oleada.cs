using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oleada : MonoBehaviour
{
    EnemySoldier[] enemigos;
    // Start is called before the first frame update
    void Awake()
    {
        enemigos = GetComponentsInChildren<EnemySoldier>();
    }

    public void DeactivateEnemys()
    {
        foreach (EnemySoldier e in enemigos){
            e.gameObject.SetActive(false);
        }
    }

    public void ActivateEnemys()
    {
        foreach (EnemySoldier e in enemigos){
            e.gameObject.SetActive(true);
        }
    } 

    public bool AllEnemysDead()
    {
        bool dead = true;
        for (int i = 0; dead && (i < enemigos.Length); i++){
            dead = enemigos[i] == null;
        }

        return dead;
    }
}

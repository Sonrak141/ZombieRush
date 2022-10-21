using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavigateToTransform : MonoBehaviour
{
    public Transform goTo;
    NavMeshAgent navMeshAgent;



    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (goTo)
        {

            navMeshAgent.SetDestination(goTo.position);
        }


    }
}

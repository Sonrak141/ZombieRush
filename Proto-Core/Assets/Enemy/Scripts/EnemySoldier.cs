using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySoldier : MonoBehaviour, TargetWithLifeNotification.IDeathNotifiable, NoiseMaker.INoiseListener
{
    WeaponBase currentWeapon;
    [SerializeField] float attackDistance = 0.5f;
    [SerializeField] float attackSpeed = 0.5f;

    [SerializeField] Animator animator;
    [SerializeField] NavigateToRoute navigateRoute;

    private NavigateToTransform navigate;
    NavigateToPosition navigateToPosition;

    Transform currentTarget = null;
    NoiseMaker lastHeard = null;
    [SerializeField] float timeToForgetNoise = 1f;

    Sight Sight;
    public enum Behaviour
    {
        Cautious,
        Valiant,
        Sneaky,
        Guardian
    };

    [SerializeField] Behaviour behaviourType = Behaviour.Valiant;
    enum State
    {
        Idle,
        Patrol,
        Seek,
        Attack,
        Die,
        Check
    }
    State state = State.Idle;

    void Awake()
    {
        currentWeapon = GetComponentInChildren<WeaponBase>();
        navigateRoute = GetComponent<NavigateToRoute>();
        navigate = GetComponent<NavigateToTransform>();
        navigateToPosition = GetComponent<NavigateToPosition>();
        animator = GetComponent<Animator>();
        Sight = GetComponent<Sight>();
    }

    void Start()
    {
        if (behaviourType == Behaviour.Guardian)
        {
            GetComponent<NavMeshAgent>().speed = 0f;
        }
    }

    float timeForNextAttack = 0f;
    float timeLeftToForgetNoise;
    private Vector3 lastNoticed;
    private float checkPositionTreshold;
    private bool locateFirstTarget = false;

    void Update()
    {
        updateNoiseMaker();
        updateCurrentTarget();
        

        Debug.Log(state);

        switch (state)
        {
            case State.Idle:
                
                if (currentTarget != null)
                {
                    state = State.Seek;
                }
                break;

            case State.Patrol:
                if (currentTarget != null)
                {
                    state = State.Seek;
                    navigateRoute.enabled = true;
                }
                break;
            case State.Seek:
               
                
                if (currentTarget == null)
                {
                    
                    state = State.Idle;
                    

                }
                else {
                    navigate.goTo = currentTarget;
                    if (Vector3.Distance(transform.position, currentTarget.position) < attackDistance)
                    {
                        navigate.goTo = transform;
                        state = State.Attack;
                        timeForNextAttack = 1f / attackSpeed;
                        


                    }
                }
                break;
            case State.Check:
                navigateRoute.route = null;
                navigateToPosition.position = lastNoticed;
                if(Vector3.Distance(navigateToPosition.position, transform.position) < checkPositionTreshold)
                {
                    if (navigateRoute.route != null)
                    {
                        navigate.goTo = null;
                        state = State.Patrol;
                    }
                    else
                    {
                        state = State.Idle;
                    }
                }
                break;

            case State.Attack:
                updateAttack();
                break;

            case State.Die:
                navigate.goTo = null;
                //Aqui va la annimacion de muerte
                animator.SetBool("Dead", true);
                Destroy(gameObject, 4f);
                break;

        }
    }

    private void updateCurrentTarget()
    {
        currentTarget = null;
        if (Sight.collidersInSight.Count > 0)
        {
            currentTarget = Sight.collidersInSight[0].transform;
        }
        else
        {
            if (behaviourType != Behaviour.Sneaky || locateFirstTarget)
            {
                if (lastHeard != null) { currentTarget = lastHeard.transform; }
            }
        }
        locateFirstTarget |= currentTarget != null;
        if (currentTarget != null)
        {
            lastNoticed = currentTarget.position;

        }
    }

    private void updateNoiseMaker()
    {
        timeLeftToForgetNoise -= Time.deltaTime;
        if (timeLeftToForgetNoise <= 0) { lastHeard = null; }
    }

    private void updateAttack()
    {
        bool advanceWhileAttacking = behaviourType == Behaviour.Valiant;
        if (advanceWhileAttacking)
        {
            navigate.goTo = currentTarget;
        }
        else
        {
            navigate.goTo = transform;
        }
        
        animator.SetBool("Shoot", true);
        timeForNextAttack -= Time.deltaTime;
        if (timeForNextAttack < 0f)
        {
            timeForNextAttack += 1f / attackSpeed;
            currentWeapon.Shoot();

        }
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > attackDistance)
        {
            state = State.Seek;
            navigate.goTo = currentTarget;
            animator.SetBool("Shoot", false);
        }
    }

    void TargetWithLifeNotification.IDeathNotifiable.NotifyDead()
    {
        if (state != State.Die)
        {
            state = State.Die;


        }
    }
    void NoiseMaker.INoiseListener.OnHeard(NoiseMaker noiseMaker)
    {
        Debug.Log(noiseMaker);
        lastHeard = noiseMaker;
        timeLeftToForgetNoise = timeToForgetNoise;

    }

    void GoTo(Vector3 position)
    {
        navigateRoute.enabled = false;
        navigate.enabled = false;
        navigateToPosition.enabled = true;
        navigateToPosition.position = position;
    }

    void Patrol()
    {
        navigateRoute.enabled = true;
        navigate.enabled = false;
        navigateToPosition.enabled = false;
    }

    void GoTo(Transform targetTransform)
    {
        navigateRoute.enabled = false;
        navigate.enabled = true;
        navigate.goTo = targetTransform;
        navigateToPosition.enabled = false;
    }


}

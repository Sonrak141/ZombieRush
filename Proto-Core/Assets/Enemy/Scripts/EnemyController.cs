using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, TargetWithLifeNotification.IDeathNotifiable
{

    WeaponBase meleeWeapon;
    [SerializeField] float attackDistance = 0.5f;
    [SerializeField] float attackSpeed = 0.5f;

    [SerializeField] Animator animator;

    private NavigateToTransform navigate;


    enum State
    {
        Seek,
        Attack,
        Die
    }
    State state = State.Seek;

    void Awake()
    {
        meleeWeapon = GetComponentInChildren<WeaponBase>();
        navigate = GetComponent<NavigateToTransform>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    float timeForNextAttack = 0f;
    void Update()
    {

        switch (state)
        {
            case State.Seek:
                navigate.goTo = PlayerController.instance.transform;
                if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < attackDistance)
                {
                    state = State.Attack;
                    timeForNextAttack = 1f / attackSpeed;
                    navigate.goTo = null;

                }
                break;
            case State.Attack:

                timeForNextAttack -= Time.deltaTime;
                if (timeForNextAttack < 0f)
                {
                    timeForNextAttack += 1f / attackSpeed;
                    meleeWeapon.Swing();
                    animator.SetTrigger("Attack");
                }
                if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > attackDistance)
                {
                    state = State.Seek;
                    navigate.goTo = PlayerController.instance.transform;
                }

                break;
            case State.Die:
                navigate.goTo = null;
                //Aqui va la annimacion de muerte
                animator.SetBool("Dead", true);
                Destroy(gameObject, 5f);
                break;

        }
    }

    void TargetWithLifeNotification.IDeathNotifiable.NotifyDead()
    {
        if (state != State.Die)
        {
            state = State.Die;


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EntityAnimation : MonoBehaviour
{
    Animator animator;
    Vector3 oldPosition = Vector3.zero;
    float smoothFactor = 20f;
    private void Awake()
    {
        oldPosition = transform.position;
        animator = GetComponentInChildren<Animator>();
    }
    Vector3 smoothedVelocity = Vector3.zero;
    void Update()
    {
        Vector3 currentWorldVelocity = (transform.position - oldPosition) / Time.deltaTime;
        Vector3 currentLocalVelocity = transform.InverseTransformDirection(currentWorldVelocity);
        smoothedVelocity += (currentLocalVelocity - smoothedVelocity).normalized * smoothFactor * Time.deltaTime;
         
            animator.SetFloat("ForwardMove", currentLocalVelocity.z);
            animator.SetFloat("HorizontalMove", currentLocalVelocity.x);
       
        oldPosition = transform.position;
    }
}
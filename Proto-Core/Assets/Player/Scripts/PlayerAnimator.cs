using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void PlayerAnimation(Animator anim, Vector3 Move)
    {
        Vector3 localMove = transform.InverseTransformDirection(Move);
        anim.SetFloat("HorizontalMove", localMove.x);
        anim.SetFloat("ForwardMove", localMove.z);
    }
}

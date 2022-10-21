using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCamenra : MonoBehaviour
{
    [SerializeField][Range(0f,180f)] float MaxLookAngle = 60f;
    [SerializeField] float verticalSensitivity = -0.0005f;
    float oldMousePositionY;
    // Start is called before the first frame update
    void Awake()
    {
        oldMousePositionY = Input.mousePosition.y;    
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseDelta = Input.GetAxis("Mouse Y");
        float mouseSpeed = mouseDelta / Time.deltaTime;

        Vector3 forwardPlane = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        float angle = Vector3.SignedAngle( transform.forward, forwardPlane, transform.right);
        float angleToApply = mouseSpeed * verticalSensitivity;
        if ((angle + angleToApply) > MaxLookAngle)
        {
            angleToApply -= MaxLookAngle - (angle + angleToApply);
        }else if ((angle + angleToApply) < -MaxLookAngle)
        {
            angleToApply -= (-MaxLookAngle - (angle + angleToApply));
        }


        Quaternion rotationToApply = Quaternion.AngleAxis(angleToApply, Vector3.right);


        transform.localRotation = rotationToApply * transform.localRotation;

        oldMousePositionY = Input.mousePosition.x;
    }
}

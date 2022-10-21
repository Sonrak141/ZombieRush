using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this add the character controller automatically
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    WeaponBase[] availableWeapons;
    CharacterController _characterController;
    [SerializeField] float speed = 2f;
    Animator _animator;
    float speedY = 0f;
    private float gravity = -9.8f;
    private PlayerAnimator anim;
    [SerializeField] LayerMask layerMaskAimingDetection;
    [SerializeField] bool FirstPersonMov = true;
    float oldMousePosition;
    [SerializeField] float mouseSensitivityX = 0.005f;

  


   void Awake()
   {
        instance = this;
        oldMousePosition = Input.mousePosition.x;
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
   }
    
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        anim = GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Key Inputs
        Vector3 movementFromInput = Vector3.zero;
        if(Input.GetKey(KeyCode.W)){
            movementFromInput += Vector3.forward;
        }
        if(Input.GetKey(KeyCode.S)){
            movementFromInput += Vector3.back;
        }
        if(Input.GetKey(KeyCode.A)){
            movementFromInput += Vector3.left;
        }
        if(Input.GetKey(KeyCode.D)){
            movementFromInput += Vector3.right;
        }
        //movement with camera forward
        Vector3 moveFromCamera =  Camera.main.transform.TransformDirection(movementFromInput);
        moveFromCamera = Vector3.ProjectOnPlane(moveFromCamera, Vector3.up);
        moveFromCamera.Normalize();
        //Gravity
        speedY += gravity*Time.deltaTime;
        moveFromCamera.y = speedY;


        _characterController.Move(moveFromCamera * speed * Time.deltaTime);

        if(_characterController.isGrounded){
            speedY = 0f;
        }

        //Animation
        // Vector3 localMove = transform.InverseTransformDirection(moveFromCamera);
        // _animator.SetFloat("HorizontalMove", localMove.x);
        // _animator.SetFloat("ForwardMove", localMove.z);

        // anim.PlayerAnimation(_animator, moveFromCamera);

        //Player move forward where the camera is looking
        // if(movementFromInput.sqrMagnitude > (0.01f * 0.01F)){
        //     Vector3 desiredFor = Camera.main.transform.forward;
        //     desiredFor = Vector3.ProjectOnPlane(desiredFor, Vector3.up);
        //     desiredFor.Normalize();

        //     Quaternion desiredRot = Quaternion.LookRotation(desiredFor, Vector3.up);
        //     Quaternion currentRot =  transform.rotation;
        //     transform.rotation = Quaternion.Lerp(currentRot,desiredRot, 0.3f);
        // }
        MouseAiming();
    }

    public void MouseAiming()
	{
        if (FirstPersonMov)
        {
            FirstPersonMouseMov();
        }
        else
        {
            //Saber donde esta el cursor
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMaskAimingDetection))

            {
                Vector3 desiredFor = hit.point - transform.position;
                desiredFor = Vector3.ProjectOnPlane(desiredFor, Vector3.up);
                desiredFor.Normalize();

                Quaternion desiredRotation = Quaternion.LookRotation(desiredFor, Vector3.up);
                Quaternion currentRotation = transform.rotation;
                transform.rotation = Quaternion.Lerp(currentRotation, desiredRotation, 0.4f);

            }
        }
		
	}

    public void FirstPersonMouseMov()
    {
        float mouseDelta = Input.mousePosition.x - oldMousePosition;
        float mouseSpeed = mouseDelta / Time.deltaTime;
        Quaternion rotationToApply = Quaternion.AngleAxis(mouseSpeed * mouseSensitivityX, Vector3.up);


        transform.localRotation = rotationToApply * transform.localRotation;

        oldMousePosition = Input.mousePosition.x;
        
    }
}


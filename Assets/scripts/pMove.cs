using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class pMove : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    public bool isRunning;

    // private float nextActionTime = 0.0f;
    // public float period = 0.1f;
    // public Slider stamina;
    // public float StCounter = 0;

    void Start()
    {
        
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);
        // Press Left Shift to run
        // if (Time.time > nextActionTime ){
        //     nextActionTime += period;
            
        //     if(stamina.value == 0){
        //         walkingSpeed = 3f;
        //         StCounter = 70;
        //     }
        //     if(StCounter != 0){
        //         isRunning = false;
        //         StCounter -=1;
        //         stamina.value += 1f;
        //     }
        //     else{
        //         walkingSpeed = 6f;
        //     }
        //     if((isRunning == true) && (StCounter == 0)){
        //         stamina.value -= 2;
        //     }
        //     if((isRunning == false) && (StCounter == 0)){
        //         stamina.value += 2;
        //     }
        // }

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        
        characterController.Move(moveDirection * Time.deltaTime);

        


        // if((characterController.isGrounded) && (characterController.velocity.magnitude > 2f) && (GetComponent<AudioSource>().isPlaying == false)){
        //      GetComponent<AudioSource>().pitch = Random.Range(0.9f,1f);
        //      GetComponent<AudioSource>().volume = Random.Range(0.4f,0.6f);
        //      GetComponent<AudioSource>().Play(0);
        // }





        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
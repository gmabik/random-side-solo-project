using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (CharacterController), typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        walking,
        running,
        standing,
        inAir
    }
    public PlayerState State;

    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    public float currentMoveSpeed;
    public float speedModifier;

    [SerializeField] private CharacterController controller;
    [Header("Jump")]
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [Space(10)]
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [Space(10)]
    private Vector3 velocity;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundCheck;

    [Header("Camera")]
    [SerializeField] private float sensitivity;
    [SerializeField] private Camera mainCam;
    private float camX = 0f;

    void Start()
    {
        speedModifier = 1f;
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private float horizontal;
    private float vertical;
    private bool isRunning;

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        isRunning = Input.GetButton("Run");
        RotateCam();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        StateManage();
        SpeedManage();
        MoveCharacter();
    }

    private void StateManage() //assigns player state
    {
        if(!isGrounded) State = PlayerState.inAir;

        else if(horizontal != 0 || vertical != 0) // if any movement button is pressed
        {
            State = isRunning ? PlayerState.running : PlayerState.walking;
        }

        else State = PlayerState.standing; //if no button is pressed
    }

    private void SpeedManage() // applies speed depending on state, stays the same if inAir / standing
    {
        if (State == PlayerState.running) currentMoveSpeed = CountSpeed(runSpeed);
        else if (State == PlayerState.walking) currentMoveSpeed = CountSpeed(walkSpeed);
    }

    private float CountSpeed(float mainSpeed) //counts speed 
        => mainSpeed * speedModifier;

    private void MoveCharacter()
    {
        Vector3 move = gameObject.transform.right * horizontal + gameObject.transform.up * vertical;
        controller.Move(currentMoveSpeed * Time.deltaTime * move);

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime * 3;

        controller.Move(velocity * Time.deltaTime);
    }

    private void RotateCam()
    {
        float turner = Input.GetAxis("Mouse X") * sensitivity;
        float looker = Input.GetAxis("Mouse Y") * sensitivity;

        if (turner != 0)
        {
            transform.eulerAngles += new Vector3(0, turner, 0);
        }
        if (looker != 0)
        {
            camX -= looker;
            camX = Mathf.Clamp(camX, -140f, -50f);

            mainCam.transform.localRotation = Quaternion.Euler(camX, 0f, 0f);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        walking,
        running,
        standing,
        inAir
    }
    public PlayerState State { get; private set; }

    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    public float currentMoveSpeed {  get; private set; }
    public float speedModifier;

    [Header("Jump")]
    [SerializeField] private float jumpPower;
    public bool isGrounded;

    void Start()
    {
        speedModifier = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        StateManage();
        SpeedManage();
    }

    private void StateManage() //assigns player state
    {
        if(!isGrounded) State = PlayerState.inAir;

        else if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Horizontal") != 0) // if any movement button is pressed
        {
            if(Input.GetButton("Run")) State = PlayerState.running; //if run button is pressed

            else State = PlayerState.walking; //if run button is not pressed
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

    }
}

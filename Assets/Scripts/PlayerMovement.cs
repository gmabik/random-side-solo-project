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
    public float currentMoveSpeed;

    [Header("Jump")]
    [SerializeField] private float jumpPower;
    public bool isGrounded;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StateManage(); 
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

    private void SpeedManage()
    {

    }
}


using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private bool _jumpPressed;   
    private CharacterController _controller;

    [Tooltip("Character speed")]
    [SerializeField]
    private float playerSpeed;

    [Tooltip("Character jump velocity")]
    [SerializeField]
    private Vector3 _velocity;

    [Tooltip("Gravity")]
    [SerializeField]
    private float GravityValue = -9.81f;

    [Tooltip("Jump force")]
    [SerializeField]
    private float JumpForce = 5f;

    private Camera Camera;
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jumpPressed = true;
        }   
    }

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Camera = Camera.main;
            Camera.GetComponent<FirstPersonCamera>().Target = transform;
        }
    }

    public override void FixedUpdateNetwork()
    {

        if (_controller.isGrounded)
        {
            _velocity = new Vector3(0, 0, 0);
        }

        Quaternion cameraRotationY = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
        Vector3 move = cameraRotationY * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Runner.DeltaTime * playerSpeed;
        _controller.Move(move);

        _velocity.y += GravityValue * Runner.DeltaTime;
        
        if (_jumpPressed && _controller.isGrounded)
        {
            _velocity.y += JumpForce;
        }
        _controller.Move(move + _velocity * Runner.DeltaTime);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }



        _jumpPressed = false;
    }
}

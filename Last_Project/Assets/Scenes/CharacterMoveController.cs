using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMoveController : MonoBehaviour, GameInputAction.IFpsActions
{
    private CharacterController _characterController;
    private GameInputAction _inputAction;
    private Vector2 _moveActionValue;
    private float characterRotate = 0f;
    private float _fallingSpeed = 0f;
    private bool _isGrounded;

    private const float gravity = 9.81f;

    [SerializeField] private float characterMoveSpeed = 10f;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float groundCheckOffset = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        if (_inputAction == null)
            _inputAction = new GameInputAction();

        _inputAction.Fps.SetCallbacks(instance: this);
        _inputAction.Fps.Enable();
    }

    void Update()
    {
        //바닥인지 체크
        _isGrounded = Physics.CheckSphere(groundCheckPosition.position, groundCheckOffset, groundLayer);

        if(_isGrounded)
        {
            _fallingSpeed = 0f;
        }
    
        var verticalVector = transform.forward * (_moveActionValue.y * Time.deltaTime * characterMoveSpeed);
        //var horiaontalVector = transform.right * (_moveActionValue.x * Time.deltaTime * characterMoveSpeed);
        //characterRotate = characterRotate + _moveActionValue.x;
        transform.Rotate(new Vector3(x: 0, y: _moveActionValue.x * 1.5f, z: 0));
        _characterController.Move(verticalVector * 0.5f);

        _fallingSpeed = _fallingSpeed - Time.deltaTime * gravity;
        _characterController.Move(motion: new Vector3(x: 0, y: _fallingSpeed, z: 0));
    }

    public void OnJump(InputAction.CallbackContext context)
    {
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveActionValue = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
    }
}

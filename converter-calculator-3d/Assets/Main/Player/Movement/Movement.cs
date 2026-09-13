using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class Movement : MonoBehaviour
{
    [SerializeField] float _speed;

    private Rigidbody _rigidbody;
    private InputAction _moveAction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _moveAction = InputSystem.actions.FindAction("Move");
    }

   private void OnEnable()
    {
        _moveAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = _moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = (transform.right * inputVector.x + transform.forward * inputVector.y).normalized;
        Vector3 resultDirection = moveDirection * _speed;
        _rigidbody.linearVelocity = new Vector3(resultDirection.x, _rigidbody.linearVelocity.y, resultDirection.z);
    }
}

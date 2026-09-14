using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class Movement : MonoBehaviour
{

    [Header("Neccessary components")]
    [SerializeField] private float _speed;
    [SerializeField] private ReleaseCursour _cursor;

    private Rigidbody _rigidbody;
    private InputAction _moveAction;
    private bool _isMoveEnabled;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _moveAction = InputSystem.actions.FindAction("Move");
    }

   private void OnEnable()
    {
        _moveAction.Enable();
        _cursor.CursorStateChanged += OnCursorChanged;
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _cursor.CursorStateChanged -= OnCursorChanged;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCursorChanged(bool isCursorReleased)
    {
        _isMoveEnabled = !isCursorReleased;

        if (_isMoveEnabled)
            _moveAction.Enable();
        else
            _moveAction.Disable();
    }

    private void Move()
    {
        Vector2 inputVector = _moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = (transform.right * inputVector.x + transform.forward * inputVector.y).normalized;
        Vector3 resultDirection = moveDirection * _speed;
        _rigidbody.linearVelocity = new Vector3(resultDirection.x, _rigidbody.linearVelocity.y, resultDirection.z);
    }
}

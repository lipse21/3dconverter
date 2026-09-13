using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [Header("Neccessary Components")]
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _camera;
    [SerializeField] private ReleaseCursour _cursor;

    [Header("Characteristics")]
    [SerializeField] private float _sensivity;
    [SerializeField] private float _minXAngle;
    [SerializeField] private float _maxXAngle;

    private InputAction _look;
    private float _currentXAngle;
    private bool _isLookEnabled = true;

    private void Awake()
    {
        _look = InputSystem.actions.FindAction("Look");
    }

    private void OnEnable()
    {
        _look.Enable();
        _cursor.CursorStateChanged += OnCursorChanged;
    }

    private void OnDisable()
    {
        _look.Disable();
        _cursor.CursorStateChanged -= OnCursorChanged;
    }

    private void Update()
    {
        if (_isLookEnabled)
        {
            CameraRotation();
        }
    }

    private void OnCursorChanged(bool isCursorReleased)
    {
        _isLookEnabled = !isCursorReleased;

        if (_isLookEnabled)
            _look.Enable();
        else
            _look.Disable();
    }

    private void CameraRotation()
    {
        Vector2 inputAction = _look.ReadValue<Vector2>();
        _body.Rotate(Vector3.up * _sensivity *  inputAction.x);

        _currentXAngle -= (_sensivity * inputAction.y);
        _currentXAngle = Mathf.Clamp(_currentXAngle,_minXAngle, _maxXAngle);
        _camera.localRotation = Quaternion.Euler(_currentXAngle, 0, 0);
    }
}

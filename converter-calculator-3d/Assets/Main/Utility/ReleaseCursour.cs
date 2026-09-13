using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReleaseCursour : MonoBehaviour
{
    public event Action<bool> CursorStateChanged;

    private bool _isCursorReleased;
    private InputAction _release;

    private void Awake()
    {
        _release = InputSystem.actions.FindAction("Release");
    }

    private void Start()
    {
        SetCursorState(false);
    }

    private void OnEnable()
    {
        _release.performed += OnReleasePerformed;
        _release.Enable();
    }

    private void OnDisable()
    {
        _release.performed -= OnReleasePerformed;
        _release.Disable();
    }

    private void OnReleasePerformed(InputAction.CallbackContext context)
    {
        _isCursorReleased = !_isCursorReleased;
        SetCursorState(_isCursorReleased);
    }

    private void SetCursorState(bool released)
    {
        Cursor.visible = released;
        Cursor.lockState = released ? CursorLockMode.None : CursorLockMode.Locked;
        CursorStateChanged?.Invoke(released);
    }
}
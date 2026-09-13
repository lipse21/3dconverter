using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ATM : MonoBehaviour
{
    [SerializeField] private ATMTrigger _trigger;

    private InputAction _interact; 

    private bool _isPlayerCanInteract = false;
    private bool _isAtmActivated = false;
    public event Action<bool> ATMActivated;

    private void Awake()
    {
        _interact = InputSystem.actions.FindAction("Interact");
    }

    private void OnEnable()
    {
        _interact.performed += OnInteractWithAtm;
        _interact.Enable();
        _trigger.TriggerStateChanged += ChangeInteractFlag;
    }

    private void OnDisable( ) 
    {
        _interact.performed -= OnInteractWithAtm;
        _interact.Disable();
        _trigger.TriggerStateChanged += ChangeInteractFlag;
    }

    private void ChangeInteractFlag(bool canInteract)
    {
        _isPlayerCanInteract = canInteract;
    }

    private void OnInteractWithAtm(InputAction.CallbackContext context)
    {
        if (_isPlayerCanInteract)
        {
            _isAtmActivated = !_isAtmActivated;
            ATMActivated?.Invoke(_isAtmActivated);
           
        }
        
    }


}

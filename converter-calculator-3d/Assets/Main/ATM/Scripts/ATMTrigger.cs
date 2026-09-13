using System;
using UnityEngine;

public class ATMTrigger : MonoBehaviour
{
    public event Action<bool> TriggerStateChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerStateChanged?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerStateChanged?.Invoke(false);
        }
    }
}

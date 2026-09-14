using UnityEngine;
using TMPro;
using System;

public class Calculator : MonoBehaviour
{
    [SerializeField] private TMP_Text _display;

    private string _currentInput = "";
    private float _accumulatedResult = 0f;  
    private string _pendingOperation = "";
    private bool _isNewInput = true;

    public void OnDigitClicked(string digit)
    {
        if (_isNewInput)
        {
            _currentInput = digit;
            _isNewInput = false;
        }
        else
        {
            _currentInput += digit;
        }
        UpdateDisplay();
    }

    public void OnOperationClicked(string op)
    {
        
        if (float.TryParse(_currentInput, out float currentNumber))
        {
            
            if (!string.IsNullOrEmpty(_pendingOperation))
            {
                _accumulatedResult = Calculate(_accumulatedResult, currentNumber, _pendingOperation);
            }
            else
            {
                
                _accumulatedResult = currentNumber;
            }

           
            _display.text = _accumulatedResult.ToString("F2");
        }

        
        _pendingOperation = op;

        
        _isNewInput = true;
        _currentInput = "";
    }

    public void OnEqualsClicked()
    {
        
        if (!string.IsNullOrEmpty(_pendingOperation) &&
            float.TryParse(_currentInput, out float currentNumber))
        {
            _accumulatedResult = Calculate(_accumulatedResult, currentNumber, _pendingOperation);
            _display.text = _accumulatedResult.ToString("F2");
        }
        else
        {
            
            if (float.TryParse(_currentInput, out float number))
            {
                _accumulatedResult = number;
                _display.text = _accumulatedResult.ToString("F2");
            }
        }

        
        _pendingOperation = "";
        _currentInput = "";
        _isNewInput = true;
    }

    public void OnClearClicked()
    {
        _currentInput = "";
        _accumulatedResult = 0f;
        _pendingOperation = "";
        _isNewInput = true;
        UpdateDisplay();
    }

    private float Calculate(float first, float second, string op)
    {
        return op switch
        {
            "+" => first + second,
            "-" => first - second,
            "*" => first * second,
            "/" => second != 0 ? first / second : 0,
            _ => second
        };
    }

    private void UpdateDisplay()
    {
        if (!string.IsNullOrEmpty(_currentInput))
        {
            _display.text = _currentInput;
        }
        else
        {
            _display.text = _accumulatedResult.ToString("F2");
        }
    }
}
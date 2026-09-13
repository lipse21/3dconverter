using TMPro;
using UnityEngine;

public class Converter : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_InputField _amountInput;
    [SerializeField] private TMP_Dropdown _fromDropdown;
    [SerializeField] private TMP_Dropdown _toDropdown;
    [SerializeField] private TMP_Text _resultText;

    private readonly float[] _rates = { 1f, 0.92f, 90f, 7.2f }; // USD, EUR, RUB, CNY

    public void OnConvertButtonClicked()
    {
        if (!float.TryParse(_amountInput.text, out float amount))
        {
            _resultText.text = "Ошибка ввода";
            return;
        }

        int fromIndex = _fromDropdown.value;
        int toIndex = _toDropdown.value;

        if (fromIndex < 0 || fromIndex >= _rates.Length || toIndex < 0 || toIndex >= _rates.Length)
        {
            _resultText.text = "Ошибка валюты";
            return;
        }

        float inUsd = amount / _rates[fromIndex];
        float result = inUsd * _rates[toIndex];

        _resultText.text = $"{result:F2}";
    }
}

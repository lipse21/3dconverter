using UnityEngine;
using DG.Tweening;


public class ATMEnabler : MonoBehaviour
{

    [Header("Neccessary Components")]
    [SerializeField] private ATM _atm;
    [SerializeField] private CanvasGroup _canvasGroupConverter;
    [SerializeField] private CanvasGroup _canvasGroupCalculator;
    [SerializeField] private Converter _converter;
    [SerializeField] private float _duration;

    private bool _isCalculatorActivated = false;

    private void OnEnable()
    {
        _atm.ATMActivated += ProcessActivate;
        _converter.OnCalculatorButtonActivated += ShowCalculator;
    }

    private void OnDisable()
    {
        _atm.ATMActivated -= ProcessActivate;
        _converter.OnCalculatorButtonActivated -= ShowCalculator;
    }

    private void Start()
    {
        _canvasGroupConverter.alpha = 0f;
        _canvasGroupConverter.gameObject.SetActive(false);

        _canvasGroupCalculator.alpha = 0f;
        _canvasGroupCalculator.gameObject.SetActive(false);

    }

    private void ProcessActivate(bool typeOfActivate)
    {
        if (typeOfActivate)
        {
            ShowConverter();
        }
        else
        {
            HideConverter();
        }
    }

    private void ShowConverter()
    {
        if (_isCalculatorActivated) { return; }
        _canvasGroupConverter.gameObject.SetActive(true);
        _canvasGroupConverter.DOKill();
        _canvasGroupConverter.DOFade(1f, _duration).SetEase(Ease.Linear); 
    }

    private void HideConverter()
    {
        if (!_isCalculatorActivated)
        {
            _canvasGroupConverter.DOKill();
            _canvasGroupConverter.DOFade(0f, _duration)
                .SetEase(Ease.Linear)
                .OnComplete(() => _canvasGroupConverter.gameObject.SetActive(false));
        } else
        {
            HideCalculator();
        }

    }

    private void ShowCalculator()
    {
        HideConverter();
        _isCalculatorActivated = true;
        _canvasGroupCalculator.gameObject.SetActive(true);
        _canvasGroupCalculator.DOKill();
        _canvasGroupCalculator.DOFade(1f, _duration).SetEase(Ease.Linear);
    }

    private void HideCalculator()
    {
        _isCalculatorActivated = false;
        _canvasGroupCalculator.DOKill();
        _canvasGroupCalculator.DOFade(0f, _duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => _canvasGroupCalculator.gameObject.SetActive(false));
    }
}

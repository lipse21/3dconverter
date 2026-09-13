using UnityEngine;
using DG.Tweening;

public class ATMEnabler : MonoBehaviour
{
    [SerializeField] private ATM _atm;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _duration;

    private void OnEnable()
    {
        _atm.ATMActivated += ProcessActivate;      
    }

    private void OnDisable()
    {
        _atm.ATMActivated += ProcessActivate;
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
        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1f, _duration).SetEase(Ease.OutQuad); //поменять анимацию
    }

    private void HideConverter()
    {
        _canvasGroup.gameObject.SetActive(false);
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0f, _duration).SetEase(Ease.OutQuad); //поменять анимацию
    }
}

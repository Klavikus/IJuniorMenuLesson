using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _transitionMaxDelta;
    [SerializeField] private Image _fillImage;

    private const float MaxError = 0.01f;

    private float _targetHealth;
    private float _viewHealth;
    private bool _inTransition;

    private void OnEnable() => _health.HealthChanged += OnHealthChanged;

    private void OnDisable() => _health.HealthChanged += OnHealthChanged;

    private void Start()
    {
        _targetHealth = _health.HealthPercentage;
        _viewHealth = _targetHealth;
        _fillImage.fillAmount = _viewHealth;
    }

    private void OnHealthChanged(float healthPercentage)
    {
        _targetHealth = healthPercentage;

        if (_inTransition)
            return;

        StartCoroutine(UpdateView());
    }

    private IEnumerator UpdateView()
    {
        _inTransition = true;

        while (Mathf.Abs(_viewHealth - _targetHealth) > MaxError)
        {
            _viewHealth = Mathf.MoveTowards(_viewHealth, _targetHealth, _transitionMaxDelta * Time.deltaTime);
            _fillImage.fillAmount = _viewHealth;
            yield return null;
        }

        _viewHealth = _targetHealth;

        _inTransition = false;
    }
}
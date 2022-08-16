using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _transitionMaxDelta;
    [SerializeField] private Image _fillImage;

    private float _viewHealth;
    private Coroutine _updateViewCoroutine;
    
    private void OnEnable() => _health.HealthChanged += OnHealthChanged;

    private void OnDisable() => _health.HealthChanged += OnHealthChanged;

    private void Start()
    {
        _viewHealth = _health.HealthPercentage;
        _fillImage.fillAmount = _viewHealth;
    }

    private void OnHealthChanged(float healthPercentage)
    {
        if (_updateViewCoroutine != null)
            StopCoroutine(_updateViewCoroutine);

        _updateViewCoroutine = StartCoroutine(UpdateView(healthPercentage));
    }

    private IEnumerator UpdateView(float healthPercentage)
    {
        while (_viewHealth != healthPercentage)
        {
            _viewHealth = Mathf.MoveTowards(_viewHealth, healthPercentage, _transitionMaxDelta * Time.deltaTime);
            _fillImage.fillAmount = _viewHealth;
            yield return null;
        }
    }
}
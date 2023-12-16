using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadingAnimation : MonoBehaviour
{
    [SerializeField, Range(1f, 15f)] private float _durationInSeconds = 6f;
    [SerializeField] private Image _panelImage;

    public float DurationInSeconds => _durationInSeconds;

    private void OnEnable()
    {
        //FadeIn();
    }

    private void OnDisable()
    {
        FadeOut();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        while (_panelImage.color.a != 1)
        {
            float a = Mathf.MoveTowards(_panelImage.color.a, 1, Time.deltaTime / _durationInSeconds);

            Debug.Log($"{a} {DurationInSeconds}");

            _panelImage.color = new Color(_panelImage.color.r, _panelImage.color.g, _panelImage.color.b, a);

            yield return null;
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        while (_panelImage.color.a != 0)
        {
            float a = Mathf.MoveTowards(_panelImage.color.a, 0, Time.deltaTime / _durationInSeconds);

            _panelImage.color = new Color(_panelImage.color.r, _panelImage.color.g, _panelImage.color.b, a);

            yield return null;
        }
    }
}

using TMPro;
using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private BallsObserver observer;
    [SerializeField]
    private TextMeshPro scoreTMP;
    [SerializeField]
    private TextMeshPro showNameTMP;

    private Coroutine fadeCoroutine;

    private void Start()
    {
        observer.onScoreChanged += onScoreChanged;
    }

    private void onScoreChanged(string ballShowText, Color showTextColor, int newScore)
    {
        scoreTMP.text = $"Score: {newScore}";

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(ShowNameTextCoroutine(ballShowText, showTextColor));
    }

    private IEnumerator ShowNameTextCoroutine(string text, Color color)
    {
        showNameTMP.text = text;
        showNameTMP.color = color;

        Color visibleColor = color;
        visibleColor.a = 1f;
        showNameTMP.color = visibleColor;

        yield return new WaitForSeconds(3f);

        float fadeDuration = 1.5f;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);

            Color c = showNameTMP.color;
            c.a = alpha;
            showNameTMP.color = c;

            yield return null;
        }

        showNameTMP.text = "";
        fadeCoroutine = null;
    }
}

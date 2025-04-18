using System;
using TMPro;
using UnityEngine;

public class BallsObserver : MonoBehaviour
{
    public Action<string,Color, int> onScoreChanged;
    private int _currentScore;
    private void OnTriggerEnter(Collider other)
    {
        _currentScore++;
        var ball = other.GetComponent<Ball>();
        onScoreChanged.Invoke(ball.showText,ball.showColor, _currentScore);
    }
    private void OnTriggerExit(Collider other)
    {
        _currentScore--;

        var ball = other.GetComponent<Ball>();
        onScoreChanged.Invoke(ball.showText, ball.showColor, _currentScore);
    }

}

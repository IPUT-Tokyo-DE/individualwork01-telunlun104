using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float timeRemaining = GameManager.Instance.GetTime();

        int seconds = Mathf.FloorToInt(timeRemaining);

        timerText.text = seconds.ToString();
    }
}

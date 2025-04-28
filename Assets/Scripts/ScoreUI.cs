using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (GameManager.Instance != null && scoreText != null)
        {
            scoreText.text = GameManager.Instance.GetScore().ToString();
        }
        
    }
}

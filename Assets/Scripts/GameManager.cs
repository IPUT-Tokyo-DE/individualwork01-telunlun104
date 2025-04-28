using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int score = 0;
    public float timeRemaining = 30f;
    private bool gameEnded = false;

    public int requiredScore = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if(gameEnded) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0)
        {
            timeRemaining = 0;
            EndGame();
        }
           
    }

    public int GetScore() => score;
    public float GetTime() => timeRemaining;

    public void AddScore(int points)
    {
        score += points;

        if (score < 0)
        {
            score = 0;
        }
    }

    private void EndGame()
    {
        gameEnded = true;

        PlayerPrefs.SetInt("FinalScore", score);

        if (score >= requiredScore)
        {
            SceneManager.LoadScene("GameClear");
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}

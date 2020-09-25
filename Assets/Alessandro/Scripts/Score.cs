using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{


    GameManager gameManager;
    public Text scoreText;

    int score = 0;
    public int victoryThreshold = 100;
    public int lossThreshold = 0;
    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void AdjustScore(int _score)
    {
        score += _score;
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score < lossThreshold)
        {
            gameManager.EndGame();
        }
        else if (score >= victoryThreshold)
        {
            gameManager.Victory();
        }
    }
}

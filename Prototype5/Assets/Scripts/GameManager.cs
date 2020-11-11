using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    private int score;
    public bool isGameActive;
    public GameObject titleScreen;

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            //wait 1 second
            yield return new WaitForSeconds(spawnRate);

            //generate random index and spawn target based on index
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(score);
    }
}

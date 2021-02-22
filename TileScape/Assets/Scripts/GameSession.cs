using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 5;
    [SerializeField] int score = 0;
    [SerializeField] float timeToWait = 2.5f;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    GameObject[] numberOfCoins;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        numberOfCoins = GameObject.FindGameObjectsWithTag("Coin");
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString() + "/" + numberOfCoins.Length;
    }

    IEnumerator WaitForTime()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString() + "/" + numberOfCoins.Length;
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        LoadSceneAgain();
    }

    private void LoadSceneAgain()
    {
        StartCoroutine(WaitForTime());
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("Game Values")]
    public bool isRoundOver;
    public bool isGameOver;
    private bool isMakingGameOver;
    public int round;

    [Header("UI Values")]
    [Header("Main HUD")]
    public GameObject mainHudPanel;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI fightText;
    [Header("Game over HUD")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public GameObject retryButton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        isRoundOver = false;
        isGameOver = false;
        isMakingGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver && !isMakingGameOver)
        {
            StartCoroutine(MakeGameOver());
        }
    }

    public void RoundOver()
    {
        round++;
        StartCoroutine(ShowNextRoundText());
    }

    IEnumerator ShowNextRoundText()
    {
        roundText.text = "Round " + round;
        roundText.gameObject.SetActive(true);
        isRoundOver = true;
        yield return new WaitForSeconds(2);
        fightText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        roundText.gameObject.SetActive(false);
        fightText.gameObject.SetActive(false);
        isRoundOver = false;
    }

    IEnumerator MakeGameOver()
    {
        isMakingGameOver = true;
        mainHudPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOverText.text = "You lasted for\n";
        yield return new WaitForSeconds(1);
        gameOverText.text += round;
        yield return new WaitForSeconds(1);
        if (round == 1)
        {
            gameOverText.text += "\nround";
        } else
        {
            gameOverText.text += "\nrounds";
        }
        yield return new WaitForSeconds(2);
        retryButton.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}

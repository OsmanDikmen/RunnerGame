using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startPanel;
    public GameObject endPanel;
    public GameObject gamePanel;

    #region StartPanel
    public GameObject startTapButton;
    #endregion

    #region EndRegion
    public TMP_Text gameOverText;
    public GameObject finalScoreBoard;
    public GameObject retryButton;
    public TMP_Text finalScoreText;
    public TMP_Text bestScoreText;
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    public void ArriveIntoGame()
    {
        startTapButton.GetComponent<Animator>().SetTrigger("TapButtonArrive");
    }

    public void TapButtonClick()
    {
        if (GameManager.instance.gameOver && GameManager.instance.freezeTiles)
        {
            startTapButton.GetComponent<Animator>().SetTrigger("TapButtonFade");
            StartCoroutine(FadeOutToGame());
        }
    }

    

    private IEnumerator FadeOutToGame()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.gameOver = false;
        GameManager.instance.freezeTiles = false;
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void SetFinalScoreBoard(int score, int bestScore)
    {
        finalScoreText.text = score.ToString();
        bestScoreText.text = bestScore.ToString();
        ShowEndPanel();
    }

    private void ShowEndPanel()
    {
        endPanel.SetActive(true);
    }

    private void RetryButton()
    {
        StartCoroutine(RetryAnimations());
    }

    private IEnumerator RetryAnimations()
    {
        gameOverText.GetComponent<Animator>().SetTrigger("GameOverFadeOut");
        finalScoreBoard.GetComponent<Animator>().SetTrigger("ScoreBoardFadeOut");
        retryButton.GetComponent<Animator>().SetTrigger("RetryButtonFadeOut");

        yield return new WaitForSeconds(0.5f);
        GameManager.instance.ReloadLevel();
    }
}
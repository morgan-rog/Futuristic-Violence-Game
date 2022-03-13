using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI playerWinText;
    public Button restartButton;
    public GameObject titleScreen;
    private PlayerController player;
    public AudioClip playerWinsSound;
    public AudioClip enemyWinsSound;
    private AudioSource gameManagerAudio;
    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerAudio = GetComponent<AudioSource>();
        isGameActive = false;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int difficulty)
    {
        titleScreen.gameObject.SetActive(false);
        player.damage *= difficulty;
        isGameActive = true;
    }

    public void RestartGame()
    {
        Physics.gravity = new Vector3(0, -9.8f, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameManagerAudio.PlayOneShot(enemyWinsSound, 1.0f);
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void PlayerWins()
    {
        gameManagerAudio.PlayOneShot(playerWinsSound, 1.0f);
        playerWinText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }
}

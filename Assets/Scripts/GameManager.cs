using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TMP_Text gameOverScoreText;
    public TMP_Text gameOverHighScoreText;
    public Image[] lifeImages;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text livesText;
    public AudioClip powerPelletEatenSound;
    public AudioClip ghostEatenSound;
    public AudioClip pacmanEatenSound;
    public Ghost[] ghost;
    public AudioClip pelletEatenSound;
    private AudioSource audioSource;
    public Pacman pacman;
    public Transform pellets;
    public int score;
    public int live;
    public int ghostMultiplier = 1;
    public GameObject fruitPrefab;
    public static GameManager Instance;
    public TMP_Text fruitCountText; 
    private int fruitCount = 0;
    public void FruitEaten(Fruit fruit)
    {
        SetScore(score + fruit.fruitPoints);
        fruitCount++;
        fruitCountText.text = "Fruit                    x " + fruitCount.ToString();
       
    }
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        NewGame();

        
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void NewGame()
    {
        SetScore(0);
        SetLives(3);
        UpdateHighScore();
        NewRound();
        gameOverPanel.SetActive(false);
    }

    private void UpdateHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }
    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghost.Length; i++)
        {
            this.ghost[i].ResetState();
        }
        this.pacman.ResetState();
    }
    private void GameOver()
    {
        for (int i = 0; i < ghost.Length; i++)
        {
            ghost[i].gameObject.SetActive(false);
        }
        pacman.gameObject.SetActive(false);

       
        gameOverPanel.SetActive(true);

        
        gameOverScoreText.text = "Score: " + score.ToString();
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        gameOverHighScoreText.text = "High Score: " + highScore.ToString();
    }
    public void SetScore(int score)
    {
        this.score = score;
        scoreText.text = "Score: " + score.ToString();
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save(); 
            highScoreText.text = "High Score: " + score.ToString();
        }
    }
    private void DropFruit()
    {
        Vector3 randomPosition = GetRandomPosition();
        bool isColliding = CheckCollision(randomPosition);

       
        while (isColliding)
        {
            randomPosition = GetRandomPosition();
            isColliding = CheckCollision(randomPosition);
        }

        GameObject fruit = Instantiate(fruitPrefab, randomPosition, Quaternion.identity);
        fruit.GetComponent<Fruit>();
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-14f, 14f);
        float y = Random.Range(-11f, 11f);
        return new Vector3(x, y, 0f);
    }

    private bool CheckCollision(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.4f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                return true;
            }
        }

        return false;
    }
    private void SetLives(int lives)
    {
        this.live = lives;
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (i < lives)
                lifeImages[i].gameObject.SetActive(true);
            else
                lifeImages[i].gameObject.SetActive(false);
        }
    }
    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplier;
        SetScore(score + points);
        ghostMultiplier++;

        audioSource.PlayOneShot(ghostEatenSound);

        
        DropFruit();

        
    }
    public void PacmanEaten()
    {
        {
    this.pacman.gameObject.SetActive(false);
    SetLives(this.live - 1);
    audioSource.PlayOneShot(pacmanEatenSound);  
    
    if(this.live > 0)
    {
        Invoke(nameof(ResetState), 3f);
        
    }
    else
    {
        GameOver();
    }
}

    }
    public void PelletEaten(Pellet pellet)
{
    pellet.gameObject.SetActive(false);
    SetScore(this.score + pellet.points);
    audioSource.PlayOneShot(pelletEatenSound); 
    if (!HasRemainingPellets())
    {
        this.pacman.gameObject.SetActive(false);
        Invoke(nameof(NewRound), 3f);
    }
}
    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < this.ghost.Length; i++)
        {
            this.ghost[i].vulnerable.Enable(pellet.duration);
        }

        audioSource.PlayOneShot(powerPelletEatenSound); 

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }
    // Update is called once per frame
    void Update()
    {
        /*if (live <= 0 && Input.anyKeyDown)
        {
           
            NewGame();
            
        }*/
    }
    private bool HasRemainingPellets()
    {
        foreach(Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
}

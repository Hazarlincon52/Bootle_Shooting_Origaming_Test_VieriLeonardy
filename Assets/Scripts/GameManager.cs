using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Texture2D cursorDef;
    [SerializeField] private Texture2D cursorShoot;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameOverCollider;

    private Camera mainCamera;

    private int score;

    [SerializeField] private float gameTime;

    private bool gameOver = false;
    private bool scorePowerUp = false;

    private enum State
    {
        Menu,
        PlayState,
        GameOver
    }

    private State state;

    private void Awake() 
    {
        Instance = this;

        //Set Cursor
        Cursor.SetCursor(cursorDef, Vector2.zero, CursorMode.Auto);

        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Menu;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Menu:
                // do Nothing
                break;
            case State.PlayState:
                gameTime -= Time.deltaTime;
                TimeSpan second = TimeSpan.FromSeconds(gameTime);

                timerText.text = second.Seconds.ToString() + "'s";
            
                if(gameTime <= 0)
                {
                    state = State.GameOver;
                }
                
                if (Input.GetMouseButtonDown(0))
                {
                    DetectObject();
                }

                break;
            case State.GameOver:
                if(gameTime <= 0)
                {
                    //Debug.Log("gamefinish");
                    GameFinish();
                }
                break;
        }
        
    }
    private void DetectObject()// detect if mouse hit bottle and activate method Hit()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
        if(hit2D && hit2D.collider.gameObject.GetComponent<Bottles>() != null)
        {
            hit2D.collider.GetComponent<Bottles>().Hit();
        }
        
    }
 
    public void CalculateScore(int score)
    {
        if(scorePowerUp)
        {
            this.score += score * 2;
            scoreText.text = "Score: " + this.score;
        }
        else
        {
            this.score += score;
            scoreText.text = "Score: " + this.score;
        }
        
    }

    public void StartGame()// start game button
    {
        animator.SetTrigger("Start");
        Cursor.SetCursor(cursorShoot, Vector2.zero, CursorMode.Auto);

        //start game
        SpawnBottles.Instance.GetStartGame(true);

        state = State.PlayState;

        AudioManager.instance.ButtonSound();//SFX button
    }

    public void ExitGame()// exit game button
    {
        AudioManager.instance.ButtonSound();//SFX button
        Application.Quit();
    }

    private void GameFinish()
    {
        state = State.GameOver;
        gameOver = true; // stop shooting
        gameOverPanel.SetActive(true);
        gameOverScreen.SetActive(true);
        gameOverCollider.SetActive(true);
        finalScoreText.text = "Final Score: " + score.ToString();

        //stop spawn bottles
        SpawnBottles.Instance.GetStartGame(false);

        //Stop all PowerUps UI
        PowerUps.Instance.StopAllPowerUps();

        //Set Cursor
        Cursor.SetCursor(cursorDef, Vector2.zero, CursorMode.Auto);
    }

    public void RestartGame()// setup all game to startgame
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();

        gameTime = 60f;

        Cursor.SetCursor(cursorShoot, Vector2.zero, CursorMode.Auto);
        GunBullets.Instance.Reload();
        gameOver = false;// restart shooting
        gameOverPanel.SetActive(false);
        gameOverScreen.SetActive(false);
        gameOverCollider.SetActive(false);
        
        //Start spawn bottles
        SpawnBottles.Instance.GetStartGame(true);

        //Restart all PowerUps UI
        PowerUps.Instance.RestartAllPowerUps();

        state = State.PlayState;
        AudioManager.instance.ButtonSound();//SFX button
    }

    public bool GetGameOverActive()
    {
        return gameOver;
    }

    public void ScorePowerUp()
    {
        scorePowerUp = true;
    }

    public void StopScorePowerUp()
    {
        scorePowerUp = false;
    }

    public float GetTime()
    {
        return gameTime;
    }
    
}

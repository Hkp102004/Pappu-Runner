using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float waitTime;
    [SerializeField] private Text score;
    [SerializeField] private Text shotScore;
    [SerializeField] private Sprite[] lives_images; //array of lives display
    [SerializeField] private Image lives_displayer;
    [SerializeField] private GameObject gameover_Screen;
    [SerializeField] private AudioSource CollectionSound;
    [SerializeField] private AudioSource GameOverSound;
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource DamageSound;
    [SerializeField] private GameObject gamewin_win; //display if all votes are collected
    [SerializeField] private GameObject gamewin_lose;  //display if even one vote is missing
    [SerializeField] private AudioSource gamewinSound;
    [SerializeField] private AudioSource gameloseSound;
    [SerializeField] private GameObject pauseMenu;
    spawner spawn;
    playerBehaviour player;
    PowerSpawner poweupSpawner;
    private int scorevar=0;
    private int shotScorevar = 0;
    void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("Spawner").GetComponent<spawner>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehaviour>();
        gameover_Screen.gameObject.SetActive(false);
        poweupSpawner = GameObject.FindGameObjectWithTag("PowerSpawner").GetComponent<PowerSpawner>();
        if(score==null)
        {
            Debug.LogError("The score text or memory crystal text is missing in uiscript");
            return;
        }
        if(spawn==null)
        {
            Debug.LogError("Spawner script is missing in UIManager script");
            return;
        }
        if(player==null)
        {
            Debug.LogError("playerBehaviour script is missing in uimanager script");
            return;
        }
        if(lives_displayer==null)
        {
            Debug.LogError("Lives displayer is missing in uimanager");
            return;
        }
        if(CollectionSound == null)
        {
            Debug.LogError("Collection sound is missing from UIManager script");
            return;
        }
        if(GameOverSound == null)
        {
            Debug.LogError("The GameoverSound is missing in UIManager script");
            return;
        }
        if(DamageSound == null)
        {
            Debug.LogError("The damage sound is missing in UIManager script");
            return;
        }
        if(gamewin_win == null)
        {
            Debug.LogError("Game win screen is missing in UIManager script");
            return;
        }
        if(gamewin_lose==null)
        {
            Debug.LogError("The losing part of the gamewin_lose screen is missing in ui manager");
        }
        if(gamewinSound == null)
        {
            Debug.LogError("Game win sound is not in uimanager script");
            return;
        }
        if(poweupSpawner == null)
        {
            Debug.LogError("powerup spawner is missing from the uiManager script");
            return;
        }
        if(bgMusic==null)
        {
            Debug.LogError("Backgorund music is missing");
            return;
        }
        if(pauseMenu == null)
        {
            Debug.LogError("pause menu is missing from the uiManager");
            return;
        }
        gamewin_win.gameObject.SetActive(false);
        gamewin_lose.gameObject.SetActive(false);
        lives_displayer.sprite = lives_images[3];
    }

    // Update is called once per frame
    void Update()
    {
        PowerCheck();
    }

    public void AddScore()
    {
        CollectionSound.Play();
        scorevar+=1;
        score.text = scorevar.ToString();
    }

    public void AddShotscore()
    {
        shotScorevar+=1;
        shotScore.text = shotScorevar.ToString();
    }

    public void UpdateLive(int lives)
    {
        lives_displayer.sprite = lives_images[lives];
        DamageSound.Play();
    }

    public void DeadScreen()
    {
        gameover_Screen.gameObject.SetActive(true);
        GameOverSound.Play();
        bgMusic.Stop();
        spawn.StopSpawning();
    }

    public void WinScreen()
    {
        if(scorevar == 8)
        {
            gamewin_win.gameObject.SetActive(true);
            gamewinSound.Play();
        }
        else
        {
            gamewin_lose.gameObject.SetActive(true);
            gameloseSound.Play();
        }
        // gamewin_win.gameObject.SetActive(true);  //working on this shi
        // gamewinSound.Play();
    }

    public void PowerCheck()
    {
        if(shotScorevar == 20)
        {
            poweupSpawner.spawn();
            shotScorevar = 0;
            shotScore.text = shotScorevar.ToString();
        }
    }

    public void paused()
    {
        pauseMenu.gameObject.SetActive(true);
        spawn.StopSpawning();
        player.stopPlayer();
        bgMusic.Stop();
    }
}
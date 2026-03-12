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
    [SerializeField] private AudioSource DamageSound;
    [SerializeField] private GameObject gamewinScreen;
    [SerializeField] private AudioSource gamewinSound;
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
        if(gamewinScreen == null)
        {
            Debug.LogError("Game win screen is missing in UIManager script");
            return;
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
        gamewinScreen.gameObject.SetActive(false);
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
        spawn.StopSpawning();
    }

    public void WinScreen()
    {
        gamewinSound.Play();
        gamewinScreen.gameObject.SetActive(true);
    }

    public void PowerCheck()
    {
        if(shotScorevar == 40)
        {
            poweupSpawner.spawn();
            shotScorevar = 0;
            shotScore.text = shotScorevar.ToString();
        }
    }
}
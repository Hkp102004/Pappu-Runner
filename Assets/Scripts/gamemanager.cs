using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    UIManager ui;
    bool paused;
    public void Restart()
    {
        SceneManager.LoadScene(1); // 1 is for game scene
    }
    public void Main_menu()
    {
        SceneManager.LoadScene(0); //0 is for main menu scene
    }

    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();

        if(ui == null)
        {
            Debug.LogError("the uiManager script is missing in gamemanager");
            return;
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused) play();
            else pause();
        }
    }

    private void pause() //to start the pause menu
    {
        ui.paused();
        paused = true;
        Time.timeScale=0f;
    }

    public void play() //to resume the game
    {
        ui.play();
        Time.timeScale =1f;
        paused = false;
    }
}

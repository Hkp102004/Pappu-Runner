using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    UIManager ui;
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
        pause();
    }

    public void pause() //to start the pause menu
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ui.paused();
        }
    }

    public void play() //to resume the game
    {
        ui.play();
    }
}

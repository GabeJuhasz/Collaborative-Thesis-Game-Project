using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausmenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);
        //Time.timeScale = 1.0f;
    }

    void Update()
    {
        if
            (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                play();
            else 
                Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void play()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void MainMenu()
    {
        
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("mainmenu");
    }







}

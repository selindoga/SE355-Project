using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int game_scene_indx = 1;
    public Text high_score;

    void Start()
    {
        LoadHighScore();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(game_scene_indx);
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        LoadHighScore();
    }

    private void LoadHighScore()
    {
        high_score.text = "High Score: " + PlayerPrefs.GetInt("high score", 0);
    }
}

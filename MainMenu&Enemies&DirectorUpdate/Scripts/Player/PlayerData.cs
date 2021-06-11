using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public int max_health = 3;
    public SpaceshipWing[] left_wings;
    public SpaceshipWing[] right_wings;

    private int health;

    void Start()
    {
        SetHealth(max_health);
    }

    public void Damage(int damage)
    {
        SetHealth(health - damage);

        if (health <= 0)
        {
            SetHighScore();
            SceneManager.LoadScene(0);
        }
    }

    public void CheckWingsForIncrement()
    {
        CheckLeftWingsForIncrement();
        CheckRightWingsForIncrement();
    }

    private void CheckLeftWingsForIncrement()
    {
        for (int i = 0; i < left_wings.Length; i++)
        {
            if (!left_wings[i].gameObject.activeSelf)
            {
                left_wings[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    private void CheckRightWingsForIncrement()
    {
        for (int i = 0; i < right_wings.Length; i++)
        {
            if (!right_wings[i].gameObject.activeSelf)
            {
                right_wings[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    private void SetHealth(int value)
    {
        health = value;
        UserInterface.Instance.UpdateHealth(health);
    }

    private void SetHighScore()
    {
        if (UserInterface.Instance.getScore() > PlayerPrefs.GetInt("high score", 0))
            PlayerPrefs.SetInt("high score", UserInterface.Instance.getScore());
    }
}

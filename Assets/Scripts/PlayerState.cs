using UnityEngine;


public class PlayerState : MonoBehaviour
{

    public int currentLevel = 0;
    public int currentLives;

    private int maxLives = 3;

    void Start()
    {
        currentLives = maxLives;
    }


    public void IncreaseLevel()
    {
        currentLevel++;
    }

    public void DecreaseLives()
    {
        currentLives--;
    }


}

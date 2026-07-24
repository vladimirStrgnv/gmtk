using TMPro;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{

    private PlayerState playerState;
    private ChangeScene sceneChanger;

    private TextMeshProUGUI timerText;


    private int remainingTime;
    private int roundTime;

    void Start()
    {

        playerState = GameObject.Find("PlayerState").GetComponent<PlayerState>();
        sceneChanger = GameObject.Find("SceneManager").GetComponent<ChangeScene>();

        timerText = transform.Find("Timer").GetComponent<TextMeshProUGUI>();

        roundTime = sceneChanger.levelDictionary[playerState.currentLevel].roundTime;
        remainingTime = roundTime;
        DisplayTime();

        InvokeRepeating(nameof(Countdown), 1, 1);
    }


    private void DisplayTime()
    {
        timerText.text = remainingTime.ToString() + '/' + roundTime.ToString();
    }

    private void Countdown()
    {
        remainingTime--;
        DisplayTime();

        if (remainingTime > 0)
        {
            return;
        }

        RunOutOfTime();
    }


    private void RunOutOfTime()
    {
        playerState.DecreaseLives();
        sceneChanger.GoToInstructionsScreen();
    }



    void OnDestroy()
    {
        StopAllCoroutines();
    }
}

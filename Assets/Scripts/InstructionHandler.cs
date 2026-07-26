using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstructionHandler : MonoBehaviour
{

    private PlayerState playerState;
    private ChangeScene sceneChanger;

    private Image background;

    private TextMeshProUGUI instructionText;
    private TextMeshProUGUI livesText;

    void Start()
    {
        playerState = GameObject.Find("PlayerState").GetComponent<PlayerState>();
        sceneChanger = GameObject.Find("SceneManager").GetComponent<ChangeScene>();

        background = transform.Find("Background").GetComponent<Image>();
        instructionText = transform.Find("Instruction").GetComponent<TextMeshProUGUI>();
        livesText = transform.Find("Lives").GetComponent<TextMeshProUGUI>();

        background.color = sceneChanger.levelDictionary[playerState.currentLevel + 1].color;
        instructionText.text = sceneChanger.levelDictionary[playerState.currentLevel + 1].instructionText;
        livesText.text = "Lives:" + playerState.currentLives.ToString();

        sceneChanger.Invoke("GoToNextScene", 2);
    }


    void Update()
    {
    }

}

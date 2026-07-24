using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LevelInfo
{
    [SerializeField]
    public string instructionText;
    [SerializeField]
    public Color color;
    [SerializeField]
    public string sceneName;
    [SerializeField]
    public int roundTime;
}

public class ChangeScene : MonoBehaviour
{

    [SerializeField]

    public LevelInfo[] levelDictionary;

    private GameObject playerStateGO;
    private PlayerState playerState;


    void Start()
    {
        playerStateGO = GameObject.Find("PlayerState");
        playerState = playerStateGO.GetComponent<PlayerState>();
    }

    public void GoToInstructionsScreen()
    {
        StartCoroutine(LoadInScene("InstructionScreen"));
    }

    public void GoToNextScene()
    {
        string nextScene = levelDictionary[playerState.currentLevel + 1].sceneName;

        StartCoroutine(LoadInScene(nextScene));

        playerState.IncreaseLevel();

    }

    IEnumerator LoadInScene(string sceneName)
    {

        Scene currentScene = SceneManager.GetActiveScene();


        AsyncOperation asycnLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asycnLoad.isDone)
        {
            yield return null;
        }

        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(sceneName));

        SceneManager.MoveGameObjectToScene(playerStateGO, SceneManager.GetSceneByName(sceneName));


        SceneManager.UnloadSceneAsync(currentScene);
    }
}

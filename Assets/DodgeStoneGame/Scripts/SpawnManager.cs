using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    public GameObject stone;
    public InputAction spawnAction;

    private float spawnRangeLeftX = -4;
    private float spawnPosRightX = 20;

    public float repeatTime = 1.2f;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 0.2f, repeatTime);

    }

    void Update()
    {

    }

    void Spawn()
    {
        Vector3 spawnPos = new Vector3(Random.Range(spawnRangeLeftX, spawnPosRightX),
        12, 0);

        Instantiate(stone, spawnPos,
        stone.transform.rotation);
    }
}
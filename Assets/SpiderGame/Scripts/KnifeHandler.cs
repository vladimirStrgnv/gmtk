using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnifeHandler : MonoBehaviour
{
    public InputAction moveAction;
    public InputAction hitAction;

    private ChangeScene sceneChanger;

    private bool isOver = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
        hitAction.Enable();
        sceneChanger = GameObject.Find("SceneManager").GetComponent<ChangeScene>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            transform.Translate(new Vector3(moveInput.x, moveInput.y, 0) * Time.deltaTime * 1f, Space.World);
        }


        if (hitAction.triggered)
        {
            StartCoroutine(DoHit());
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target" && !isOver)
        {
            sceneChanger.GoToInstructionsScreen();
            isOver = true;
        }
    }

    IEnumerator DoHit()
    {
        int frames = 0;

        while (frames < 30)
        {
            transform.Translate(Vector3.forward * 2f * Time.deltaTime, Space.World);
            frames++;
            yield return null;
        }

        frames = 0;

        while (frames < 30)
        {
            transform.Translate(Vector3.back * 2f * Time.deltaTime, Space.World);
            frames++;
            yield return null;
        }


    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class SwapperHandler : MonoBehaviour
{

    public InputAction moveAction;
    public InputAction hitAction;

    private Animator playerAnimator;

    private static readonly int IsHit = Animator.StringToHash("IsHit");

    private GameObject target;

    private ChangeScene sceneChanger;

    private int score = 0;

    private bool isOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
        hitAction.Enable();
        playerAnimator = GetComponent<Animator>();
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
            playerAnimator.SetTrigger(IsHit);
            if (target)
            {
                Destroy(target);
                score++;
            }
        }

        if (score >= 3 && !isOver)
        {
            sceneChanger.GoToInstructionsScreen();
            isOver = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.tag == "Target")
        {
            Debug.Log("aaaaaaaa");
            target = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            target = null;
        }
    }

}

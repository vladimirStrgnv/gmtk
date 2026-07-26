
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class RockController : MonoBehaviour
{

    public float speed = 1.0f;
    public InputAction moveAction;
    public Vector2 moveInput;
    private float lastDirection = 1;

    private Animator anim;
    private bool alive = true;

    private ChangeScene sceneChanger;

    AudioSource source;
    public AudioClip clip;

    void Start()
    {
        moveAction.Enable();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        sceneChanger = GameObject.Find("SceneManager").GetComponent<ChangeScene>();

    }

    void Update()
    {


        Move();




    }



    void Move()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.x > 0)
        {

            transform.Translate(Vector3.right * Time.deltaTime * speed);


        }

        if (moveInput.x < 0)
        {

            transform.Translate(Vector3.left * Time.deltaTime * speed);


        }

        if (transform.position.x < -4)
        {
            transform.position = new Vector3(-4, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 20)
        {
            transform.position = new Vector3(20, transform.position.y, transform.position.z);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);

        }

        if (other.gameObject.CompareTag("Runner"))
        {

            sceneChanger.GoToInstructionsScreen();
        }
    }


}
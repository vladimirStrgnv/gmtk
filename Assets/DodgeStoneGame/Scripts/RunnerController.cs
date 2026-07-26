using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class RunnerController : MonoBehaviour
{

    public float speed = 1.0f;
    public InputAction moveAction;
    public Vector2 moveInput;
    private float lastDirection = 1;

    private Animator anim;
    private bool alive = true;

    AudioSource source;
    public AudioClip clip;

    private PlayerState playerState;
    private ChangeScene sceneChanger;


    void Start()
    {
        moveAction.Enable();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        playerState = GameObject.Find("PlayerState").GetComponent<PlayerState>();
        sceneChanger = GameObject.Find("SceneManager").GetComponent<ChangeScene>();


    }

    void Update()
    {

        if (alive)
        {
            Move();
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rock") && alive && other.gameObject.transform.position.y > 2.0f)
        {
            alive = false;
            anim.SetBool("death", true);
            source.PlayOneShot(clip);

            playerState.DecreaseLives();
            sceneChanger.GoToInstructionsScreen();

        }
    }

    void Move()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.x > 0)
        {
            if (lastDirection < 0)
            {
                transform.Rotate(new Vector3(0, 180, 0));

            }
            transform.Translate(Vector3.forward * Time.deltaTime * speed);


        }

        if (moveInput.x < 0)
        {
            if (lastDirection > 0)
            {
                transform.Rotate(new Vector3(0, -180, 0));

            }
            transform.Translate(Vector3.forward * Time.deltaTime * speed);


        }
        if (moveInput.x != 0)
        {
            lastDirection = moveInput.x;
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


}
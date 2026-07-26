using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class RunSideToSide : MonoBehaviour
{

    public float speed = 1.0f;
    public InputAction moveAction;
    public Vector2 moveInput;

    private Animator anim;
    private bool alive = true;

    AudioSource source;
    public AudioClip clip;

    public string currentDirection = "right";

    void Start()
    {
        moveAction.Enable();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

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


        }
    }

    

    void Move()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if (currentDirection == "right")
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);


        }

        if (currentDirection == "left")
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);


        }

        if (currentDirection == "left" && transform.position.x < -4)
        {
            currentDirection = "right";
            transform.Rotate(new Vector3(0, 180, 0));
        }
        if (currentDirection == "right" && transform.position.x > 20)
        {
            currentDirection = "left";
            transform.Rotate(new Vector3(0, 180, 0));

        }

    }


}
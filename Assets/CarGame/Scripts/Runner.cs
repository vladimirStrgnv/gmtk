using UnityEngine;

public class Runner : MonoBehaviour
{
    private Animator anim;
    [SerializeField] int speed;
    private bool alive = true;

    AudioSource source;
    public AudioClip clip;

    private CarControl car;

    void Awake()
    {
        car = FindFirstObjectByType<CarControl>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();


    }

    void Update()
    {

        if (alive) { Run(); }


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car") && alive)
        {
            alive = false;
            anim.SetBool("death", true);
            source.PlayOneShot(clip);
            car.smashCounts++;

        }
    }

    void Run()
    {
        transform.position = transform.position + new Vector3(0, 0, speed * Time.deltaTime);
    }


}
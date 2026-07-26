using UnityEngine;

public class RockRunner : MonoBehaviour
{
    private Animator anim;
    private bool alive = true;

    AudioSource source;
    public AudioClip clip;


    void Awake()
    {
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();


    }

    void Update()
    {



    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rock") && alive)
        {
            alive = false;
            anim.SetBool("death", true);

        }
    }




}
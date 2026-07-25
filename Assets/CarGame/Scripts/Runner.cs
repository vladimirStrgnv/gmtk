using UnityEngine;

public class Runner : MonoBehaviour
{
    private Animator anim;
    [SerializeField] int speed;
    private bool alive = true;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (alive) { Run(); }


    }


    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Car"))
        {
            alive = false;
            anim.SetBool("death", true);
        }
    }
 
    void Run()
    {
        transform.position = transform.position + new Vector3(0, 0, speed * Time.deltaTime);
    }


}
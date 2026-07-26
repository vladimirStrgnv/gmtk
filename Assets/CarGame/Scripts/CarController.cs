using UnityEngine;
using UnityEngine.InputSystem;


public enum GameModes
{
    Dodge,
    Smash,

}

public class CarControl : MonoBehaviour
{
    [Header("Car Properties")]
    public float motorTorque = 2000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 20f;
    public float steeringRange = 30f;
    public float steeringRangeAtMaxSpeed = 10f;
    public float centreOfGravityOffset = -1f;

    public int smashCounts = 0;
    public int smashCountsToWin = 4;

    private WheelControl[] wheels;
    private Rigidbody rigidBody;

    private PlayerState playerState;
    private ChangeScene sceneChanger;

    private bool isOver = false;

    [SerializeField] GameModes gameMode;


    [Header("Input Actions")]
    public InputActionReference moveAction;

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        Vector3 centerOfMass = rigidBody.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        rigidBody.centerOfMass = centerOfMass;

        wheels = GetComponentsInChildren<WheelControl>();

        playerState = GameObject.Find("PlayerState").GetComponent<PlayerState>();
        sceneChanger = GameObject.Find("SceneManager").GetComponent<ChangeScene>();

    }

    void Update()
    {
        if (gameMode == GameModes.Smash)
        {
            if (smashCountsToWin == smashCounts && !isOver)
            {
                sceneChanger.GoToInstructionsScreen();
                isOver = true;
            }
        }
    }

    void FixedUpdate()
    {
        float vInput = moveAction.action.ReadValue<Vector2>().y;
        float hInput = moveAction.action.ReadValue<Vector2>().x;

        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));

        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

        foreach (var wheel in wheels)
        {
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
            }

            if (isAccelerating)
            {
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;
                }
                wheel.WheelCollider.brakeTorque = 0f;
            }
            else
            {
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameMode == GameModes.Dodge && collision.gameObject.tag == "Runner")
        {
            playerState.DecreaseLives();
            sceneChanger.GoToInstructionsScreen();
            return;
        }


    }

}

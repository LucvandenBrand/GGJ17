using UnityEngine;

/* Allows users to control their player with keyboard and xbox controllers. */
public class Control : MonoBehaviour
{
    private const string STICK_HORIZONTAL = "HSP";
    private const string STICK_VERTICAL = "VSP";

    [SerializeField]
    private float movementSpeed = 1f;
    [SerializeField]
    private int playerNumber = -1;

    // Movement directions.
    private float horInput;
    private float verInput;

    private Rigidbody2D rigidbodyCurrent;

    public void Start()
    {
        rigidbodyCurrent = gameObject.GetComponent<Rigidbody2D>();
    }

    // Set the player number, for which the controls will be read.
    public void SetPlayerNumber(int player)
    {
        playerNumber = player;
    }

    // Process movement input into physics force every frame.
    private void Update()
    {
        PollInput();
        Move();
    }

    // Read the axis values and store them.
    private void PollInput()
    {
        horInput = Input.GetAxisRaw(STICK_HORIZONTAL + playerNumber);
        verInput = Input.GetAxisRaw(STICK_VERTICAL + playerNumber);
    }
    
    // Use the current direction and speed to move.
    private void Move()
    {
        float independentSpeed = movementSpeed * Time.deltaTime;
        float angle = Mathf.Atan2(verInput, horInput);
        float amplitude = Mathf.Sqrt(verInput * verInput + horInput * horInput);

        amplitude = Mathf.Clamp(amplitude, 0, 1) * independentSpeed;
        Vector2 movementVector = new Vector2(Mathf.Cos(angle) * amplitude, Mathf.Sin(angle) * amplitude);
        rigidbodyCurrent.AddForce(movementVector);
    }

    // Return the current direction.
    public Vector2 GetDirection()
    {
        return new Vector2(horInput, verInput);
    }
    
    // Change the current speed of movement.
    public void SetMovementSpeed(float speed)
    {
        this.movementSpeed = speed;
    }
}

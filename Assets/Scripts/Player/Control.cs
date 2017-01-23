using UnityEngine;
using System.Collections;
using System;

public class Control : MonoBehaviour
{
    private string leftStickHorName = "HorizontalLeftStick";

    private string leftStickVerName = "VerticalLeftStick";

    private string rightStickHorName = "HorizontalRightStick";

    private string rightStickVerName = "VerticalRightStick";

    private string leftTriggerName = "LeftTrigger";

    private string rightTriggerName = "RightTrigger";

    private float horInput;

    private float verInput;

    public float movementSpeed = 1f;

    [SerializeField]
    private int playerNumberLeft = 0;

    [SerializeField]
    private int playerNumberRight = 0;

    [SerializeField]
    private bool arrowKeysPlayer = false;
    [SerializeField]
    private bool wasdKeysPlayer = false;

    private Rigidbody2D rigidbodyCurrent;


    // Use this for initialization
    private void Start()
    {
        rigidbodyCurrent = gameObject.GetComponent<Rigidbody2D>();
    }

    public void SetLeftPlayerNumber(int playerNr)
    {
        playerNumberLeft = playerNr;
    }

    public void SetRightPlayerNumber(int playerNr)
    {
        playerNumberRight = playerNr;
    }
    public void SetArrowKeysPlayer()
    {
        arrowKeysPlayer = true;
    }

    public void SetWASDKeysPlayer()
    {
        wasdKeysPlayer = true;
    }

    // Update is called once per frame
    private void Update()
    {
        AssignInput();
        float independentSpeed = movementSpeed * Time.deltaTime;
        float angle = Mathf.Atan2(verInput, horInput);
        float amplitude = Mathf.Sqrt(verInput * verInput + horInput * horInput);

        amplitude = Mathf.Clamp(amplitude, 0, 1) * independentSpeed;
        Vector2 movementVector = new Vector2(Mathf.Cos(angle) * amplitude, Mathf.Sin(angle) * amplitude);
        
        rigidbodyCurrent.AddForce(movementVector);
    }

    private void AssignInput()
    {
        if (arrowKeysPlayer || wasdKeysPlayer)
        {
            int arrUp, arrDown, arrLeft, arrRight;
            if(arrowKeysPlayer)
            {
                arrUp = Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
                arrDown = Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
                arrLeft = Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
                arrRight = Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
            }
            else
            {
                arrUp = Input.GetKey(KeyCode.W) ? 1 : 0;
                arrDown = Input.GetKey(KeyCode.S) ? 1 : 0;
                arrLeft = Input.GetKey(KeyCode.A) ? 1 : 0;
                arrRight = Input.GetKey(KeyCode.D) ? 1 : 0;
            }
            verInput = arrUp - arrDown;
            horInput = arrRight - arrLeft;
        }

        else if (playerNumberLeft > 0 && playerNumberLeft <= 4)
        {
            horInput = Input.GetAxisRaw(leftStickHorName + playerNumberLeft);

            verInput = Input.GetAxisRaw(leftStickVerName + playerNumberLeft);
        }

        else if (playerNumberRight > 0 && playerNumberRight <= 4)
        {
            horInput = Input.GetAxisRaw(rightStickHorName + (playerNumberRight));

            verInput = Input.GetAxisRaw(rightStickVerName + (playerNumberRight));
        }
    }

    public Vector2 GetDirection()
    {
        return new Vector2(horInput, verInput);
    }
}

﻿using UnityEngine;

/* Allows users to control their player with keyboard and xbox controllers. */
public class Control : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 1f;
    [SerializeField]
    private int playerNumberLeft = 0;
    [SerializeField]
    private int playerNumberRight = 0;
    [SerializeField]
    private bool arrowKeysPlayer = false;
    [SerializeField]
    private bool wasdKeysPlayer = false;
    [SerializeField]
    private bool ijklKeysPlayer = false;
    [SerializeField]
    private bool numpadKeysPlayer = false;

    private string leftStickHorName = "HorizontalLeftStick";
    private string leftStickVerName = "VerticalLeftStick";
    private string rightStickHorName = "HorizontalRightStick";
    private string rightStickVerName = "VerticalRightStick";
    private string leftTriggerName = "LeftTrigger";
    private string rightTriggerName = "RightTrigger";
    private float horInput;
    private float verInput;
    private Rigidbody2D rigidbodyCurrent;

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

    public void SetIJKLKeysPlayer()
    {
        ijklKeysPlayer = true;
    }

    public void SetNumpadKeysPlayer()
    {
        numpadKeysPlayer = true;
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
        if (arrowKeysPlayer || wasdKeysPlayer || ijklKeysPlayer || numpadKeysPlayer)
        {
            int arrUp, arrDown, arrLeft, arrRight;
            if(arrowKeysPlayer)
            {
                arrUp = Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
                arrDown = Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
                arrLeft = Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
                arrRight = Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
            }
            else if(wasdKeysPlayer)
            {
                arrUp = Input.GetKey(KeyCode.W) ? 1 : 0;
                arrDown = Input.GetKey(KeyCode.S) ? 1 : 0;
                arrLeft = Input.GetKey(KeyCode.A) ? 1 : 0;
                arrRight = Input.GetKey(KeyCode.D) ? 1 : 0;
            }
            else if (ijklKeysPlayer)
            {
                arrUp = Input.GetKey(KeyCode.I) ? 1 : 0;
                arrDown = Input.GetKey(KeyCode.K) ? 1 : 0;
                arrLeft = Input.GetKey(KeyCode.J) ? 1 : 0;
                arrRight = Input.GetKey(KeyCode.L) ? 1 : 0;
            }
            else
            {
                arrUp = Input.GetKey(KeyCode.Keypad8) ? 1 : 0;
                arrDown = Input.GetKey(KeyCode.Keypad5) ? 1 : 0;
                arrLeft = Input.GetKey(KeyCode.Keypad4) ? 1 : 0;
                arrRight = Input.GetKey(KeyCode.Keypad6) ? 1 : 0;
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

    public void SetMovementSpeed(float speed)
    {
        this.movementSpeed = speed;
    }
}

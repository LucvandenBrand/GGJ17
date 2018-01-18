using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/* Listens for key presses and assigns players to the individual pressing the key. */

/* This entire class deserves to be set on fire. 
 * I strongly suggest running away now, whilst you are still clean. 
 * The things that happened here should never be spoken about again. 
 * There is no god.
 * - Luc */
public class LobbySpawn : MonoBehaviour {
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject playerSpawn;

    Control controllerScript;
    private string leftTriggerName = "LeftTrigger";
    private string rightTriggerName = "RightTrigger";

    private List<int> assignedLeftPlayerIndexies = new List<int>();
    private List<int> assignedRightPlayerIndexies = new List<int>();
    private List<int> leftControllerNumberIndex = new List<int>();
    private List<int> rightControllerNumberIndex = new List<int>();

    int leftControllerNumber = 0;
    int rightControllerNumber = 0;

    /* Array to track which controller has been connected. 
     * Initialized at false. */
    bool[] controllerConnected = new bool[8];

    /* Array to track which player has been spawned. 
     * Initialized at false. */
    bool[] playerSpawned = new bool[8];

    private bool arrowKeysPlayerConnected = false;
    private bool wasdKeysPlayerConnected = false;
    private bool ijklKeysPlayerConnected = false;
    private bool numpadKeysPlayerConnected = false;

    void Update() {
        float threshold = 0.75f;

        for (int player = 0; player < 8; player++) {
            int axis = player / 2 + 1;

            if (player % 2 == 0) {
                // Uneven, the left trigger.
                string axisName = leftTriggerName + axis;
                if (!playerSpawned[player] && (Input.GetAxisRaw(axisName) > threshold))
                {
                    leftControllerNumber = axis;
                    leftControllerNumberIndex.Insert(axis - 1, leftControllerNumber);
                    controllerConnected[player] = true;
                }
            } else {
                // Even, the right trigger.
                string axisName = rightTriggerName + axis;

                if (!playerSpawned[player] && (Input.GetAxisRaw(axisName) > threshold))
                {
                    rightControllerNumber = axis;
                    rightControllerNumberIndex.Insert(axis - 1, rightControllerNumber);
                    controllerConnected[player] = true;
                }
            }
        }

        foreach (int leftControllerNumber in leftControllerNumberIndex)
        {
            if (leftControllerNumber == 1 && controllerConnected[0] && !playerSpawned[0])
            {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
                go.GetComponent<Control>().SetLeftPlayerNumber(leftControllerNumber);
                assignedLeftPlayerIndexies.Add(leftControllerNumber);
                playerSpawned[0] = true;
            }

            else if (leftControllerNumber == 2 && controllerConnected[2] && !playerSpawned[2])
            {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
                go.GetComponent<Control>().SetLeftPlayerNumber(leftControllerNumber);
                assignedLeftPlayerIndexies.Add(leftControllerNumber);
                playerSpawned[2] = true;
            }

            else if (leftControllerNumber == 3 && controllerConnected[4] && !playerSpawned[4])
            {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
                go.GetComponent<Control>().SetLeftPlayerNumber(leftControllerNumber);
                assignedLeftPlayerIndexies.Add(leftControllerNumber);
                playerSpawned[4] = true;
            }

            else if (leftControllerNumber == 4 && controllerConnected[6] && !playerSpawned[6])
            {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
                go.GetComponent<Control>().SetLeftPlayerNumber(leftControllerNumber);
                assignedLeftPlayerIndexies.Add(leftControllerNumber);
                playerSpawned[6] = true;
            }

        }

        foreach (int rightControllerNumber in rightControllerNumberIndex)
        {
            if (rightControllerNumber == 1 && controllerConnected[1] && !playerSpawned[1])
            {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
                go.GetComponent<Control>().SetRightPlayerNumber(rightControllerNumber);
                assignedLeftPlayerIndexies.Add(rightControllerNumber);
                playerSpawned[1] = true;
            }

            else if (rightControllerNumber == 2 && controllerConnected[3] && !playerSpawned[3])
            {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
                go.GetComponent<Control>().SetRightPlayerNumber(rightControllerNumber);
                assignedLeftPlayerIndexies.Add(rightControllerNumber);
                playerSpawned[3] = true;
            }

            else if (rightControllerNumber == 3 && controllerConnected[5] && !playerSpawned[5])
            {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
                go.GetComponent<Control>().SetRightPlayerNumber(rightControllerNumber);
                assignedLeftPlayerIndexies.Add(rightControllerNumber);
                playerSpawned[5] = true;
            }

            else if (rightControllerNumber == 4 && controllerConnected[7] && !playerSpawned[7])
            {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
                go.GetComponent<Control>().SetRightPlayerNumber(rightControllerNumber);
                assignedLeftPlayerIndexies.Add(rightControllerNumber);
                playerSpawned[7] = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !arrowKeysPlayerConnected)
        {
            GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
            go.GetComponent<Control>().SetArrowKeysPlayer();
            arrowKeysPlayerConnected = true;
        }
        
        if (Input.GetKeyDown(KeyCode.W) && !wasdKeysPlayerConnected)
        {
            GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
            go.GetComponent<Control>().SetWASDKeysPlayer();
            wasdKeysPlayerConnected = true;
        }
        
        if (Input.GetKeyDown(KeyCode.I) && !ijklKeysPlayerConnected)
        {
            GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
            go.GetComponent<Control>().SetIJKLKeysPlayer();
            ijklKeysPlayerConnected = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad8) && !numpadKeysPlayerConnected)
        {
            GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
            go.GetComponent<Control>().SetNumpadKeysPlayer();
            numpadKeysPlayerConnected = true;
        }

        OnButtonStartGame();
    }

    private void LateUpdate()
    {
        ClampPlayers();
    }

    /* Players are disallowed to move outside of the screen. Preventing death. */
    void ClampPlayers()
    {
        for (int index = 0; index < playerSpawn.transform.childCount; ++index)
        {
            var pos = Camera.main.WorldToViewportPoint(playerSpawn.transform.GetChild(index).transform.position);
            pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
            pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
            playerSpawn.transform.GetChild(index).transform.position = Camera.main.ViewportToWorldPoint(pos);
        }
    }

    /* Checks if the player wants to start the game and leave the lobby.
     * If so, we load the Game Scene whilst keeping the players loaded. */
    void OnButtonStartGame()
    {
        if (Input.GetButtonDown("StartButtonJ1") || Input.GetKeyDown(KeyCode.Return))
        {
            if (assignedLeftPlayerIndexies.Count + assignedRightPlayerIndexies.Count > 0 
                || arrowKeysPlayerConnected || wasdKeysPlayerConnected 
                || ijklKeysPlayerConnected  || numpadKeysPlayerConnected)
            {
                Object.DontDestroyOnLoad(playerSpawn);
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
        }
    }
}
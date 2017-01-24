using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LobbySpawn : MonoBehaviour {

    Control controllerScript;

    private string leftTriggerName = "LeftTrigger";

    private string rightTriggerName = "RightTrigger";

    private List<int> assignedLeftPlayerIndexies = new List<int>();

    private List<int> assignedRightPlayerIndexies = new List<int>();

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject playerSpawn;

    private List<int> leftControllerNumberIndex = new List<int>();

    private List<int> rightControllerNumberIndex = new List<int>();

    int leftControllerNumber = 0;

    int rightControllerNumber = 0;

    private bool player1spawned = false;
    private bool player2spawned = false;
    private bool player3spawned = false;
    private bool player4spawned = false;
    private bool player5spawned = false;
    private bool player6spawned = false;
    private bool player7spawned = false;
    private bool player8spawned = false;

    private bool j1LeftConnected = false;
    private bool j2LeftConnected = false;
    private bool j3LeftConnected = false;
    private bool j4LeftConnected = false;

    private bool j1RightConnected = false;
    private bool j2RightConnected = false;
    private bool j3RightConnected = false;
    private bool j4RightConnected = false;

    private bool arrowKeysPlayerConnected = false;
    private bool wasdKeysPlayerConnected = false;
    private bool ijklKeysPlayerConnected = false;
    private bool numpadKeysPlayerConnected = false;

    void Start() {

    }


    void Update() {

            if (player1spawned == false && (Input.GetAxisRaw(leftTriggerName + 1) > 0.75f))
            {
                leftControllerNumber = 1;

            leftControllerNumberIndex.Insert(0, leftControllerNumber);

            j1LeftConnected = true;
            }

            else if (player2spawned == false && (Input.GetAxisRaw(rightTriggerName + 1) > 0.75f))
        {
            rightControllerNumber = 1;

            rightControllerNumberIndex.Insert(0, rightControllerNumber);

            j1RightConnected = true;
        }

            else if (player3spawned == false && (Input.GetAxisRaw(leftTriggerName + 2) > 0.75f))
        {
            leftControllerNumber = 2;

            leftControllerNumberIndex.Insert(1, leftControllerNumber);

            j2LeftConnected = true;
        }

        else if (player4spawned == false && (Input.GetAxisRaw(rightTriggerName + 2) > 0.75f))
        {
            rightControllerNumber = 2;

            rightControllerNumberIndex.Insert(1, rightControllerNumber);

            j2RightConnected = true;
        }

        else if (player5spawned == false && (Input.GetAxisRaw(leftTriggerName + 3) > 0.75f))
        {
            leftControllerNumber = 3;

            leftControllerNumberIndex.Insert(2, leftControllerNumber);

            j3LeftConnected = true;
        }

        else if (player6spawned == false && (Input.GetAxisRaw(rightTriggerName + 3) > 0.75f))
        {
            rightControllerNumber = 3;

            rightControllerNumberIndex.Insert(2, rightControllerNumber);

            j3RightConnected = true;
        }

        else if (player7spawned == false && (Input.GetAxisRaw(leftTriggerName + 4) > 0.75f))
        {
            leftControllerNumber = 4;

            leftControllerNumberIndex.Insert(3, leftControllerNumber);

            j4LeftConnected = true;
        }

        else if (player8spawned == false && (Input.GetAxisRaw(leftTriggerName + 4) > 0.75f))
        {
            leftControllerNumber = 4;

            leftControllerNumberIndex.Insert(3, leftControllerNumber);

            j4LeftConnected = true;
        }








        foreach (int leftControllerNumber in leftControllerNumberIndex)
            {
                if (leftControllerNumber == 1 && j1LeftConnected == true && player1spawned == false)
                {
                    GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);

                go.GetComponent<Control>().SetLeftPlayerNumber(leftControllerNumber);

                    assignedLeftPlayerIndexies.Add(leftControllerNumber);

                    player1spawned = true;

            //controllerNumberIndex.Remove(controllerNumber);
                 }

                else if (leftControllerNumber == 2 && j2LeftConnected == true && player3spawned == false)
                {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);

                go.GetComponent<Control>().SetLeftPlayerNumber(leftControllerNumber);

                assignedLeftPlayerIndexies.Add(leftControllerNumber);

                player3spawned = true;

                //controllerNumberIndex.Remove(controllerNumber);
                }

            else if (leftControllerNumber == 3 && j3LeftConnected == true && player5spawned == false)
            {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);

                go.GetComponent<Control>().SetLeftPlayerNumber(leftControllerNumber);

                assignedLeftPlayerIndexies.Add(leftControllerNumber);

                player5spawned = true;

                //controllerNumberIndex.Remove(controllerNumber);
            }

            else if (leftControllerNumber == 4 && j4LeftConnected == true && player7spawned == false)
            {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);

                go.GetComponent<Control>().SetLeftPlayerNumber(leftControllerNumber);

                assignedLeftPlayerIndexies.Add(leftControllerNumber);

                player7spawned = true;

                //controllerNumberIndex.Remove(controllerNumber);
            }

        }

        foreach (int rightControllerNumber in rightControllerNumberIndex)
        {
            if (rightControllerNumber == 1 && j1RightConnected == true && player2spawned == false)
            {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);

                go.GetComponent<Control>().SetRightPlayerNumber(rightControllerNumber);

                assignedLeftPlayerIndexies.Add(rightControllerNumber);

                player2spawned = true;

                //controllerNumberIndex.Remove(controllerNumber);
            }

            else if (rightControllerNumber == 2 && j2RightConnected == true && player4spawned == false)
            {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);

                go.GetComponent<Control>().SetRightPlayerNumber(rightControllerNumber);

                assignedLeftPlayerIndexies.Add(rightControllerNumber);

                player4spawned = true;

                //controllerNumberIndex.Remove(controllerNumber);
            }

            else if (rightControllerNumber == 3 && j3RightConnected == true && player6spawned == false)
            {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);

                go.GetComponent<Control>().SetRightPlayerNumber(rightControllerNumber);

                assignedLeftPlayerIndexies.Add(rightControllerNumber);

                player6spawned = true;

                //controllerNumberIndex.Remove(controllerNumber);
            }

            else if (rightControllerNumber == 4 && j4RightConnected == true && player8spawned == false)
            {
                GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);

                go.GetComponent<Control>().SetRightPlayerNumber(rightControllerNumber);

                assignedLeftPlayerIndexies.Add(rightControllerNumber);

                player8spawned = true;

                //controllerNumberIndex.Remove(controllerNumber);
            }
        }



            /*
            string[] inputNames = Input.GetJoystickNames();

            int controllerCount = 0;

            for (int i = 0; i < inputNames.Length; i++) {
                string joystickname = inputNames[i];

                if (joystickname == "Controller (Xbox 360 Wireless Receiver for Windows)") {
                    controllerCount++;
                }
            }

            for (int i = 1; i <= controllerCount; i++) {

                if (assignedLeftPlayerIndexies.Contains(i) == false  &&  Input.GetAxisRaw(leftTriggerName + i) > 0.75f) {
                    GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);

                    go.GetComponent<Control>().SetLeftPlayerNumber(i);

                    assignedLeftPlayerIndexies.Add(i);

                } else if (assignedRightPlayerIndexies.Contains(i) == false  &&  Input.GetAxisRaw(rightTriggerName + i) > 0.75f) {
                    GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);

                    go.GetComponent<Control>().SetRightPlayerNumber(i);

                    assignedRightPlayerIndexies.Add(i);
                }

            }
            */
            // GameObject arrow_keys_player = Instantiate(playerPrefab, playerSpawn.gameObject.transform);

        if( Input.GetKeyDown(KeyCode.UpArrow) && !arrowKeysPlayerConnected)
        {
            GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
            go.GetComponent<Control>().SetArrowKeysPlayer();
            arrowKeysPlayerConnected = true;
        }
        
        if( Input.GetKeyDown(KeyCode.W) && !wasdKeysPlayerConnected)
        {
            GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
            go.GetComponent<Control>().SetWASDKeysPlayer();
            wasdKeysPlayerConnected = true;
        }
        
        if( Input.GetKeyDown(KeyCode.I) && !ijklKeysPlayerConnected)
        {
            GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
            go.GetComponent<Control>().SetIJKLKeysPlayer();
            ijklKeysPlayerConnected = true;
        }
        
        if( Input.GetKeyDown(KeyCode.Keypad8) && !numpadKeysPlayerConnected)
        {
            GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
            go.GetComponent<Control>().SetNumpadKeysPlayer();
            numpadKeysPlayerConnected = true;
        }

        OnButtonStartGame();
    }


    private void LateUpdate() {
        clampPlayers();
    }


    void clampPlayers() {

        for (int index = 0; index < playerSpawn.transform.childCount; ++index) {
            var pos = Camera.main.WorldToViewportPoint(playerSpawn.transform.GetChild(index).transform.position);
            pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
            pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
            playerSpawn.transform.GetChild(index).transform.position = Camera.main.ViewportToWorldPoint(pos);
        }
    }


    void OnButtonStartGame() {
        if (Input.GetButtonDown("StartButtonJ1") == true || Input.GetKeyDown(KeyCode.Return) == true) {
            if (assignedLeftPlayerIndexies.Count + assignedRightPlayerIndexies.Count > 0 || arrowKeysPlayerConnected || wasdKeysPlayerConnected || ijklKeysPlayerConnected || numpadKeysPlayerConnected) {
                Object.DontDestroyOnLoad(playerSpawn);

                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
        }
    }
}

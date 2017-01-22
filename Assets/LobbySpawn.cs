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



    void Start() {

    }


    void Update() {

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
        if (Input.GetButtonDown("StartButtonJ1") == true) {
            if (assignedLeftPlayerIndexies.Count + assignedRightPlayerIndexies.Count > 0) {
                Object.DontDestroyOnLoad(playerSpawn);

                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
        }
    }
}

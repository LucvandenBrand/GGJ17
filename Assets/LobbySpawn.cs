using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LobbySpawn : MonoBehaviour {

    Control controllerScript;

    private string leftTriggerName = "LeftTrigger";

    private string rightTriggerName = "RightTrigger";

    private List<int> assignedLeftPlayers = new List<int>();

    private List<int> assignedRightPlayers = new List<int>();

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject players;

    // Use this for initialization
    void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {

        string[] inputNames = Input.GetJoystickNames();
        int controllerCount = 0;

        for (int i = 0; i < inputNames.Length; i++)
        {
            string joystickname = inputNames[i];

            if (joystickname == "Controller (Xbox 360 Wireless Receiver for Windows)")
            {
                controllerCount++;
            }
        }

        for (int i = 1; i <= controllerCount; i++)
        {

            if (!assignedLeftPlayers.Contains(i) && Input.GetAxisRaw(leftTriggerName + i) == 1f)
            {
                GameObject go = Instantiate(playerPrefab, players.gameObject.transform);

                go.GetComponent<Control>().SetLeftPlayerNumber(i);

                assignedLeftPlayers.Add(i);

            }
            else if (!assignedRightPlayers.Contains(i) && Input.GetAxisRaw(rightTriggerName + i) == 1f)
            {
                GameObject go = Instantiate(playerPrefab, players.gameObject.transform);

                go.GetComponent<Control>().SetRightPlayerNumber(i);
                    
                assignedRightPlayers.Add(i);
            }
            
        }

        

        OnButtonStartGame();
    }

    private void LateUpdate()
    {
        clampPlayers();
    }

    void clampPlayers()
    {
        
        for(int index = 0; index < players.transform.childCount; ++index)
        {
            var pos = Camera.main.WorldToViewportPoint(players.transform.GetChild(index).transform.position);
            pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
            pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
            players.transform.GetChild(index).transform.position = Camera.main.ViewportToWorldPoint(pos);
        }
    }

    void OnButtonStartGame()
    {
        if (Input.GetButtonDown("StartButtonJ1") == true)
        {

            Object.DontDestroyOnLoad(players);

            SceneManager.LoadScene(2,LoadSceneMode.Single);
        }
    }
}

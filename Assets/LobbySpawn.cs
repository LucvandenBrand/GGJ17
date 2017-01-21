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

    public int playerCount = 0;

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

        string[] inputNames = Input.GetJoystickNames();    // random array of joysticks

        for (int i = 1; i <= inputNames.Length; i++)        // loop array
        {
            string joystickname = inputNames[i-1];

            if (joystickname == "Controller (Xbox 360 Wireless Receiver for Windows)")
            {
                Debug.Log( "This is a XBOX piece of shit" );
                if (assignedLeftPlayers.Contains(i) == false && Input.GetAxisRaw(leftTriggerName + i) > 0.75f)
                {
                    GameObject go = Instantiate(playerPrefab, players.gameObject.transform);

                    go.GetComponent<Control>().SetLeftPlayerNumber(i); 

                    Debug.Log( "New Left" );

                    playerCount++;

                    assignedLeftPlayers.Add(i);

                }
                else if (assignedRightPlayers.Contains(i) == false && Input.GetAxisRaw(rightTriggerName + i) > 0.75f)
                {
                    GameObject go = Instantiate(playerPrefab, players.gameObject.transform);

                    go.GetComponent<Control>().SetRightPlayerNumber(i);

                    Debug.Log( "New Right" );

                    playerCount++;
                    
                    assignedRightPlayers.Add(i);
                }
            }
        }

        OnButtonStartGame();
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

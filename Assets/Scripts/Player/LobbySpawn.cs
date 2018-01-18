using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/* Listens for key presses and assigns players to the individual pressing the key. */
public class LobbySpawn : MonoBehaviour {
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject playerSpawn;

    Control controllerScript;
    private const string TRIGGER = "TriggerP";
    private const string START = "StartButtonJ1";

    private List<int> spawnedPlayers = new List<int>();

    private bool arrowKeysPlayerConnected = false;
    private bool wasdKeysPlayerConnected = false;
    private bool ijklKeysPlayerConnected = false;
    private bool numpadKeysPlayerConnected = false;

    void Update() {
        float threshold = 0.75f;

        /* For every player, check if their trigger is pressed.
         * If this is the case, and they are not spawned, spawn them. */
        for (int player = 0; player < 8; player++) {
            // Uneven, the left trigger.
            if (Input.GetAxisRaw(TRIGGER + player) > threshold) {
                if (!spawnedPlayers.Contains(player)) {
                    spawnedPlayers.Add(player);
                    // Create new player.
                    GameObject go = Instantiate(playerPrefab, playerSpawn.gameObject.transform);
                    go.GetComponent<Control>().SetPlayerNumber(player);
                }
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
        if (Input.GetButtonDown(START) || Input.GetKeyDown(KeyCode.Return))
        {
            if (spawnedPlayers.Count > 0 
                || arrowKeysPlayerConnected || wasdKeysPlayerConnected 
                || ijklKeysPlayerConnected  || numpadKeysPlayerConnected)
            {
                Object.DontDestroyOnLoad(playerSpawn);
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
        }
    }
}
using UnityEngine;
using System.Collections;

public class PlayerLoader : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    public AudioSystemController audioSystemController;

    public int playerCount = 0;

    //public int[] playerCount;

    // Use this for initialization
    void Start ()
    {
        string[] inputNames = Input.GetJoystickNames();

        for (int i = 0; i < inputNames.Length; i++)
        {
            string joystickname = inputNames[i];

            if (joystickname == "Controller (Xbox 360 Wireless Receiver for Windows)")
            {
                playerCount += 2;
            }
        }

        for (int i = 1; i <= playerCount; i++)
        {
            GameObject go = Instantiate(player, gameObject.transform);

            //go.GetComponent<Control>().SetPlayerNumber(i);
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

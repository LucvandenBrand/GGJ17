using UnityEngine;

/* Makes sure the object is only shown when player 
 * entities are present in the scene. */
[RequireComponent(typeof(CanvasRenderer))]
public class OnlyWithPlayers : MonoBehaviour
{
	void Update ()
    {
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        if (playerCount == 0)
            gameObject.GetComponent<CanvasRenderer>().SetAlpha(0); 
        else
            gameObject.GetComponent<CanvasRenderer>().SetAlpha(1);
    }
}

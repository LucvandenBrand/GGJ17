using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    [SerializeField] private string triggerInputName;
    public GameObject bullet;
    private float triggerInput;

    // Update is called once per frame
    void Update () {
        triggerInput = Input.GetAxisRaw(triggerInputName);
        if (triggerInput == 1)
        {
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = this.transform.position;
            Debug.Log(this.transform.eulerAngles);
            //newBullet.GetComponent<SoundMovement>().SetDirection(transform.eulerAngles);
        }
	}
}

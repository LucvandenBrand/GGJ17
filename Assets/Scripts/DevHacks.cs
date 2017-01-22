using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class DevHacks : MonoBehaviour {




	void Start() {
        GameObject.DontDestroyOnLoad( this.gameObject );
    }
	

	void Update () {
        if(Input.GetKeyDown( KeyCode.Escape )) {    //  ESC
            for (int i = 0; i < SceneManager.sceneCount; i++) {
                SceneManager.UnloadScene( i );
            }
		    SceneManager.LoadScene( 0 );
		}


		if(Input.GetKeyDown( KeyCode.R )) {        //  -->
		    SceneManager.LoadScene( 1 );
		}


        if(Input.GetKeyDown( KeyCode.F1 )) {        //  -->
		    StartCoroutine( LoadGlowEffect() );
		}
	}

    IEnumerator LoadGlowEffect() {
//        BloomOptimized bloom = Camera.main.GetComponent<BloomOptimized>();
//        bloom.threshold = 2;

        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoy : MonoBehaviour {

    private enum GameState {inGame, start, options};
    private static GameState currentState;

	// Use this for initialization
	void Awake () {
        currentState = GameState.start;	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Debug.Log("right");
           /* transform.position += new Vector3(1, 0, 0);
            if (isValidPosition())
                GridUpdate();
            else
                transform.position += new Vector3(-1, 0, 0);*/
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            /*
            transform.position += new Vector3(-1, 0, 0);
            if (isValidPosition())
                GridUpdate();
            else
                transform.position += new Vector3(1, 0, 0);*/
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            /*
            transform.Rotate(0, 0, -90);
            if (isValidPosition())
                GridUpdate();
            else
                transform.Rotate(0, 0, 90);
                */
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow)) { 
            //||Time.time - fall & gt;= 1) {
        }
    }

    
}

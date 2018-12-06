using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    private GridOccupantTypePool pool;
    private GameObject launchee;

	// Use this for initialization
	void Start () {
		
	}

    void Load()
    {
        launchee = pool.GetRandomOccupant().graphic;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetButton("Fire"))
        {

        }
	}

    void Fire()
    {
        if (launchee)
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {


    [SerializeField]
    private GameObject spot;

    [SerializeField]
    private int live = 10;

    [SerializeField]
    private float period = 120f;

    float Timer = 0f;

	// Use this for initialization
	void Start () {
		
	}

    void FixedUpdate()
    {
        Timer += 0.3f;
        if (Timer >= period)
        {
            int act = (int)(Random.Range(1, 10));
            if (act > 4 && act < 8)
            {
                Spawn();
            }
            Timer = 0;
        }
    }

    public void Spawn()
    {
        GameObject o = (GameObject)Instantiate(spot);
        o.transform.position = transform.position;
        spotComtroller sp = o.GetComponent<spotComtroller>();
        sp.live = live;
        //get level

    }
}

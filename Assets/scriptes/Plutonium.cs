using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plutonium : MonoBehaviour {

    public int Energy = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        PlatformerCharacter2D Char = other.GetComponent<PlatformerCharacter2D>();
        if(Char != null)
        {
            Char.changeEnergy(Energy);
        }
          Destroy(this.gameObject);
     }
}

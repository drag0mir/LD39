using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullit : MonoBehaviour {

    public Vector3 Direction;
    public float speed=0;
    public float LiveTime = 60f;
    public int Damage = 10;
    Transform tr;

	// Use this for initialization
	void Start () {
        tr = transform;
	}
	
	// Update is called once per frame
	void Update () {
        if(speed>0)
            tr.Translate(Direction.x* speed * Time.deltaTime, 0, tr.transform.position.z);
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("destroy");
        Destroy(this.gameObject);
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("destroy 2");
        spotComtroller spot = other.GetComponent<spotComtroller>();
        if(spot != null)
        {
            spot.OnDamage(Damage);
        }
        Destroy(this.gameObject);

    }

    void FixedUpdate()
    {
        LiveTime -= 0.3f;
        if (LiveTime <=0)
        {
            Destroy(this.gameObject);
        }
    }
}

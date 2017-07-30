using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spotComtroller : MonoBehaviour {


    public float RunSpeed = 24.0f;
    public float JumpForce = 20.0f;
    public enum State {
        IDLE,
        RUN,
        JUMP
    };
    float Timer = 0f;
    public float NextAction = 60f;

    float TimerDeath = 600f;
    public GameObject Plutonium;
    public  Animator anim;
    public  int live = 10;

        Rigidbody2D pb;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
          pb = gameObject.GetComponent<Rigidbody2D>();
          setAnimatorState(State.IDLE);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        TimerDeath -= 0.3f;
        if(TimerDeath <=0)
        {
            GameObject pl = (GameObject)Instantiate(Plutonium);
            pl.transform.position = transform.position - new Vector3(0, 2.0f, 0);
            Destroy(this.gameObject);
        }
        Timer +=0.3f;
        if(Timer >= NextAction){
            int act = (int)(Random.Range(1,10));
             if(act > 8 && act <11){
                setAnimatorState(State.JUMP);
            }
            if(act >=0 && act < 4){
                Move(1);
            }
            if(act >4 && act < 8){
                Move(-1);
            }
            Timer =0;
        }
    }
    public void Move(int direction){
        if(direction == -1){
          pb.AddForce(Vector3.left * RunSpeed);
        }else{
            pb.AddForce(Vector3.right * RunSpeed);
        }
        setAnimatorState(State.IDLE);
    }
    public void setAnimatorState(State state)
    {
        if (anim == null)
            return;

         switch (state)
        {
            case State.IDLE:
                {
                    anim.SetInteger("goState",0);
                    break;
                }
            case State.RUN:
                {
                    
                    anim.SetInteger("goState", 7);
                    break;
                }
            case State.JUMP:
                {
                    pb.AddForce(Vector3.up * JumpForce);
                    anim.SetInteger("goState", 1);
                    break;
                }

        }
    }
    public void OnDamage(int damage)
    {
        live -= damage;
        if (live <= 0)
        {
            GameObject pl = (GameObject)Instantiate(Plutonium);
            pl.transform.position = transform.position - new Vector3(0, 2.0f, 0);
            Destroy(this.gameObject);
        }
    }

}



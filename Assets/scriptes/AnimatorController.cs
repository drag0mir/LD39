using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour {

    public enum State {
        IDLE,
        WALK,
        RUN,
        SLIDE,
        JUMP,
        DEAD,
        CLIMB,
        SHOOT,
        CROUCH,
        THROW
    };

   public  Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
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
            case State.WALK:
                {
                    anim.SetInteger("goState", 10);
                    break;
                }
            case State.RUN:
                {
                    anim.SetInteger("goState", 7);
                    break;
                }
            case State.SLIDE:
                {
                    anim.SetInteger("goState", 8);
                    break;
                }
            case State.JUMP:
                {
                    anim.SetInteger("goState", 1);
                    break;
                }
            case State.DEAD:
                {
                    anim.SetInteger("goState", -1);
                    break;
                }
            case State.CLIMB:
                {
                    anim.SetInteger("goState", 2);
                    break;
                }
            case State.SHOOT:
                {
                    anim.SetInteger("goState", 20);
                    break;
                }
            case State.CROUCH:
                {
                    anim.SetInteger("goState", 3);
                    break;
                }
            case State.THROW:
                {
                    anim.SetInteger("goState", 25);
                    break;
                }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public float RunSpeed = 24.0f;
    public float JumpForce = 20.0f;

    public AnimatorController animController;
    Rigidbody2D pb;
    bool isJump = false;
	// Use this for initialization
	void Start () {
      /*  animController = gameObject.GetComponent<AnimatorController>();
        animController.setAnimatorState(AnimatorController.State.IDLE);*/
        pb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");



        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, 0);
        if (!isJump && Input.GetKey(KeyCode.Space))
        {
            //animController.setAnimatorState(AnimatorController.State.JUMP);
            pb.AddForce(Vector3.up * JumpForce);
            isJump = true;
        }
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        pb.AddForce(movement * RunSpeed);
	}
  
}

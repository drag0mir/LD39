using System;
using UnityEngine;


[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    public PlatformerCharacter2D m_Character;
    public int m_Jump=0;
    public bool m_Fire;

    public bool m_UnloadPower;

    bool isTutorialMove = false;
    bool isTutorialJump = false;
    bool isTutorialFire = false;
    bool isTutorialSwitchWeapon = false;
    

    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
    }


    private void Update()
    {
        if (m_Jump==0)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = Input.GetKeyDown(KeyCode.UpArrow) ? 1:0;
            if (m_Jump ==0 )
                m_Jump = Input.GetKeyDown(KeyCode.DownArrow) ? -1 : 0;
            if (m_Jump == 0) 
                m_Jump = Input.GetKeyDown(KeyCode.W) ? 1 : 0;
            if (m_Jump == 0)
                m_Jump = Input.GetKeyDown(KeyCode.S) ? -1 : 0;
          //  Debug.Log(m_Jump +" "+ (m_Jump > 0 ? "UP" : "DOWN"));
            if (!isTutorialJump && m_Jump != 0)
            {
                m_Character.SetHelpText("Press Mouse Left Button Or Space for Fire", true);
                isTutorialJump = true;
            }
        }
        m_UnloadPower = Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.E);

        m_Fire = Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump");



        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_Character.ChangeWeapon(0);
            if (!isTutorialSwitchWeapon)
            {
                m_Character.SetHelpText("Find Plutonium (cylinder or droped from Spot)", true);
                isTutorialSwitchWeapon = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_Character.ChangeWeapon(1);
            if (!isTutorialSwitchWeapon)
            {
                m_Character.SetHelpText("Find Plutonium (cylinder or droped from Spot)", true);
                isTutorialSwitchWeapon = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_Character.ChangeWeapon(2);
            if (!isTutorialSwitchWeapon)
            {
                m_Character.SetHelpText("Find Plutonium (cylinder or droped from Spot)", true);
                isTutorialSwitchWeapon = true;
            }
        }
    }


    private void FixedUpdate()
    {
        // Read the inputs.
        bool crouch = Input.GetKey(KeyCode.LeftControl);
        float h = Input.GetAxis("Horizontal");
        if (!isTutorialMove && h != 0)
        {
            m_Character.SetHelpText("Press W or Up for Jump", true);
            isTutorialMove = true;
        }
            
        // Pass all parameters to the character control script.
        m_Character.Move(h, crouch, m_Jump);
        if (m_Fire)
        {
            m_Character.Fire();
            if (!isTutorialFire )
            {
                m_Character.SetHelpText("Press 1,2 or 3 for switch weapon", true);
                isTutorialFire = true;
            }
        }
        if (m_UnloadPower)
            m_Character.UnloadPower();
        m_UnloadPower = false;
        m_Fire = false;
        m_Jump = 0;
    }
}
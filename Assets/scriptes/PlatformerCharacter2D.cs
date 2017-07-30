using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField]
        private LayerMask m_WhatIsStairs;        
        [SerializeField]
        private GameObject prefBullit;

        [SerializeField]
        private int FireMode = 1;    // 1?2?3

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        private Transform m_StairsCheck;
        private Transform m_TechCheck;  
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        
        private Transform m_BullitPoint;
        private bool m_isStairs;

        [SerializeField]
        private Text uiName;
        [SerializeField]
        private Text uiDamage;
        [SerializeField]
        private Text uiEnergy;
        [SerializeField]
        private RawImage uiImage;
        [SerializeField]
        private Image uiEnergyPlayer;
        const int EnergyMax = 200;
        public int Energy = EnergyMax;
        int currentWeapon = 0;

        [SerializeField]
        private Helper helper;
        [SerializeField]
        private GameObject plutonium;

        [SerializeField]
        private LayerMask m_WhatIsTechPoint;
        private bool m_isUnload;
        int CurrentTurel =0;

        EnergyManager em;

        bool isTutorialPlutonium = false;
        bool isTutorialTurel = false;
        bool isTutorialTurelLoad = false;

        class Weapon
        {
            public string Name;
            public float Speed;
            public int Damage;
            public float countDown;
            public int Energy;
            public Weapon(string number, float Speed, int Damage, float countDown, int Energy) 
            {
                this.Name = number;
                this.Speed = Speed;
                this.Damage = Damage;
                this.countDown = countDown;
                this.Energy = Energy;
            }

        }
        Weapon[] weapons = { new Weapon("Simple",140,10,10f,5), 
                               new Weapon("Triple",120,15,15f,20), 
                               new Weapon("Heavy",80,30,20f,15) };
        Weapon CurrentWeapon;
        private void Start()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_BullitPoint = transform.Find("BullitPoint");
            m_StairsCheck = transform.Find("StairsCheck");
            m_TechCheck = transform.Find("TechCheck");
           // m_Anim = GetComponent<Animator>();
            em = GetComponent<EnergyManager>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            ChangeWeapon(0);
        }


        private void FixedUpdate()
        {
            m_Grounded = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }

            colliders = Physics2D.OverlapCircleAll(m_StairsCheck.position, k_GroundedRadius, m_WhatIsStairs);
            for (int i = 0; i < colliders.Length; i++)
            {
                  if (colliders[i].gameObject != gameObject)
                    m_isStairs = true;
            }

            colliders = Physics2D.OverlapCircleAll(m_TechCheck.position, k_GroundedRadius, m_WhatIsTechPoint);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    CurrentTurel =  int.Parse(colliders[i].gameObject.name.Substring(5, 1));
                    m_isUnload = true;
                    if (!isTutorialTurel && !isTutorialTurelLoad)
                    {
                        SetHelpText("Press E for moving power to turret ", true);
                        isTutorialTurel = true;
                    }
                }                    
            }
            if (colliders.Length == 0)
                m_isUnload = false;
            
        }


        public void Move(float move, bool crouch, int jump)
        {
            if (!crouch /*&& m_Anim.GetBool("Crouch")*/)
            {
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }
            if (m_Grounded || m_AirControl)
            {
                move = (crouch ? move*m_CrouchSpeed : move);
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);
                if (move > 0 && !m_FacingRight)
                {
                    Flip();
                }
                else if (move < 0 && m_FacingRight)
                {
                    Flip();
                }
            }
            if (m_Grounded && (jump!=0 )&& !m_isStairs)
            {
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
            if ((jump!=0) && m_isStairs)
            {
                m_isStairs = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce/2 * jump));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        public void Fire()
        {
            if (Energy >= CurrentWeapon.Energy)
            {
                //TODO: FireMode
                GameObject bullit = (GameObject)Instantiate(prefBullit);
                bullit.transform.position = m_BullitPoint.position;

                Bullit b = (Bullit)bullit.GetComponent<Bullit>();
                b.Direction = m_FacingRight ? Vector3.right : Vector3.left;
                bullit.transform.TransformPoint(b.Direction);
                b.Damage = CurrentWeapon.Damage;
                b.speed = CurrentWeapon.Speed;
                changeEnergy(-CurrentWeapon.Energy);
            }
            //bullit.transform.Translate(bullit.transform.position + Vector3.right * 1000);
        }


        public void ChangeWeapon(int key)
        {
            CurrentWeapon = weapons[key];
            setUiWeapon(key);
        }

        private void setUiWeapon(int key)
        {
            uiName.text = CurrentWeapon.Name;
            uiDamage.text = "Dmg: " + CurrentWeapon.Damage;
            uiEnergy.text = "Eng: " + CurrentWeapon.Energy;


            uiImage.texture = Resources.Load<Texture2D>("bullit0" + (key+1)) ;
        }



        public void changeEnergy(int delta)
        {
            if (Energy <= EnergyMax)
            {
                Debug.Log(Energy);
                Energy += delta;
                if(Energy >= EnergyMax)
                {
                    Energy = EnergyMax;
                    Debug.Log("CreatePlutonium");
                    GameObject pl = (GameObject)Instantiate(plutonium);
                    pl.transform.position = transform.position - new Vector3(10.0f, 2.0f, 0);
                    Debug.LogError("1");
                    pl.name = "Dropped";
                }
                float r = ((float)Energy) / ((float)EnergyMax);
                uiEnergyPlayer.fillAmount = r;
            }

            if(!isTutorialPlutonium && delta >0 )
            {
                SetHelpText("Find the turret (on the left)", true);
                isTutorialPlutonium = true;
            }
        }

         public void UnloadPower()
        {
            if (!m_isUnload)
                return;
            if (Energy > 0)
            {
                if (CurrentTurel == 1) em.change1(20);
                if (CurrentTurel == 2) em.change2(20);
                if (CurrentTurel == 3) em.change3(20);
                if (CurrentTurel == 4) em.change4(20);
                changeEnergy(-20);
                if(!isTutorialTurelLoad)
                {
                    SetHelpText("Ok.Replenish "/*the energy reserves in the turrets!"*/, false);
                    em.Level = 1;
                    isTutorialTurelLoad = true;
                }
            }

             
        }

        public void SetHelpText(string text, bool visible)
         {
             ExecuteEvents.Execute<UIActions>(helper.gameObject, null, (x, y) => x.UIAction(new UIMessageAction() {  Visible = visible, paramString = text}));
         }

        public void GameOver(int i)
        {
            ExecuteEvents.Execute<UIEnds>(helper.gameObject, null, (x, y) => x.UIEnd(new UIMessageAction() { result = i }));
        }
    }


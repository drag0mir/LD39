using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour {


    const int EnergyMax = 400;
    public int Energy1 = EnergyMax;
    public int Energy2 = EnergyMax;
    public int Energy3 = EnergyMax;
    public int Energy4 = EnergyMax;



    [SerializeField]
    private Image uiEnergy1;
    [SerializeField]
    private Image uiEnergy2;
    [SerializeField]
    private Image uiEnergy3;
    [SerializeField]
    private Image uiEnergy4;

    float Timer = 0f;
    public float NextAction = 120f;

    public int Level = 0;

    PlatformerCharacter2D Char;

    void Start()
    {
        Char = gameObject.GetComponent<PlatformerCharacter2D>();
         change1(0); 
         change2(0); 
         change3(0); 
         change4(0); 
    }
    void FixedUpdate()
    {

        Timer += 0.3f;
        if (Timer >= NextAction)
        {
            ChangePower();
            Timer = 0;
        }
        if (Energy1 == 0 &&
                Energy2 == 0 &&
                Energy3 == 0 &&
                Energy4 == 0 )
            Char.GameOver(0);
        if (Level == 5)
            Char.GameOver(1);
    }

    void ChangePower()
    {
        if (Level >= 1) {change1(-1*(int)Random.Range(1,20)); }
        if (Level >= 2) {change2(-1*(int)Random.Range(1,20)); }
        if (Level >= 3) {change3(-1*(int)Random.Range(1,20)); }
        if (Level >= 4) { change4(-1 * (int)Random.Range(1, 20)); }
    }

    public void change1(int delta)
    {
        if (Energy1 <= EnergyMax)
        {
            Energy1 += delta;
            if (Energy1 <= 0)
                Energy1 = 0;
            uiEnergy1.fillAmount = ((float)Energy1) / ((float)EnergyMax);
        }
        else
        {
            Level = 2;
        }
    }
    public void change2(int delta)
    {
        if (Energy2 <= EnergyMax)
        {
            Energy2 += delta;
            if (Energy2 <= 0)
                Energy2 = 0;
            uiEnergy2.fillAmount = ((float)Energy2) / ((float)EnergyMax);
        }
        else
        {
            Level = 3;
        }
    }
    public void change3(int delta)
    {
        if (Energy3 <= EnergyMax)
        {
            Energy3 += delta;
            if (Energy3 <= 0)
                Energy3 = 0;
            uiEnergy3.fillAmount = ((float)Energy3) / ((float)EnergyMax);
        }
        else
        {
            Level = 4;
        }
    }
    public void change4(int delta)
    {
        if (Energy4 <= EnergyMax)
        {
            Energy4 += delta;
            if (Energy4 <= 0)
                Energy4 = 0;
            uiEnergy4.fillAmount = ((float)Energy4) / ((float)EnergyMax);
        }
        else
        {
            Level = 5;// FInish
        }
    }



}

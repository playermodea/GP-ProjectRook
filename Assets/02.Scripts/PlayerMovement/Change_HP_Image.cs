using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_HP_Image : MonoBehaviour
{

    public Sprite change_hp_plus;//꽉찬 HP
    public Sprite change_hp_minus;//빈 HP
    Image thisImg;
    public int HP = 3;

    public Image HP1;
    public Image HP2;
    public Image HP3;
    public Image HP4;
    public Image HP5;
    public Image HP6;
    public Image HP7;
    public Image HP8;
    public Image HP9;
    public Image HP10;
    // Start is called before the first frame update
    void Start()
    {
        thisImg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //HP 늘어났을때 이미지 바꾸기
        if(HP>=1)
        {
            HP1.GetComponent<Image>().sprite = change_hp_plus;
        }
        if (HP >= 2)
        {
            HP2.GetComponent<Image>().sprite = change_hp_plus;
        }
        if (HP >= 3)
        {
            HP3.GetComponent<Image>().sprite = change_hp_plus;
        }
        if (HP >= 4)
        {
            HP4.GetComponent<Image>().sprite = change_hp_plus;
        }
        if (HP >= 5)
        {
            HP5.GetComponent<Image>().sprite = change_hp_plus;
        }
        if (HP >= 6)
        {
            HP6.GetComponent<Image>().sprite = change_hp_plus;
        }
        if (HP >= 7)
        {
            HP7.GetComponent<Image>().sprite = change_hp_plus;
        }
        if (HP >= 8)
        {
            HP8.GetComponent<Image>().sprite = change_hp_plus;
        }
        if (HP >= 9)
        {
            HP9.GetComponent<Image>().sprite = change_hp_plus;
        }
        if (HP >= 10)
        {
            HP10.GetComponent<Image>().sprite = change_hp_plus;
        }

        //HP 깎였을때 이미지 바꾸기
        if(HP < 1)
        {
            HP1.GetComponent<Image>().sprite = change_hp_minus;
        }
        if (HP < 2)
        {
            HP2.GetComponent<Image>().sprite = change_hp_minus;
        }
        if (HP < 3)
        {
            HP3.GetComponent<Image>().sprite = change_hp_minus;
        }
        if (HP < 4)
        {
            HP4.GetComponent<Image>().sprite = change_hp_minus;
        }
        if (HP < 5)
        {
            HP5.GetComponent<Image>().sprite = change_hp_minus;
        }
        if (HP < 6)
        {
            HP6.GetComponent<Image>().sprite = change_hp_minus;
        }
        if (HP < 7)
        {
            HP7.GetComponent<Image>().sprite = change_hp_minus;
        }
        if (HP < 8)
        {
            HP8.GetComponent<Image>().sprite = change_hp_minus;
        }
        if (HP < 9)
        {
            HP9.GetComponent<Image>().sprite = change_hp_minus;
        }
        if (HP < 10)
        {
            HP10.GetComponent<Image>().sprite = change_hp_minus;
        }
    }
    void ChangeHPPLUS()
    {
        thisImg.sprite = change_hp_plus;
    }
    void ChangeHPMINUS()
    {
        thisImg.sprite = change_hp_minus;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textsc : MonoBehaviour
{
    private const string brandish = "휘두르기";
    private const string stamping_feet = "발구르기";
    private const string stabbing = "찌르기";
    private const string sound_wave = "음파";
    private const string float_throw = "찌 투척";
    private const string crack_the_sky = "Crack The Sky";
    private const string shout = "함성";
    private const string bomb_throw = "폭탄 투척";
    private const string horizontal_cutting = "가로 베기";
    private const string spin_cutting = "회전 베기";
    //private Text V = "asfaf";
    public Text Skill_1;
    public Text Skill_2;
    public Text Skill_3;
    public Text Skill_4;
    public Text skilldamage_1;
    public Text skilldamage_2;
    public Text skilldamage_3;
    public Text skilldamage_4;
    public Text Dmg;

    private int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //휘두르기
        if (Skill_1.text == brandish.ToString())
        {
            skilldamage_1.text = 8 + damage * 0.5 + " DMG";
        }

        //발구르기
        if (Skill_1.text == stamping_feet.ToString())
        {
            skilldamage_1.text = 10 + damage + " DMG";
        }

        //찌르기
        if (Skill_1.text == stabbing.ToString())
        {
            skilldamage_1.text = 10 + damage * 1.8 + " DMG";
        }

        //음파
        if (Skill_1.text == sound_wave.ToString())
        {
            skilldamage_1.text = 15 + damage * 1.5 + " DMG";
        }

        //찌투척
        if (Skill_1.text == float_throw.ToString())
        {
            skilldamage_1.text = 20 + damage * 2 + " DMG";
        }

        //Crack The Sky
        if (Skill_1.text == crack_the_sky.ToString())
        {
            skilldamage_1.text = 20 + damage * 1.7 + " DMG";
        }

        //함성
        if (Skill_1.text == shout.ToString())
        {
            skilldamage_1.text = 5 + damage * 2 + " DMG";
        }

        //폭탄투척
        if (Skill_1.text == bomb_throw.ToString())
        {
            skilldamage_1.text = 15 + damage * 1.7 + " DMG";
        }

        //가로베기
        if (Skill_1.text == horizontal_cutting.ToString())
        {
            skilldamage_1.text = 17 + damage * 1.6 + " DMG";
        }

        //회전베기
        if (Skill_1.text == spin_cutting.ToString())
        {
            skilldamage_1.text = 10 + damage * 2.5 + " DMG";
        }

        Dmg.text = damage.ToString(); // DMG UI에 데미지 출력(플레이어 데미지로 바꿔줄것.)
    }

}

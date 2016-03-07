using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player_properties : MonoBehaviour {
    public float pull_cd;
    public float HP, ATKK, DEF, SPD, TSPD;
    public float remaining_HP;
    public float BT_M, LS_M, RT_M;
    public float BT_CD, LS_CD, SR_CD, DR_CD, CMS_CD;
    public float RADAR_R;

    public int BULLET, LASER, SROCKET, DROCKET, COUNTERMSRS,
        ACTIVE_RADAR, ACTIVE_DEFENSE, AI_SYSTEM, COUNTER_DR,
        COUNTER_SR, COUNTER_BT, SR_CNT, DR_CNT, EXPLOSIVE;

    public int CREW, STR_PT, DEF_PT, EDR_PT, EXP_PT, EXP;

    private float defATKK;

    void Start () {
        GetDataFromPrefs();
        pull_cd = 10f;
    }

    void LateUpdate()
    {
        if (remaining_HP <= 0f)
        {
            Destroy(gameObject);
        }
        if (pull_cd <= 0f)
        {
            GetDataFromPrefs();
            pull_cd = 10f;
        }
        else
        {
            pull_cd -= Time.deltaTime;
        }
    }

    void GetDataFromPrefs()
    {
        HP = PlayerPrefs.GetFloat("HP");
        ATKK = PlayerPrefs.GetFloat("ATK");
        Debug.Log("GDFP " + ATKK);
        EXP = PlayerPrefs.GetInt("EXP");
        DEF = PlayerPrefs.GetFloat("DEF");
        SPD = PlayerPrefs.GetFloat("SPD");
        TSPD = PlayerPrefs.GetFloat("TSPD");

        BULLET = PlayerPrefs.GetInt("BULLET");
        LASER = PlayerPrefs.GetInt("LASER");
        EXPLOSIVE = PlayerPrefs.GetInt("EXPLOSIVE");
        SROCKET = PlayerPrefs.GetInt("SROCKET");
        DROCKET = PlayerPrefs.GetInt("DROCKET");
        COUNTERMSRS = PlayerPrefs.GetInt("COUNTERMSRS");

        COUNTER_SR = PlayerPrefs.GetInt("COUNTER_SR");
        COUNTER_DR = PlayerPrefs.GetInt("COUNTER_DR");
        COUNTER_BT = PlayerPrefs.GetInt("COUNTER_BT");

        BT_M = PlayerPrefs.GetFloat("BT_M");
        LS_M = PlayerPrefs.GetFloat("LS_M");
        RT_M = PlayerPrefs.GetFloat("RT_M");

        SR_CNT = PlayerPrefs.GetInt("SR_CNT");
        DR_CNT = PlayerPrefs.GetInt("DR_CNT");

        BT_CD = PlayerPrefs.GetFloat("BT_CD");
        LS_CD = PlayerPrefs.GetFloat("LS_CD");
        SR_CD = PlayerPrefs.GetFloat("SR_CD");
        DR_CD = PlayerPrefs.GetFloat("DR_CD");
        CMS_CD = PlayerPrefs.GetFloat("CMS_CD");

        ACTIVE_RADAR = PlayerPrefs.GetInt("ACTIVE_RADAR");
        ACTIVE_DEFENSE = PlayerPrefs.GetInt("ACTIVE_DEFENSE");
        AI_SYSTEM = PlayerPrefs.GetInt("AI_SYSTEM");

        CREW = PlayerPrefs.GetInt("CREW");

        RADAR_R = PlayerPrefs.GetFloat("RADAR_R");

        STR_PT = PlayerPrefs.GetInt("STR_PT");
        DEF_PT = PlayerPrefs.GetInt("DEF_PT");
        EDR_PT = PlayerPrefs.GetInt("EDR_PT");
        EXP_PT = PlayerPrefs.GetInt("EXP_PT");
    }

    void FixedUpdate()
    {
        Debug.Log("PLAY_PROP ATKK " + ATKK);
    }
}

using UnityEngine;
using System.Collections;

public class stats_manager : MonoBehaviour {

    private float pull_time;
    private float base_spd;
    private float base_atk;
    private float base_def;
	// Use this for initialization
	void Start () {
        pull_time = 120f;
        base_spd = 10f;
        base_def = 10f;
        base_atk = 10f;
        LoadDefaultValues(); // for demo only
        if (!PlayerPrefs.HasKey("Game Exists"))
        {
            LoadDefaultValues();
            PlayerPrefs.SetInt("Game Exists", 1);
        }

        PullDataFromWeb();
	}
	
	// Update is called once per frame
	void Update () {
        if (pull_time <= 0f)
        {
            PullDataFromWeb();
            pull_time = 120f;
        }
        else
        {
            pull_time -= Time.deltaTime;
        }
	}

    void PullDataFromWeb()
    {
        StartCoroutine(getDataFromServer());
    }

    private IEnumerator getDataFromServer()
    {
        string url = "http://testdudeyyu.azurewebsites.net";
        WWW req = new WWW(url);

        yield return req;
        string[] splitted = req.text.Split('|');

        int[] parsedVals = new int[] { int.Parse(splitted[0]), int.Parse(splitted[1]) };
        PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") + parsedVals[0] % 100);
        PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") + parsedVals[1] % 1000);
    }

    void LoadDefaultValues()
    {
        PlayerPrefs.SetFloat("HP", 100f);
        PlayerPrefs.SetFloat("ATK", 5f);
        PlayerPrefs.SetFloat("DEF", 0.95f);
        PlayerPrefs.SetFloat("SPD", 10f);
        PlayerPrefs.SetFloat("TSPD", 10f);

        PlayerPrefs.SetInt("BULLET", 1);
        PlayerPrefs.SetInt("LASER", 0);
        PlayerPrefs.SetInt("EXPLOSIVE", 0);
        PlayerPrefs.SetInt("SROCKET", 0);
        PlayerPrefs.SetInt("DROCKET", 0);
        PlayerPrefs.SetInt("COUNTERMSRS", 0);

        PlayerPrefs.SetInt("COUNTER_SR", 0);
        PlayerPrefs.SetInt("COUNTER_DR", 0);
        PlayerPrefs.SetInt("COUNTER_BT", 0);

        PlayerPrefs.SetFloat("BT_M", 1f); // bullet dmg multiplier
        PlayerPrefs.SetFloat("LS_M", 0.5f); // laser dmg multiplier
        PlayerPrefs.SetFloat("RT_M", 1.25f); // rocket dmg multiplier

        PlayerPrefs.SetInt("SR_CNT", 2); // number of smart rockets before reload
        PlayerPrefs.SetInt("DR_CNT", 2); // number of dumb rockets before reload

        PlayerPrefs.SetFloat("BT_CD", 0.25f); // cooldown for each bullet
        PlayerPrefs.SetFloat("LS_CD", 0f); // cool down for each laser fire
        PlayerPrefs.SetFloat("SR_CD", 5f); // cool down for replenishing smart rockets
        PlayerPrefs.SetFloat("DR_CD", 5f); // cool down for replenishing dumb rockets
        PlayerPrefs.SetFloat("CMS_CD", 5f);

        PlayerPrefs.SetInt("ACTIVE_RADAR", 0); // has active radar
        PlayerPrefs.SetInt("ACTIVE_DEFENSE", 0); // has active defense
        PlayerPrefs.SetInt("AI_SYSTEM", 0); // has AI system 
        PlayerPrefs.SetInt("CREW", 1);

        PlayerPrefs.SetFloat("RADAR_R", 40f);

        PlayerPrefs.SetInt("EXP", 0);

        // points tracker

        PlayerPrefs.SetInt("STR_PT", 0); // used to upgrade dmg, dmg modifiers, and number of rockets
        PlayerPrefs.SetInt("DEF_PT", 0); // used to increase DEF, countermeasure range, and decrease CDs
        PlayerPrefs.SetInt("EDR_PT", 0); // used to increase hp and ship speed
        PlayerPrefs.SetInt("EXP_PT", 0); // used to increase crew members, and purchase systems
    }
}

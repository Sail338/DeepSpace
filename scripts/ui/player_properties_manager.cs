using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class player_properties_manager : MonoBehaviour {
    private player_properties prop;
    public Button add_hp, add_dmg, add_spd, add_crew;
    public Button explosive, radar, active_def, ai;
    public Button dumb, cms, smart, add_smart, add_dumb;
    public Text warning;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        add_hp.onClick.AddListener(delegate { IncreaseHP(); });
        add_dmg.onClick.AddListener(delegate { IncreaseDMG(); });
        add_spd.onClick.AddListener(delegate { IncreaseSPD(); });
        add_crew.onClick.AddListener(delegate { AddCrew(); });

        explosive.onClick.AddListener(delegate { getExplosive(); });
        radar.onClick.AddListener(delegate { getRadar(); });
        active_def.onClick.AddListener(delegate { getActiveDef(); });
        ai.onClick.AddListener(delegate { getAI(); });

        dumb.onClick.AddListener(delegate { AddDumbRocket(); });
        cms.onClick.AddListener(delegate { AddCMS(); });
        smart.onClick.AddListener(delegate { AddSmartRocket(); });
        add_smart.onClick.AddListener(delegate { AddSmartRocketCnt(); });
        add_dumb.onClick.AddListener(delegate { AddDumbRocketCnt(); });
	}

    void AddSmartRocket()
    {
        if (PlayerPrefs.GetInt("EXPLOSIVE") != 1 || PlayerPrefs.GetInt("ACTIVE_RADAR") != 1 || PlayerPrefs.GetInt("ACTIVE_DEFENSE") != 1 || PlayerPrefs.GetInt("EXP") < 10)
        {
            warning.text = "Must have purchased explosive tech, active radar, active defense, and 10exp";
        }
        else
        {
            PlayerPrefs.SetInt("SROCKET", 1);
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") - 10);
        }
    }

    void AddSmartRocketCnt()
    {
        if (PlayerPrefs.GetInt("SROCKET") != 1 || PlayerPrefs.GetInt("EXP") < 5)
        {
            warning.text = "Must have acquired smart rockets and 5exp";
        }
        else
        {
            PlayerPrefs.SetInt("SR_CNT", PlayerPrefs.GetInt("SR_CNT") + 1);
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") - 5);
        }
    }

    void AddDumbRocketCnt()
    {
        if (PlayerPrefs.GetInt("DROCKET") != 1 || PlayerPrefs.GetInt("EXP") < 5)
        {
            warning.text = "Must have acquired dumb rockets and 5exp";
        }
        else
        {
            PlayerPrefs.SetInt("DR_CNT", PlayerPrefs.GetInt("DR_CNT") + 1);
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") - 5);
        }
    }

    void AddDumbRocket()
    {
        if (PlayerPrefs.GetInt("EXPLOSIVE") != 1 || PlayerPrefs.GetInt("EXP") < 10)
        {
            warning.text = "Must have purchased explosive tech first and 10exp";
        }
        else
        {
            PlayerPrefs.SetInt("DROCKET", 1);
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") - 10);
        }
    }

    void AddCMS()
    {
        if (PlayerPrefs.GetInt("ACTIVE_RADAR") != 1 || PlayerPrefs.GetInt("ACTIVE_DEFENSE") != 1 || PlayerPrefs.GetInt("EXP") < 10)
        {
            warning.text = "Must have purchased active radar and active defense and 10exp";
        }
        else
        {
            PlayerPrefs.SetInt("COUNTERMSRS", 1);
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") - 10);
        }
    }
    void IncreaseHP()
    {
        if (PlayerPrefs.GetInt("EXP") < 1)
        {
            warning.text = "not enough exp";
        }
        else
        {
            PlayerPrefs.SetFloat("HP", PlayerPrefs.GetFloat("HP") + 10f);
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") - 1);
        }        
    }

    void IncreaseDMG()
    {
        if (PlayerPrefs.GetInt("EXP") < 1)
        {
            warning.text = "not enough exp";
        }
        else {
            PlayerPrefs.SetFloat("ATK", PlayerPrefs.GetFloat("ATK") + 2f);
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") - 1);
        }
    }

    void IncreaseSPD()
    {
        if (PlayerPrefs.GetInt("EXP") < 1)
        {
            warning.text = "not enough exp";
        }
        else {
            PlayerPrefs.SetFloat("SPD", PlayerPrefs.GetFloat("SPD") + 2f);
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") - 1);
        }
    }

    void AddCrew()
    {
        if (PlayerPrefs.GetInt("EXP") < 3)
        {
            warning.text = "not enough exp";
        }
        else
        {
            float SRCD = PlayerPrefs.GetFloat("SR_CD");
            float DRCD = PlayerPrefs.GetFloat("DR_CD");
            float CMSCD = PlayerPrefs.GetFloat("CMS_CD");
            if (SRCD > 1.5f)
            {
                SRCD -= 0.2f;
            }
            if (DRCD > 1.5f)
            {
                DRCD -= 0.2f;
            }
            if (CMSCD > 1.5f)
            {
                CMSCD -= 0.2f;
            }

            PlayerPrefs.SetFloat("SR_CD", SRCD);
            PlayerPrefs.SetFloat("DR_CD", DRCD);
            PlayerPrefs.SetFloat("CMS_CD", CMSCD);
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") - 3);
        }
    }

    void getExplosive()
    {
        if (PlayerPrefs.GetInt("EXP") < 5)
        {
            warning.text = "not enough exp";
        }
        else {
            PlayerPrefs.SetInt("EXPLOSIVE", 1);
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") - 5);
        }
    }

    void getRadar()
    {
        if (PlayerPrefs.GetInt("EXP") < 10)
        {
            warning.text = "not enough exp";
        }
        else {
            PlayerPrefs.SetInt("ACTIVE_RADAR", 1);
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") - 10);
        }
    }

    void getActiveDef()
    {
        if (PlayerPrefs.GetInt("EXP") < 10)
        {
            warning.text = "not enough exp";
        }
        else {
            PlayerPrefs.SetInt("ACTIVE_DEFENSE", 1);
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") - 10);
        }
    }

    void getAI()
    {
        if (PlayerPrefs.GetInt("EXP") < 10)
        {
            warning.text = "not enough exp";
        }
        else
        {
            PlayerPrefs.SetInt("AI_SYSTEM", 1);
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") - 10);
        }
    }
    

    public void AddPlayerProperties(player_properties props)
    {
        this.prop = props;
    }
}

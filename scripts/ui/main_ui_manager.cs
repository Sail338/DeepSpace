using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class main_ui_manager : MonoBehaviour {
    public Color color1, color2;
    public Text bullet, laser, df, sf, cms;

    public Text hp, score, time_alive;

    public Text spd, atk, def;

    private GameObject player;

    public Canvas shop_canvas;
    private bool shop_ui_engaged;
    private Canvas shop_ui;

    private player_properties props;

    private float cmstimer;

    private int sc;

    private float sec;
	// Use this for initialization
	void Start () {

        
        sec = 0f;

        sc = 0;
        if (player != null)
        {
            props = player.GetComponent<player_properties>();
            hp.text = "HP: " + props.remaining_HP.ToString();
        }
        time_alive.text = "00:00:00:000";

        color1 = new Color(1f, 1f, 1f);
        color2 = new Color(.2f, .2f, .2f);

        if (PlayerPrefs.GetInt("COUNTERMSRS") == 1)
        {
            cms.text = "COUNTERMEASURES: ACTIVE";
            score.text = "Score: 0";
            cms.color = color1;
        }

        bullet.color = color1;
        laser.color = color2;

        if (PlayerPrefs.GetInt("DROCKET") == 1)
        {
            df.color = color1;
            sf.color = color2;
        }
        else if (PlayerPrefs.GetInt("SROCKET") == 1)
        {
            df.color = color2;
            sf.color = color1;
        }
        else
        {
            df.color = color2;
            sf.color = color2;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (shop_ui_engaged)
            {
                Time.timeScale = 1.0f;
                Destroy(shop_ui);
                shop_ui_engaged = false;
            }
            else
            {
                shop_ui = (Canvas)Instantiate(shop_canvas, shop_canvas.transform.position, shop_canvas.transform.rotation);
                shop_ui.GetComponent<player_properties_manager>().AddPlayerProperties(props);
                Time.timeScale = 0f;
                shop_ui_engaged = true;
            }
        }
        
        atk.text = "ATK: " + props.ATKK;
        def.text = "DEF: " + props.DEF;
        spd.text = "EXP: " + props.EXP;
        if (cmstimer > 0f)
        {
            cmstimer -= Time.deltaTime;
            cms.text = "COUNTERMEASURES: INACTIVE";
            cms.color = color2;
        }
        else
        {
            if (PlayerPrefs.GetInt("COUNTERMSRS") == 1)
            {
                cms.text = "COUNTERMEASURES: ACTIVE";
                cms.color = color1;
            }
        }

        if (props != null)
        {
            sec += Time.deltaTime;

            int milliseconds = (int)(sec * 1000f);

            int seconds = milliseconds / 1000;
            milliseconds = milliseconds % 1000;
            int minutes = seconds / 60;
            seconds = seconds % 60;
            int hours = minutes / 60;
            minutes = minutes % 60;

            time_alive.text = hours.ToString().PadLeft(2, '0') + 
                ":" + minutes.ToString().PadLeft(2, '0') + ":" + 
                seconds.ToString().PadLeft(2, '0') + ":" + milliseconds.ToString().PadLeft(3, '0');
            if (props.remaining_HP < 0f)
            {
                hp.text = "HP: --";
            }
            else hp.text = "HP: " + props.remaining_HP.ToString();
        }
        else
        {
            if (player != null)
            {
                props = player.GetComponent<player_properties>();
            }
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            bullet.color = color1;
            laser.color = color2;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            // bullet.color = color2;
            // laser.color = color1;
        }

        if (Input.GetKey(KeyCode.Alpha3) && PlayerPrefs.GetInt("DROCKET") == 1)
        {
            df.color = color1;
            sf.color = color2;
        }
        else if (Input.GetKey(KeyCode.Alpha4) && PlayerPrefs.GetInt("SROCKET") == 1)
        {
            df.color = color2;
            sf.color = color1;
        }

        if (Input.GetKey(KeyCode.F) && cmstimer <= 0f)
        {
            cms.text = "COUNTERMEASURES: INACTIVE";
            cmstimer = 5f;
            cms.color = color2;
        }
	}

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    public void increaseScore()
    {
        sc++;
        score.text = "Score: " + sc;
    }
}

using UnityEngine;
using System.Collections;

public class player_weapons_controller : MonoBehaviour {
    public GameObject BT, SRT, DRT;

    private float ATK, DEF;
    private float SR_CT, DR_CT;
    private float COUNTERMSRS;

    private float[] cooldowns;

    private int SRT_CNT, DRT_CNT;

    private int primary, secondary;

    private Rigidbody rb;
    private player_properties props;

    private float[] intervals;
    private float up_lim;
	// Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        props = GetComponent<player_properties>();

        props.remaining_HP = props.HP;

        cooldowns = new float[] { 0f, 0f, 0f, 0f, 0f };
        primary = 1;
        if (PlayerPrefs.GetInt("DROCKET") == 1)
        {
            secondary = 3;
        }
        else if (PlayerPrefs.GetInt("SROCKET") == 1)
        {
            secondary = 4;
        }
        else
        {
            secondary = 0;
        }

        SRT_CNT = props.SR_CNT;
        DRT_CNT = props.DR_CNT;

        intervals = new float[] { 0f, 0f };
        up_lim = 0.25f;
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 2; i++)
        {
            if (intervals[i] > 0)
            {
                intervals[i] -= Time.deltaTime;
            }
        }
            if (cooldowns[0] > 0)
            {
                cooldowns[0] -= Time.deltaTime;
            }

        if (cooldowns[1] > 0)
        {
            cooldowns[1] -= Time.deltaTime;
        }

        if (cooldowns[2] > 0)
        {
            cooldowns[2] -= Time.deltaTime;
        }
        else
        {
            if (DRT_CNT < props.DR_CNT)
            {
                DRT_CNT++;
            }
            cooldowns[2] = props.DR_CD;
        }

        if (cooldowns[3] > 0)
        {
            cooldowns[3] -= Time.deltaTime;
        }
        else
        {
            if (SRT_CNT < props.SR_CNT)
            {
                SRT_CNT++;
            }
            cooldowns[3] = props.SR_CD;
        }

        if (cooldowns[4] > 0)
        {
            cooldowns[4] -= Time.deltaTime;
        }

		if (Input.GetMouseButton(0)) {
			if (primary == 1 && cooldowns[0] <= 0f) {
				GameObject bt = (GameObject)Instantiate(BT, transform.position + transform.forward, Quaternion.identity);
                projectile_DMG proj_dmg = bt.GetComponent<projectile_DMG>();
                Debug.Log("PWC " + props.ATKK);
                proj_dmg.SetProjectileInfo(props.ATKK, "BT");
                bt.GetComponent<Rigidbody>().AddForce(transform.forward * 200f);

                Destroy(bt, 1f);

				cooldowns[0] = props.BT_CD;
			} /* else if (primary == 2 && cooldowns[1] <= 0f) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				Physics.Raycast(ray, out hit);
				if (hit.collider.tag == "enemy") {
					hit.gameObject.GetComponent<em_weapons_controller>().SetDmg(props.ATK);
				}
			} */ // implemented at a later time
		}

		if (Input.GetMouseButton(1)) {
            if (secondary == 3 && intervals[0] <= 0f && DRT_CNT > 0 && PlayerPrefs.GetInt("DROCKET") == 1)
            {
                Vector3 pointerPos = getCursorPosition("", 0f);
                Vector3 startPos, endPos;
                endPos = pointerPos;
                startPos = transform.position + transform.forward;

                GameObject drocket = (GameObject)Instantiate(DRT, startPos, Quaternion.identity);
                rocket_script drs = drocket.GetComponent<rocket_script>();
                drs.SetTargetPosition(endPos);
                drs.SetRocketInfo(props.ATKK);
                drs.SetSpeed(10f);

                Destroy(drocket, 2f);

                intervals[0] = up_lim;

                DRT_CNT--;

            }
            else if (secondary == 4 && intervals[1] <= 0f && SRT_CNT > 0 && PlayerPrefs.GetInt("SROCKET") == 1)
            {
                GameObject target = this.gameObject;

                Vector3 startPos = transform.position + transform.forward;

                Collider collider = GetOPFORColliders(props.RADAR_R);

                if (collider != null) {
                    GameObject srocket = (GameObject)Instantiate(SRT, startPos, Quaternion.identity);
                    rocket_script srs = srocket.GetComponent<rocket_script>();
                    srs.SetRocketInfo(props.ATKK * 1.5f);

                    srs.SetTarget(collider.gameObject);
                    srs.SetSpeed(10f);

                    Destroy(srocket, 5f);

                    intervals[1] = up_lim;
                    
                    SRT_CNT--;
                }
            }
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
            primary = 1;
		} else if (Input.GetKeyDown(KeyCode.Alpha2) && props.LASER == 1) {

		}

		if (Input.GetKeyDown(KeyCode.Alpha3) && PlayerPrefs.GetInt("DROCKET") == 1) {
            secondary = 3;
		} else if (Input.GetKeyDown(KeyCode.Alpha4) && PlayerPrefs.GetInt("SROCKET") == 1) {
            secondary = 4;
		}

		if (Input.GetKeyDown(KeyCode.F) && cooldowns[4] <= 0f && PlayerPrefs.GetInt("COUNTERMSRS") == 1) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, props.RADAR_R);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "RT")
                {
                    colliders[i].gameObject.GetComponent<rocket_script>().Destroy();
                }
            }

            cooldowns[4] = props.CMS_CD;
		}
	}

    Vector3 getCursorPosition(string tag = "", float heightAdj = -1f)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 target = Vector3.zero;
        if (tag == "")
        {
            target = hit.point;
        }
        else
        {
            if (hit.collider.gameObject.tag == tag)
            {
                target = hit.point;
            }
        }
        if (heightAdj != -1f)
        {
            target.y = heightAdj;
        }
        return target;
    }

    public void SetDmg(float atk, string type)
    {
        float totalDmg = 0f;
        if (type == "BT")
        {
            totalDmg = atk * 0.3f;
        }
        else if (type == "LS")
        {
            totalDmg = atk * 0.8f;
        }
        else if (type == "RT")
        {
            totalDmg = atk * 1.5f;
        }

        props.remaining_HP -= (totalDmg);
    }

    Collider GetOPFORColliders(float r)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, r);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "enemy")
            {
                return colliders[i];
            }
        }

        return null;
    }
}

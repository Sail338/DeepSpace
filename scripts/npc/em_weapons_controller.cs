using UnityEngine;
using System.Collections;

public class em_weapons_controller : MonoBehaviour {
    
    public GameObject BT, DRT, SRT;

    private int drtCnt, srtCnt;

    private em_properties props;
    private float[] cds;
	// Use this for initialization
	void Start () {
        props = GetComponent<em_properties>();
        cds = new float[props.WEPCDS.Length];

        for (int i = 0; i < props.WEPCDS.Length; i++)
        {
            cds[i] = props.WEPCDS[i];
        }

        drtCnt = props.DROCKET;
        srtCnt = props.SROCKET;
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < cds.Length; i++)
        {
            if (cds[i] > 0f)
            {
                cds[i] -= Time.deltaTime;
            }
        }
	}

    public void FireLaser()
    {
        // laser code
    }

    public void FireBullet()
    {
        if (cds[0] <= 0f && props.BULLET)
        {
            GameObject bt = (GameObject)Instantiate(BT, transform.position + transform.forward, Quaternion.identity);
            projectile_DMG proj_dmg = bt.GetComponent<projectile_DMG>();
            proj_dmg.SetProjectileInfo(props.ATK, "BT");
            bt.GetComponent<Rigidbody>().AddForce(transform.forward * 200f);
            
            Destroy(bt, 1f);

            cds[0] = props.WEPCDS[0];
        }
    }

    public void FireDRocket (Vector3 pos) {
        if (cds[2] <= 0f && drtCnt != 0) {
            GameObject drt = (GameObject)Instantiate(DRT, transform.position + transform.forward, Quaternion.identity);
            rocket_script drs = drt.GetComponent<rocket_script>();
            drs.SetTargetPosition(pos);
            drs.SetRocketInfo(props.ATK);

            drs.SetSpeed(15f);
            drtCnt--;

            if (drtCnt == 0)
            {
                drtCnt = props.DROCKET;
                cds[2] = props.WEPCDS[2];
            }
            else
            {
                cds[2] = 0.25f;
            }

            Destroy(drt, 3f);
        }
    }

    public void FireSRocket (GameObject target) {
        if (cds[3] <= 0f && srtCnt != 0) {
            GameObject srt = (GameObject)Instantiate(SRT, transform.position + transform.forward, Quaternion.identity);
            rocket_script srs = srt.GetComponent<rocket_script>();
            srs.SetTarget(target);
            srs.SetRocketInfo(props.ATK);

            srs.SetSpeed(15f);

            srtCnt--;

            if (srtCnt == 0)
            {
                srtCnt = props.SROCKET;
                cds[3] = props.WEPCDS[3];
            }
            else
            {
                cds[3] = 0.25f;
            }

            Destroy(srs, 3f);
        }
    }

    public void SetDmg(float atk, string type)
    {
        float totalDmg = 0f;
        if (type == "BT")
        {
            totalDmg = atk;
        }
        else if (type == "RT")
        {
            totalDmg = atk * 2f;
        }
        else if (type == "LS")
        {
            totalDmg = atk * 0.75f;
        }

        props.DEF -= totalDmg;
    }
}

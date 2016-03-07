using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class em_properties : MonoBehaviour {
    public float HP;


	public float ATK, DEF, SHLD;
	public float SPD, TSPD, MIN_DIST;
	public int DROCKET, SROCKET;
	public bool LASER, BULLET;

	public float[] WEPCDS;

	public void SetProperties(em_properties_cls props) {
		this.ATK = props.ATK;
		this.DEF = props.DEF; // float
		this.SHLD = props.SHLD; // float
		this.BULLET = props.BULLET; // bool
		this.LASER = props.LASER;
		this.DROCKET = props.DROCKET; // int
		this.SROCKET = props.SROCKET; // int
		
		this.WEPCDS = props.WEPCDS; // float[3] 0: bullet cd 1: laser cd 2: d-rocket recharge cd 3: s-rocket recharge cd

		this.SPD = props.SPD;
		this.TSPD = props.TSPD;
		this.MIN_DIST = props.MIN_DIST;
	}

    void LateUpdate()
    {
        if (DEF <= 0f)
        {
            GameObject[] master_ = GameObject.FindGameObjectsWithTag("master");
            GameObject m_ = master_[0];
            m_.GetComponent<exp_manager>().AddPoint();

            m_.GetComponent<main_ui_manager>().increaseScore();
            GetComponent<shard_explode>().explode(transform.position, 5);
            Destroy(gameObject);
        }
    }
}

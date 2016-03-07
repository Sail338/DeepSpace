using UnityEngine;
using System.Collections;

public class rocket_script : MonoBehaviour {
	
	private Rigidbody rb;
	private TrailRenderer tr;

    private float atk;

	private GameObject target;

	private float trackCd;
	private float trackCdMax;
	private float spd;

	private Vector3 targetPosition;

	private Vector3 default_rotation;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		tr = GetComponent<TrailRenderer>();

		default_rotation = transform.rotation.eulerAngles;

		trackCd = trackCdMax;
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(Vector3.Distance(transform.position, targetPosition)) < 1f) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "player")
                {
                    player_weapons_controller pwc = colliders[i].GetComponent<player_weapons_controller>();
                    pwc.SetDmg(atk, "RT");
                }
                else if (colliders[i].tag == "enemy")
                {
                    em_weapons_controller ewc = colliders[i].GetComponent<em_weapons_controller>();
                    ewc.SetDmg(atk, "RT");
                }
            }


            Destroy();
		}

		rb.velocity = transform.forward * spd;

		if (trackCd <= 0f) {
			if (target != null) pullTargetPositionData();
			trackCd = trackCdMax;
		} else {
			trackCd -= Time.deltaTime;
		}
		
		// It works. Do not touch pls.
		Vector3 direction = targetPosition - transform.position;
		float newRot = Mathf.Atan(direction.x / direction.z) * 180f / Mathf.PI;
		float rotSpd = 20f;
		if (direction.z > 0f) {
			Quaternion newQuatRot = Quaternion.Euler(default_rotation.x, default_rotation.y + newRot, default_rotation.z);
			transform.rotation = Quaternion.Lerp(transform.rotation, newQuatRot, Time.deltaTime * rotSpd);
		} else {
			Quaternion newQuatRot = Quaternion.Euler(default_rotation.x, default_rotation.y + 180f + newRot, default_rotation.z);
			transform.rotation = Quaternion.Lerp(transform.rotation, newQuatRot, Time.deltaTime * rotSpd);
		}
	}

	public void SetTarget(GameObject target) {
		this.target = target;
	} // sets target to track, use for smart rockets

	public void SetTargetPosition(Vector3 pos) {
		this.targetPosition = pos;
	} // sets position directly, use for dumb fire

	public void SetTrackCd(float cd) {
		trackCdMax = cd;
	} // track cooldown, sets update intervals

	public void SetSpeed(float spd) {
		this.spd = spd;
	} // set rocket speed

	void pullTargetPositionData() {
		if (target != null) {
			targetPosition = target.transform.position;
		}
	} // gets target position

	void OnCollisionEnter (Collision c) {
        if (c.collider.tag == "player")
        {
            player_weapons_controller pwc = c.collider.GetComponent<player_weapons_controller>();
            pwc.SetDmg(atk, "RT");
        }
        else if (c.collider.tag == "enemy")
        {
            em_weapons_controller ewc = c.collider.GetComponent<em_weapons_controller>();
            ewc.SetDmg(atk, "RT");
        }
	}

    public void SetRocketInfo(float atk)
    {
        this.atk = atk;
    }

	public void Destroy() {
        shard_explode se = GetComponent<shard_explode>();
        se.explode_controlled(transform.position, 10, 3f);
		Destroy(gameObject);
	}
}

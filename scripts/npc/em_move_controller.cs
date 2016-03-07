using UnityEngine;
using System.Collections;

public class em_move_controller : MonoBehaviour {
	private GameObject target;
	private Vector3 default_rotation;

	private em_properties props;

    private em_weapons_controller wep_controller;

	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		props = GetComponent<em_properties>();
		default_rotation = transform.rotation.eulerAngles;

        wep_controller = GetComponent<em_weapons_controller>();
		
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            Destroy(gameObject);
        }
        wep_controller.FireSRocket(target);
        wep_controller.FireLaser();
		if (rb.velocity.x > 0f) {
			rb.velocity -= transform.right * Time.deltaTime;
			if (rb.velocity.x < 0f) rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);
		} else if (rb.velocity.x < 0f) {
			rb.velocity += transform.right * Time.deltaTime;
			if (rb.velocity.x > 0f) rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);
		}

		if (transform.position.y != 0f) transform.position += Vector3.up * -1f * transform.position.y;

		transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f));
		Vector3 direction = target.transform.position - transform.position;
		direction.y = 0f;
		
		float tDist = Mathf.Abs(Vector3.Distance(transform.position, target.transform.position));
		if (tDist > props.MIN_DIST) rb.velocity = transform.forward * props.SPD;
		else rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);

		float newRot = Mathf.Atan(direction.x / direction.z) * 180f / Mathf.PI;
		if (direction.z > 0f) {
			Quaternion newQuatRot = Quaternion.Euler(default_rotation.x, default_rotation.y + newRot, default_rotation.z);
			transform.rotation = Quaternion.Lerp(transform.rotation, newQuatRot, Time.deltaTime * props.TSPD);
		} else {
			Quaternion newQuatRot = Quaternion.Euler(default_rotation.x, default_rotation.y + 180f + newRot, default_rotation.z);
			transform.rotation = Quaternion.Lerp(transform.rotation, newQuatRot, Time.deltaTime * props.TSPD);
		}

        Quaternion targetRot = Quaternion.Euler(default_rotation.x, default_rotation.y + newRot, default_rotation.z);
        if (Mathf.Abs(Vector3.Distance(targetRot.eulerAngles, transform.rotation.eulerAngles)) < 0.5f)
        {
            wep_controller.FireBullet();
            wep_controller.FireDRocket(target.transform.position);
        }
	}

	void OnCollisionEnter(Collision c) {
		if (c.collider.tag == "enemy") {
			float d = Random.value;
			rb.velocity = (d > 0.5 ? transform.right : (transform.right * -1f)) * 5f;
		}
	}

	public void SetTarget(GameObject target) {
		this.target = target;
	}
}

using UnityEngine;
using System.Collections;

public class player_move_controller : MonoBehaviour {
	
	private Rigidbody rb;
	private Vector3 target;
	private player_properties props;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		target = Vector3.zero;
		props = GetComponent<player_properties>();

		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y != 0f) transform.position += Vector3.up * -1f * transform.position.y; // fixes to plane

		target = getCursorPosition("gnd");
		target.y = transform.position.y;
		transform.LookAt(target, Vector3.up);	
	
		float dist = Mathf.Abs(Vector3.Distance(transform.position, target));
		if (Input.GetKey(KeyCode.W) && dist >= 2f) {
			if (Input.GetKey(KeyCode.LeftShift)) {
				rb.velocity = transform.forward * props.SPD * 2;
			}
			else rb.velocity = transform.forward * props.SPD;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = transform.forward * props.SPD * -1f;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = transform.right * props.SPD * -2f;
            }
            else rb.velocity = transform.right * props.SPD * -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = transform.right * props.SPD * 2f;
            }
            else rb.velocity = transform.right * props.SPD;
        }
	}

	Vector3 getCursorPosition(string tag="", float heightAdj = -1f) {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);
		Vector3 target = Vector3.zero;
		if (tag == "") {
			target = hit.point;
		} else {
			if (hit.collider.gameObject.tag == tag) {
				target = hit.point;
			}
		}
		if (heightAdj != -1f) {
			target.y = heightAdj;
		}
		return target;
	}	
}

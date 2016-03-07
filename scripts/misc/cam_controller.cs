using UnityEngine;
using System.Collections;

public class cam_controller : MonoBehaviour {
	private GameObject target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            Vector3 targetPos = target.transform.position;
            targetPos.y = transform.position.y;

            transform.position = targetPos;
        }
	}

	public void SetTarget(GameObject target) {
		this.target = target;
	}
}

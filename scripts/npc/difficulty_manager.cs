using UnityEngine;
using System.Collections;

public class difficulty_manager : MonoBehaviour {
    private float time_increment;

    private properties props;
	// Use this for initialization
	void Start () {
        time_increment = 30f;
        props = GetComponent<properties>();
	}
	
	// Update is called once per frame
	void Update () {
        if (time_increment <= 0f)
        {
            for (int i = 0; i < 5; i++)
            {
                props.ATK[i] *= 1.5f;
                props.DEF[i] *= 1.5f;
            }

            time_increment = 30f;
        }
        else
        {
            time_increment -= Time.deltaTime;
        }
	}
}

using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class master : MonoBehaviour {
	// ships
	public GameObject[] ecps; // enemy combatant players
	public GameObject player;

    public Canvas endgame;
	// spawn cooldowns of enemies

    private float[] spawnCds;
    public float[] maxCds;
	// spawn limits
	
	private float maxDistance, minDistance;

	// property manager
	
	private properties prop_manager;

    private float timeAdjust;
	// Use this for initialization

    void Awake()
    {
        timeAdjust = 30f;
        prop_manager = gameObject.AddComponent<properties>() as properties;
    }

    void Start()
    {
        spawnCds = new float[5];
        player = (GameObject)Instantiate(player, Vector3.zero, Quaternion.identity);
        GetComponent<main_ui_manager>().SetPlayer(player);
        Camera.main.GetComponent<cam_controller>().SetTarget(player);

        maxDistance = 20f;
        minDistance = 5f;

        prop_manager.AssignDefaults();

        for (int i = 0; i < spawnCds.Length; i++)
        {
            spawnCds[i] = maxCds[i];
        }
    }    
	
	// Update is called once per frame
	void Update () {
        if (timeAdjust <= 0f)
        {
            for (int i = 0; i < maxCds.Length; i++)
            {
                if (maxCds[i] > 3f) maxCds[i] -= 0.25f;
            }

            timeAdjust = 20f;
        }
        else
        {
            timeAdjust -= Time.deltaTime;
        }
        if (player == null)
        {
            Instantiate(endgame, endgame.transform.position, endgame.transform.rotation);
            Destroy(gameObject);
        }

        for (int i = 0; i < spawnCds.Length; i++)
        {
            if (spawnCds[i] <= 0f && maxCds[i] != -100)
            {

                Vector2 spawn = Random.insideUnitCircle;
                spawn *= 20f;

                float x = spawn.x;
                float z = spawn.y;

                GameObject ecp = (GameObject)Instantiate(ecps[i], new Vector3(x, 0f, z), Quaternion.Euler(Vector3.zero));
                ecp.GetComponent<em_move_controller>().SetTarget(player);

                em_properties_cls em_props = prop_manager.GetProperties(i);

                ecp.GetComponent<em_properties>().SetProperties(em_props);

                spawnCds[i] = maxCds[i];

            }
            else
            {
                spawnCds[i] -= Time.deltaTime;
            }
        }
	}

    public GameObject getPlayer()
    {
        return player;
    }
}

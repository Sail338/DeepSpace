using UnityEngine;
using System.Collections;

public class projectile_DMG : MonoBehaviour {
    public float DMG;
    public string TAG;

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.tag == "player") {
            c.gameObject.GetComponent<player_weapons_controller>().SetDmg(DMG, TAG);
            Destroy(gameObject);
        }
        else if (c.collider.tag == "enemy")
        {
            c.gameObject.GetComponent<em_weapons_controller>().SetDmg(DMG, TAG);
            Debug.Log("COLE " + DMG);
            Destroy(gameObject);
        }

    }

    public void SetProjectileInfo(float dmg, string tag)
    {
        DMG = dmg;
        TAG = tag;
    }
} 
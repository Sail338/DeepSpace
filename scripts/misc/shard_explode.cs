using UnityEngine;
using System.Collections;

public class shard_explode : MonoBehaviour {
    public GameObject shard_base;

    public void explode(Vector3 pos, int shards)
    {
        explode_controlled(pos, shards, 5f);
    }

    public void explode_controlled(Vector3 pos, int shards, float lifetime)
    {
        for (int i = 0; i < shards; i++)
        {
            GameObject shard = (GameObject)Instantiate(shard_base, pos, Quaternion.identity);
            Rigidbody rb = shard.GetComponent<Rigidbody>();
            float rand_Fx = Random.value * 50f - 25f;
            float rand_Fz = Random.value * 50f - 25f;

            rb.AddForce(new Vector3(rand_Fx, 0f, rand_Fz));

            Destroy(shard, lifetime);
        }
    }
}

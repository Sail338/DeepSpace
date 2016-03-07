using UnityEngine;
using System.Collections;

public class exp_manager : MonoBehaviour
{
    int points;
    // Use this for initialization
    void Awake()
    {
        points = 0;
    }

    public void AddPoint()
    {
        points++;
        if (points % 3 == 0) // for demo only, else 5
        {
            PlayerPrefs.SetInt("EXP", PlayerPrefs.GetInt("EXP") + 1);
            points = 0;
        }
    }
}
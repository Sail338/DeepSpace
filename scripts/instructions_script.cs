using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class instructions_script : MonoBehaviour {
    public Button main;
	public void toMain()
    {
        Application.LoadLevel(0);
    }
}

using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class main_menu_script : MonoBehaviour {
    public Button play, instructions;
    
	public void toPlay()
    {
        Application.LoadLevel(2);
    }

    public void toInstructions()
    {
        Application.LoadLevel(1);
    }
}

using UnityEngine;
using System.Collections;

public class properties : MonoBehaviour {
	private int size;
    public float[] ATK, DEF, SHLD;
	public bool[] BULLET;
	public bool[] LASER;
	public int[] DROCKET, SROCKET;
	public float[] SPD;
	public float[] TSPD;
	
	public float[] MIN_DIST;

	public float[][] WEPCDS;

	void Awake() {
		size = 5;
        ATK = new float[size];
        Debug.Log(ATK.Length);
        DEF = new float[size];
        SHLD = new float[size];
        SPD = new float[size];
        TSPD = new float[size];
        MIN_DIST = new float[size];
        DROCKET = new int[size];
        SROCKET = new int[size];
        BULLET = new bool[size];
        LASER = new bool[size];

		WEPCDS = new float[5][];

	}

	public void AssignDefaults() {
		// enemy 1 normal - bullet - specials
		ATK[0] = 1f;
		DEF[0] = 10f;
		SHLD[0] = 0f;
		BULLET[0] = LASER[0] = true;
		DROCKET[0] = SROCKET[0] = 0;
		SPD[0] = 2f;
		TSPD[0] = 10f;
		WEPCDS[0] = new float[]{.1f, 0f, 0f, 0f};

		MIN_DIST[0] = 20f;

		// enemy 2 mid-fast - bullet - drockets
		ATK[1] = 10f;
		DEF[1] = 10f;
		SHLD[1] = 0f;
		BULLET[1] = true;
		LASER[1] = false;
		DROCKET[1] = 3;
		SROCKET[1] = 0;
		SPD[1] = 5f;
		TSPD[1] = 10f;
		WEPCDS[1] = new float[]{0.25f, 0f, 6f, 0f};

		MIN_DIST[1] = 20f;

		// enemy 3 fast - laser - srockets - shield 1x
		ATK[2] = 15f;
		DEF[2] = 20f;
		SHLD[2] = 10f;
		BULLET[2] = false;
		LASER[2] = true;
		DROCKET[2] = 0;
		SROCKET[2] = 5;
		SPD[2] = 10f;
		TSPD[2] = 10f;
		WEPCDS[2] = new float[]{0f, 3f, 0f, 2f};

		MIN_DIST[2] = 20f;
		
		// enemy 4 fast - suicider
		ATK[3] = 10f;
		DEF[3] = 10f;
		SHLD[3] = 0f;
		BULLET[3] = LASER[3] = false;
		DROCKET[3] = SROCKET[3] = 0;
		SPD[3] = 20f;
		TSPD[3] = 10f;
		WEPCDS[3] = new float[]{0f, 0f, 0f, 0f};

		MIN_DIST[3] = 20f;

		// enemy 5 mid-fast - laser - srocket - drocket - shield 2x
		ATK[4] = 10f;
		DEF[4] = 30f;
		SHLD[4] = 45f;
		BULLET[4] = false;
		LASER[4] = true;
		SPD[4] = 8f;
		TSPD[4] = 10f;
		DROCKET[4] = SROCKET[4] = 5;	

		WEPCDS[4] = new float[]{.1f, .25f, 2f, 2f};

		MIN_DIST[4] = 20f;

		Debug.Log("FINISHED DEFS");
	}

	public em_properties_cls GetProperties(int i) {
		em_properties_cls propsCls = new em_properties_cls();
		propsCls.ATK = ATK[i];
        Debug.Log("PPCS " + ATK[i]);
		propsCls.DEF = DEF[i]; // float
		propsCls.SHLD = SHLD[i]; // float
		propsCls.BULLET = BULLET[i]; // bool
		propsCls.LASER = LASER[i];
		propsCls.DROCKET = DROCKET[i]; // int
		propsCls.SROCKET = SROCKET[i]; // int
		
		propsCls.WEPCDS = WEPCDS[i]; // float[3] 0: bullet cd 1: laser cd 2: d-rocket recharge cd 3: s-rocket recharge cd

		propsCls.MIN_DIST = MIN_DIST[i];

		propsCls.SPD = SPD[i];
		propsCls.TSPD = TSPD[i];

		return propsCls;
	}
}

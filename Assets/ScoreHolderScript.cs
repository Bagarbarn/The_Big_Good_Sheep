using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHolderScript : MonoBehaviour {

    public int p_endScore;

	void Awake () {

        GameObject[] ScoreHolders = GameObject.FindGameObjectsWithTag("ScoreHolder");

        if (ScoreHolders.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
	}
}

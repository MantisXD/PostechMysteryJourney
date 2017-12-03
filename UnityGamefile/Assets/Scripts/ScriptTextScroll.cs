using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScriptTextScroll : MonoBehaviour {

    public ScrollRect TextScroll; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Set_Scroll()
    {
        TextScroll.verticalNormalizedPosition = 0f;
    }
}

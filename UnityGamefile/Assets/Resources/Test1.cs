using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour {

    public Button Dummy;
    public Text Script;

	// Use this for initialization
	void Start () {
		
	}
	

    public void ShowText()
    {
        Script.text = "테스트 텍스트";
    }

	// Update is called once per frame
	void Update () {
		
	}
}

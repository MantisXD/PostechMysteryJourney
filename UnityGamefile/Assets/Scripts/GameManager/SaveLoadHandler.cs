using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadHandler : MonoBehaviour {

    public GameObject SaveLoadBackground;

    // Use this for initialization
    void Start () {
		
	}
	
    public void LoadWindowCreate(bool NewFile)
    {
        SaveLoadBackground.SetActive(true);
        SaveLoadBackground.GetComponent<SaveLoadButtonCreate>().CreateLoadButton();
    }

	// Update is called once per frame
	void Update () {
		
	}
}

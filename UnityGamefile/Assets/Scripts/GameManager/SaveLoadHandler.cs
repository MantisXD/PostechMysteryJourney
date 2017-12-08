using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadHandler : MonoBehaviour {

    public GameObject SaveLoadBackground;
    bool NewGame;

    // Use this for initialization
    void Start () {
		
	}
	
    public void LoadWindowCreate(bool NG)
    {
        NewGame = NG;
        SaveLoadBackground.SetActive(true);
        SaveLoadBackground.GetComponent<SaveLoadButtonCreate>().CreateLoadButton(NewGame);
    }

	// Update is called once per frame
	void Update () {
		
	}
    
    public bool IsNewGame()
    {
        return NewGame;
    }
}

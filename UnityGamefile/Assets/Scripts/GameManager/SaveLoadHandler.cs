using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveLoadHandler : MonoBehaviour {

    public GameObject SaveLoadBackground;
    public GameObject SaveLoadBoundary;
    public GameObject StartHead;
    public Sprite NewHead, OldHead;
    bool NewGame;

    // Use this for initialization
    void Start () {
		
	}
	
    public void LoadWindowCreate(bool NG)
    {
        if (NG)
        {
            StartHead.GetComponent<Image>().sprite = NewHead;
        }
        else
        {
            StartHead.GetComponent<Image>().sprite = OldHead;
        }

        NewGame = NG;
        SaveLoadBoundary.SetActive(true);
        SaveLoadBackground.SetActive(true);
        SaveLoadBoundary.GetComponent<SaveLoadButtonCreate>().CreateLoadButton(NewGame);

    }

	// Update is called once per frame
	void Update () {
		
	}
    
    public bool IsNewGame()
    {
        return NewGame;
    }
}

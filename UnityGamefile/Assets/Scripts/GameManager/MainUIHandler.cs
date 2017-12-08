using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIHandler : MonoBehaviour {

    public GameObject Map, Save, PlayerData;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MapButtonOn()
    {
        Save.SetActive(false);
        PlayerData.SetActive(false);
        Map.SetActive(true);
    }
    public void SaveButtonOn()
    {
        Save.SetActive(true);
        PlayerData.SetActive(false);
        Map.SetActive(false);
    }
    public void PlayerDataButtonOn()
    {
        Save.SetActive(false);
        PlayerData.SetActive(true);
        Map.SetActive(false);

    }

    public void Linker()
    {
        Map = GameObject.Find("Map");
        Save = GameObject.Find("Save");
        PlayerData = GameObject.Find("PlayerData");
        Map.SetActive(false);
        Save.SetActive(false);
        PlayerData.SetActive(false);
    }
}


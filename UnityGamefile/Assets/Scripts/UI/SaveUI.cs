using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveUI : MonoBehaviour {

    public GameObject SaveBackground, SaveBoundary;

    //버튼 누르면 배경 호출하고 버튼 생성도 호출한다.
    public void SaveWindowCreate()
    {
        SaveBackground.SetActive(true);
        SaveBoundary.SetActive(true);
        SaveBoundary.GetComponent<SaveLoadButtonCreate>().CreateSaveButton(SaveBoundary);
    }
    //세이브창 띄워둔거 끌때 사용
    public void SaveCancel()
    {
        GameObject temp;
        while ((temp = GameObject.FindWithTag("SaveButton")) != null)
        {
            temp.SetActive(false);
        }

        SaveBackground.SetActive(false);
        SaveBoundary.SetActive(false);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

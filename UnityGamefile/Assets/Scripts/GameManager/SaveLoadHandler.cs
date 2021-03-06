﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveLoadHandler : MonoBehaviour {

    public GameObject LoadBackground;
    public GameObject LoadBoundary;

    public GameObject StartHead;
    public Sprite NewHead, OldHead;
    bool NewGame;


    // Use this for initialization
    void Start() {

    }

    //로드창 띄워둔거 끌때 사용
    public void LoadCancel()
    {
        GameObject temp;
        while((temp=GameObject.FindWithTag("LoadButton")) != null)
        {
            temp.SetActive(false);
        }

        LoadBackground.SetActive(false);
        LoadBoundary.SetActive(false);

    }
    public void LoadWindowCreate(bool NG)
    {
        //새로 시작하는건지 이어서 하는건지를 구분하는 이미지를 띄웁니다.
        if (NG)
        {
            StartHead.GetComponent<Image>().sprite = NewHead;
        }
        else
        {
            StartHead.GetComponent<Image>().sprite = OldHead;
        }

        NewGame = NG;
        LoadBoundary.SetActive(true);
        LoadBackground.SetActive(true);
        LoadBoundary.GetComponent<SaveLoadButtonCreate>().CreateLoadButton(NewGame);

    }
	// Update is called once per frame
	void Update () {
		
	}
    
    public bool IsNewGame()
    {
        return NewGame;
    }
    public void Linker()
    {
       
    }
}

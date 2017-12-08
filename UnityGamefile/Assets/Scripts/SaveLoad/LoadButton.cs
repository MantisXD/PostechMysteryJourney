using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class LoadButton : MonoBehaviour {

    //게임매니저
    GameObject GameManager;
    String[] LoadedFile;
    int[] Data = new int[5];
    //수수께끼 정보
    List<String> RiddleInfo = new List<String>();

    bool NewGame;
	// Use this for initialization
	void Start () {
        GameManager = GameObject.Find("GameManager");
        NewGame = GameManager.GetComponent<SaveLoadHandler>().IsNewGame();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GetLoadData(String data)
    {
        LoadedFile = data.Split('\n');
    }

    public void Load()
    {
        //이름을 세팅ㅎ나다.
        String[] Temp;
        Temp = LoadedFile[0].Split(' ');
        GameManager.GetComponent<ScriptHandler>().SetName(Temp[1]);
        //데이터를 로드합니다.
        for(int k=1;k<=4;k++)
        {
            Temp = LoadedFile[k].Split(' ');
            Data[k] = int.Parse(Temp[1]);
        }
        Temp = LoadedFile[5].Split(' ');
        //수수께끼를 로드합니다.
        GameManager.GetComponent<RiddleHandler>().RiddleDataLoad(Temp[1], Data[3], Data[4]);
        //데이터 세팅 끝내고

        if (NewGame)
        {
            //새로운 게임을 시작하는거면 별도의 함수를 호출
        }
        else
        {
            //기존 파일 Load하는거면 걍 씬 전환
            GameManager.GetComponent<SceneMove>().SceneShift(Data[1], Data[2]);
        }
    }

}

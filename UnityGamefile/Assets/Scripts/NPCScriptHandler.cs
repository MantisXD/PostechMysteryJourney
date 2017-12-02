﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

//NPC 각자의 Script를 관리하는 Class입니다. ScriptHandler를 상속받습니다.
public class NPCScriptHandler : ScriptHandler
{
    //Script를 출력하는 과정은 다음과 같습니다.
    //1. GameManager의 ScriptHandler에게 적절한 Script를 요청합니다.
    //2. 스크립트 오브젝트는 NPCScriptHandler에게 적절한 Script(String)을 리턴합니다.
    //3. 리턴받은 Script(문자열)을 ScriptText에게 전달합니다
    //이거 만들어 주세요

    //모든 Script가 저장된 Handler를 가리킵니다.
    public ScriptHandler ScriptManager;
    //ScriptHandler가 주는 Printer
    GameObject Printer;

    List<String> TempScript;
    //NPC의 ID와 Sequence입니다. ScriptHandler에게 적절한 Script를 받아내기 위한 정보입니다.
    int NPC_ID, ScriptSequence;
    // Use this for initialization
    void Start () {
        ScriptSequence = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Set_Printer(GameObject P)
    {
        Printer = P;
    }
    public void ScriptFetcher()
    {
        TempScript = ScriptManager.Get_Script(NPC_ID, ScriptSequence);
        //새로운 ScriptPrinter를 생성
        GameObject TempPrinter = Instantiate(Printer);
        //대사를 넘겨줍니다.
        TempPrinter.GetComponent<ScriptPrinter>().Get_Script(RawScript);
        //생성한 뒤 TempScript를 넘겨준다.
    }
    public void Set_ID(int ID)
    {
        NPC_ID = ID;
    }
    /*
    //Script를 로드합니다.(ScriptHandler의 ScriptLoader를 Override합니다)
    void ScriptLoader()
    {
        //현재 씬/Phase에 맞는 Script 파일의 이름을 생성합니다.
        string ScriptTextName = "Script" + Scene.ToString() + "_" + Phase.ToString();
        string Scriptlocation = Application.dataPath + "/Resources/Script/" + ScriptTextName;//Script가 저장된 위치입니다.
        if (File.Exists(Scriptlocation))
        {
            ScriptCache = File.ReadAllLines(Scriptlocation, Encoding.UTF8);
            //맵핑은 한줄한줄 읽은 뒤 String에 Append하는 방식으로 이루어집니다. 이때 \n은 무시합니다.
            //이 NPC에 적합한 Script에 한해서 RawScript에 할당합니다.
        }
        else
        {
            Debug.Log("Script File doesn't exists!");
        }
        Loaded = true;
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

//NPC 각자의 Script를 관리하는 Class입니다. ScriptHandler를 상속받습니다.
public class NPCScriptHandler : ScriptHandler
{
    int NPC_ID;//NPC Script에서 받아옵니다.
    string[] RawScript;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Script를 로드합니다.(ScriptHandler의 ScriptLoader를 Override합니다.
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
    }
}

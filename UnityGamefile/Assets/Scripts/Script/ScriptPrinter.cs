using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class ScriptPrinter : MonoBehaviour {

   
    public ScriptText ScriptText;
    //평소엔 True, 대사를 출력할 때는 False로 지정.
    bool ScriptGate, Loaded;

    List<String> Script;
    //Script를 받고 한줄씩 실행하다가 대사 출력 키워드가 나오면 Click을 기다린다.
    //한프레임에 한줄이요

	// Use this for initialization
	void Start () {
        ScriptGate = false;
        Loaded = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Loaded)
        {
            //한줄씩 실행
        }
	}
    //대사를 복사한다(NPCScriptHandler에서 얘를 생성함과 동시에 호출함)
    public void Get_Script(List<String> S)
    {
        Script = S;
        Loaded = true;
    }

    public void Gate_Open()
    {
        ScriptGate = true;
    }

    //script의 명령어에 따른 수행을 나타내는 함수입니다.

    //화자 설정
    void Set_Speaker(String Speaker)
    {

    }
    //대사 설정(한글자씩 타이핑하는 효과는 일단 안넣을 예정입니다.(넣을 수는 있어요))
    void Set_Script(String Script)
    {

    }
    //Scene이나 Phase를 바꿔주는 함수입니다.
    void Shift(int Phase, int Scene)
    {

    }
    //Speaker의 Sprite를 소환하는 Method입니다.
    void Set_SpeakerSprite(int type,int ID)
    {

    }
    //Speaker를 강조하는지 결정하는 Method입니다(True면 하얗게, False면 약간 뒤로 물러나고, 회색빛으로)
    void Set_SpeakerFocus(int type,bool focus)
    {

    }
}

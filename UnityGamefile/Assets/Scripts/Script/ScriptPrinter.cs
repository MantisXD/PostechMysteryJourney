using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

public class ScriptPrinter : MonoBehaviour {

    ScriptHandler Handler;
    ScriptText ScriptText;
    Text Speaker;
    //평소엔 True, 대사를 출력할 때는 False로 지정.
    bool ScriptGate, Loaded;
    //몇줄 남았는가?
    int pos = 0, count;
    
    List<String> Script;
    //Script를 받고 한줄씩 실행하다가 대사 출력 키워드가 나오면 Click을 기다린다.
    //한프레임에 한줄이요

    private void Awake()
    {
        ScriptGate = false;
    }

    // Use this for initialization
    void Start ()
    {
        /*
        float width, height;
        GameObject CanvasTransform = GameObject.Find("Canvas");
        //Canvas의 하위 Object가 되게 한다.
        transform.SetParent(CanvasTransform.transform);
        width = CanvasTransform.GetComponent<RectTransform>().rect.width;
        height = CanvasTransform.GetComponent<RectTransform>().rect.height;
        this.GetComponent<RectTransform>().pivot.Set(0.5f, 0.5f);
        this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width*2);
        this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height*2);
        //this.GetComponent<RectTransform>().anchorMax.Set(0f, 0f);
     //   this.GetComponent<RectTransform>().anchorMin.Set(width, height);
       // this.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
       */


        //Script와 Speaker를 출력하기 위해 Object를 찾아서 메모리에 Load한다.
        ScriptText = GameObject.Find("ScriptText").GetComponent<ScriptText>();
        Speaker = GameObject.Find("SpeakerText").GetComponent<Text>();

        //자기 자신을 비활성화 시키는 함수를 호출하기 위해 Handler를 Load한다ㅏ.
        Handler = GameObject.Find("GameManager").GetComponent<ScriptHandler>();

        //일단 Gate를 닫고, 파일이 로드되지 않았음을 나타낸다.
     
	}
	
	// Update is called once per frame
	void Update () {
		if(ScriptGate && (Loaded && pos < count))
        {
            string[] TempScript = Script[pos].Split(' ');

            if(TempScript[0] == "Speaker")
            {
                Set_Speaker(TempScript[1]);
            }
            if(TempScript[0] == "Script")
            {
                Set_Script(Script[pos]);
            }
            if(TempScript[0] == "SetSpeakerPhase")
            {
                pos++;
            }
            
        }
        else if(pos >= count)
        {
            Set_Speaker("NULL");
            Set_Script("NULL");

            pos = 0;
            count = 1;
            ScriptGate = false;

            //나를 죽여줘
            Handler.Deactivate_Printer();
        }
	}
    //대사를 복사한다(NPCScriptHandler에서 얘를 생성함과 동시에 호출함)
    public void Get_Script(List<String> S)
    {
        Script = S;
        Loaded = true;
        ScriptGate = true;
        pos = 0;
        count = S.Count;
    }

    public void Gate_Open()
    {
        if (ScriptGate == false)
        {
            ScriptGate = true;
            pos++;
        }
    }

    //script의 명령어에 따른 수행을 나타내는 함수입니다.

    //화자 설정
    void Set_Speaker(String SpeakerName)
    {
        if (SpeakerName == "NULL")
        {
            Speaker.text = " ";
        }
        else
        {
            Speaker.text = SpeakerName;
        }
        pos++;
    }
    //대사 설정(한글자씩 타이핑하는 효과는 일단 안넣을 예정입니다.(넣을 수는 있어요))
    void Set_Script(String Script)
    {
        if (Script == "NULL")
        {
            ScriptText.TextPrint(" ");
        }
        else
        {
            string[] TempScript = Script.Split('"');
            ScriptText.TextPrint(TempScript[1]);
        }
        ScriptGate = false;
    }
    //Scene이나 Phase를 바꿔주는 함수입니다.
    void Shift(int Phase, int Scene)
    {
        pos++;
    }
    //Speaker의 Sprite를 소환하는 Method입니다.
    void Set_SpeakerSprite(int type,int ID)
    {
        pos++;
    }
    //Speaker를 강조하는지 결정하는 Method입니다(True면 하얗게, False면 약간 뒤로 물러나고, 회색빛으로)
    void Set_SpeakerFocus(int type,bool focus)
    {
        pos++;
    }
}

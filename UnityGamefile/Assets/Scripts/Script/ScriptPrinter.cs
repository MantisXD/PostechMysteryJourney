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
    String PlayerName;

    List<String> Script;

    public GameObject ScriptBack, SpeakerBack;
    NPCHandler NPCSpriteHandler;
    public GameObject LeftStand, MiddleStand, RightStand;

    //수수께끼를 풀었는가?(0 = 수수께기 시작도 안함 / 1 = 수수께끼 푸는 중 / 2 = 수수께끼 못품 / 3 = 수수께끼 풀었음)
    int RiddleStatus;

    private void Awake()
    {
        ScriptGate = false;
    }

    // Use this for initialization
    void Start ()
    {
        
        //Script와 Speaker를 출력하기 위해 Object를 찾아서 메모리에 Load한다.
        ScriptText = GameObject.Find("ScriptText").GetComponent<ScriptText>();
        Speaker = GameObject.Find("SpeakerText").GetComponent<Text>();
        //NPCSpriteHandler도 찾는다
        NPCSpriteHandler = GameObject.Find("GameManager").GetComponent<NPCHandler>();
        //자기 자신을 비활성화 시키는 함수를 호출하기 위해 Handler를 Load한다ㅏ.
        Handler = GameObject.Find("GameManager").GetComponent<ScriptHandler>();

        //일단 Gate를 닫고, 파일이 로드되지 않았음을 나타낸다.
     
	}
	
	// Update is called once per frame
	void Update () {

        if ((ScriptGate && (Loaded && pos < count)))
        {
            if (RiddleStatus == 0)
            {
                ScriptBack.SetActive(true);
                SpeakerBack.SetActive(true);
                string[] TempScript = Script[pos].Split(' ');

                if (TempScript[0] == "Speaker")
                {
                    Set_Speaker(TempScript[1]);
                }
                if (TempScript[0] == "Script")
                {
                    Set_Script(Script[pos]);
                }
                if (TempScript[0] == "SetSpeakerPhase")
                {
                    pos++;
                }
                if (TempScript[0] == "SceneShift")
                {
                    int temp;
                    int.TryParse(TempScript[1], out temp);
                    Handler.Shifter(temp, -1);
                }
                if (TempScript[0] == "PhaseShift")
                {
                    int temp;
                    int.TryParse(TempScript[1], out temp);
                    Handler.Shifter(-1, temp);
                }
                if (TempScript[0] == "Shift")
                {
                    int tempPhase, tempScene;
                    int.TryParse(TempScript[1], out tempScene);
                    int.TryParse(TempScript[2], out tempPhase);
                    Handler.Shifter(tempScene, tempPhase);
                }
                if (TempScript[0] == "Sprite")
                {
                    pos++;
                    if (TempScript[1] == "Left")
                    {
                        if (TempScript[2] == "NULL")
                        {
                            LeftStand.GetComponent<Image>().sprite = null;
                            LeftStand.SetActive(false);
                        }
                        else
                        {
                            Sprite tempS = NPCSpriteHandler.GetStandingNPCSprite(int.Parse(TempScript[2]));
                            LeftStand.SetActive(true);
                            LeftStand.GetComponent<Image>().sprite = tempS;
                            LeftStand.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tempS.rect.width / tempS.rect.height * 350);
                        }
                    }
                    if (TempScript[1] == "Middle")
                    {
                        if (TempScript[2] == "NULL")
                        {
                            MiddleStand.GetComponent<Image>().sprite = null;
                            MiddleStand.SetActive(false);
                        }
                        else
                        {
                            Sprite tempS = NPCSpriteHandler.GetStandingNPCSprite(int.Parse(TempScript[2]));
                            MiddleStand.SetActive(true);
                            MiddleStand.GetComponent<Image>().sprite = tempS;
                            MiddleStand.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tempS.rect.width / tempS.rect.height * 350);
                        }
                    }
                    if (TempScript[1] == "Right")
                    {
                        if (TempScript[2] == "NULL")
                        {
                            RightStand.GetComponent<Image>().sprite = null;
                            RightStand.SetActive(false);
                        }
                        else
                        {
                            Sprite tempS = NPCSpriteHandler.GetStandingNPCSprite(int.Parse(TempScript[2]));
                            RightStand.SetActive(true);
                            RightStand.GetComponent<Image>().sprite = tempS;
                            RightStand.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tempS.rect.width / tempS.rect.height * 350);
                        }
                    }
                }
                if (TempScript[0] == "Riddle")
                {

                    pos++;
                    //수수께끼 푸는중...
                    RiddleStatus = 1;
                    //수수께끼를 호출한다.
                    GameObject.Find("GameManager").GetComponent<RiddleHandler>().RiddleStart(int.Parse(TempScript[1]), true);
                }
            }
            //수수께끼를 못 풀었구나!(대사출력만 관리합니다, 점수 까이는건 다른데서 하세요)
            if (RiddleStatus == 2)
            {
                ScriptBack.SetActive(true);
                SpeakerBack.SetActive(true);
                string[] TempScript = Script[pos].Split(' ');
                if (TempScript[0] == "RiddleSuccess")
                {
                    do
                    {
                        pos++;
                        TempScript = Script[pos].Split(' ');
                    } while (!(TempScript[0] == "RiddleSuccess" && TempScript[1] == "End"));
                    pos++;
                }

                TempScript = Script[pos].Split(' ');
                if (TempScript[0] == "Speaker")
                {
                    Set_Speaker(TempScript[1]);
                }
                if (TempScript[0] == "Script")
                {
                    Set_Script(Script[pos]);
                }
                if (TempScript[0] == "SetSpeakerPhase")
                {
                    pos++;
                }
                if (TempScript[0] == "SceneShift")
                {
                    RiddleStatus = 0;
                    int temp;
                    int.TryParse(TempScript[1], out temp);
                    Handler.Shifter(temp, -1);
                }
                if (TempScript[0] == "PhaseShift")
                {
                    RiddleStatus = 0;
                    int temp;
                    int.TryParse(TempScript[1], out temp);
                    Handler.Shifter(-1, temp);
                }
                if (TempScript[0] == "Shift")
                {
                    int tempPhase, tempScene;
                    int.TryParse(TempScript[1], out tempScene);
                    int.TryParse(TempScript[2], out tempPhase);
                    Handler.Shifter(tempScene, tempPhase);
                }
                if (TempScript[0] == "Sprite")
                {
                    pos++;
                    if (TempScript[1] == "Left")
                    {
                        if (TempScript[2] == "NULL")
                        {
                            LeftStand.GetComponent<Image>().sprite = null;
                            LeftStand.SetActive(false);
                        }
                        else
                        {
                            Sprite tempS = NPCSpriteHandler.GetStandingNPCSprite(int.Parse(TempScript[2]));
                            LeftStand.SetActive(true);
                            LeftStand.GetComponent<Image>().sprite = tempS;
                            LeftStand.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tempS.rect.width / tempS.rect.height * 350);
                        }
                    }
                    if (TempScript[1] == "Middle")
                    {
                        if (TempScript[2] == "NULL")
                        {
                            MiddleStand.GetComponent<Image>().sprite = null;
                            MiddleStand.SetActive(false);
                        }
                        else
                        {
                            Sprite tempS = NPCSpriteHandler.GetStandingNPCSprite(int.Parse(TempScript[2]));
                            MiddleStand.SetActive(true);
                            MiddleStand.GetComponent<Image>().sprite = tempS;
                            MiddleStand.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tempS.rect.width / tempS.rect.height * 350);
                        }
                    }
                    if (TempScript[1] == "Right")
                    {
                        if (TempScript[2] == "NULL")
                        {
                            RightStand.GetComponent<Image>().sprite = null;
                            RightStand.SetActive(false);
                        }
                        else
                        {
                            Sprite tempS = NPCSpriteHandler.GetStandingNPCSprite(int.Parse(TempScript[2]));
                            RightStand.SetActive(true);
                            RightStand.GetComponent<Image>().sprite = tempS;
                            RightStand.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tempS.rect.width / tempS.rect.height * 350);
                        }
                    }
                }
                if(TempScript[0] == "RiddleFail" && TempScript[1] == "End")
                {
                    RiddleStatus = 0;
                    pos++;
                }
            }
            if (RiddleStatus == 3)
            {
                ScriptBack.SetActive(true);
                SpeakerBack.SetActive(true);
                string[] TempScript = Script[pos].Split(' ');
                if (Script[pos] == "RiddleSuccess")
                {
                    pos++;
                }
                if (TempScript[0] == "RiddleFail")
                {
                    do
                    {
                        pos++;
                        TempScript = Script[pos].Split(' ');
                    } while (!(TempScript[0] == "RiddleFail" && TempScript[1] == "End"));
                    pos++;
                }
                if (TempScript[0] == "Speaker")
                {
                    Set_Speaker(TempScript[1]);
                }
                if (TempScript[0] == "Script")
                {
                    Set_Script(Script[pos]);
                }
                if (TempScript[0] == "SetSpeakerPhase")
                {
                    pos++;
                }
                if (TempScript[0] == "SceneShift")
                {
                    RiddleStatus = 0;
                    int temp;
                    int.TryParse(TempScript[1], out temp);
                    Handler.Shifter(temp, -1);
                }
                if (TempScript[0] == "PhaseShift")
                {
                    RiddleStatus = 0;
                    int temp;
                    int.TryParse(TempScript[1], out temp);
                    Handler.Shifter(-1, temp);
                }
                if (TempScript[0] == "Shift")
                {
                    int tempPhase, tempScene;
                    int.TryParse(TempScript[1], out tempScene);
                    int.TryParse(TempScript[2], out tempPhase);
                    Handler.Shifter(tempScene, tempPhase);
                }
                if (TempScript[0] == "Sprite")
                {
                    pos++;
                    if (TempScript[1] == "Left")
                    {
                        if (TempScript[2] == "NULL")
                        {
                            LeftStand.GetComponent<Image>().sprite = null;
                            LeftStand.SetActive(false);
                        }
                        else
                        {
                            Sprite tempS = NPCSpriteHandler.GetStandingNPCSprite(int.Parse(TempScript[2]));
                            LeftStand.SetActive(true);
                            LeftStand.GetComponent<Image>().sprite = tempS;
                            LeftStand.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tempS.rect.width / tempS.rect.height * 350);
                        }
                    }
                    if (TempScript[1] == "Middle")
                    {
                        if (TempScript[2] == "NULL")
                        {
                            MiddleStand.GetComponent<Image>().sprite = null;
                            MiddleStand.SetActive(false);
                        }
                        else
                        {
                            Sprite tempS = NPCSpriteHandler.GetStandingNPCSprite(int.Parse(TempScript[2]));
                            MiddleStand.SetActive(true);
                            MiddleStand.GetComponent<Image>().sprite = tempS;
                            MiddleStand.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tempS.rect.width / tempS.rect.height * 350);
                        }
                    }
                    if (TempScript[1] == "Right")
                    {
                        if (TempScript[2] == "NULL")
                        {
                            RightStand.GetComponent<Image>().sprite = null;
                            RightStand.SetActive(false);
                        }
                        else
                        {
                            Sprite tempS = NPCSpriteHandler.GetStandingNPCSprite(int.Parse(TempScript[2]));
                            RightStand.SetActive(true);
                            RightStand.GetComponent<Image>().sprite = tempS;
                            RightStand.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tempS.rect.width / tempS.rect.height * 350);
                        }
                    }
                }
                if (TempScript[0] == "RiddleSuccess" && TempScript[1] == "End")
                {
                    RiddleStatus = 0;
                    pos++;
                }
            }
        }
        else if (pos >= count)
        {
            Set_Speaker("NULL");
            Set_Script("NULL");
            pos = 0;
            count = 1;
            ScriptGate = false;
            ScriptBack.SetActive(false);
            SpeakerBack.SetActive(false);
            //나를 죽여줘
            Handler.Deactivate_Printer();
        }
	}
    //대사를 복사한다(NPCScriptHandler에서 얘를 생성함과 동시에 호출함)
    public void Get_Script(List<String> S,String Name)
    {
        PlayerName = Name;
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
        else if (SpeakerName == "Player")
        {
            Speaker.text = PlayerName;
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
    public void SetRiddleState(int value)
    {
        RiddleStatus = value;
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

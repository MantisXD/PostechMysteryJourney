using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

public class RiddleClass : MonoBehaviour
{

    public int Number, LeftScore, Hint, InitScore, Difficulty;//난이도 항목도 필요하다고 봄.
    ScriptText ScriptText;
    Text Speaker;
    public bool IsSolved;
    public string Name;
    public UILabel answer;
    ScriptHandler Handler;
    //평소엔 True, 대사를 출력할 때는 False로 지정.
    bool ScriptGate, Loaded;
    //몇줄 남았는가?
    int pos = 0, count;

    List<String> Script;

    public GameObject ScriptBack, SpeakerBack;
    NPCHandler NPCSpriteHandler;
    public GameObject LeftStand, MiddleStand, RightStand;

    private void Awake()
    {
        ScriptGate = false;
    }
    //UILabel에 왜 빨간줄이 뜨는지 잘 모르겠다..ㅠㅠ
    //정답 입력 받는 것 필요
    //input field 를 만들어야한다고 한다.
    void OnClick()
    {//한국어도 인식하기 위해 이렇게한다고함.
        string convertName = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(answer.text));
        PlayerPrefs.SetString("answer", convertName);
        //PlayerPref라는 field를 만들면 입력받을 수 있다고 함.
    }

    // Use this for initialization
    void Start()
    {
        //Script와 Speaker를 출력하기 위해 Object를 찾아서 메모리에 Load한다. (베껴옴)
        //수수께끼도 시작하기 전에 대사 있으니까 가져오면 되지 않을까 생각했다.
        ScriptText = GameObject.Find("ScriptText").GetComponent<ScriptText>();
        Speaker = GameObject.Find("SpeakerText").GetComponent<Text>();
        //NPCSpriteHandler도 찾는다
        NPCSpriteHandler = GameObject.Find("GameManager").GetComponent<NPCHandler>();
        //자기 자신을 비활성화 시키는 함수를 호출하기 위해 Handler를 Load한다.
        Handler = GameObject.Find("GameManager").GetComponent<ScriptHandler>();
        //일단 Gate를 닫고, 파일이 로드되지 않았음을 나타낸다.
    }

    // Update is called once per frame
    void Update()
    {
        //scriptpirnter을 이용해서 비슷한 형식으로 txt를 파싱하고 쓰면 되지 않을까 생각했음.
        //txt 이름은 Riddle_m_1 => 메인 1번 퀘스트
        //서브의 경우 Riddle_s_1 과 같이?
        //사진을 띄우는 거는 npc 띄우는 걸로 생각해서 하면 되지 않을까..???
        //잘 모르겠어서 일단 복붙함
        if (ScriptGate && (Loaded && pos < count))
        {

            ScriptBack.SetActive(true);
            SpeakerBack.SetActive(true);
            string[] TempScript = Script[pos].Split(' ');
            if (TempScript[0] == "Difficulty")
            {
                // Difficulty 1 으로 나타나면 별 한개 보여주고.. 뭐 이런식으로??
                //점수랑 관련있는 항목이 될 것 같다고 생각합니다!
            }
            if (TempScript[0] == "Hint")
            {
                //일단 힌트개수를 확인하는게 필요할 것 같음
                //이에 따라 잠긴 동그라미 수가 바뀌겠지..??
                //그리고 푼 힌트 수 세야할 것 같고
                // 힌트의 경우 
                //1: 힌트라능힌트라능
                //2: 힌트라능?힌트라능!
                //3: 힌트라능!!!!!!!!!! 이제 못풀면 넌 빠가
                //이런 식으로 주어지면 이를 파싱해서 보여주는게 필요할 거 같음!
                //힌트는 오버레이로 보여지면 좋을 것 같다고 생각...!
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
            //여기서 SpeakerName을 메인 퀘스트 #1 같은 식으로 나타내도 되지 않을까 생각합니다. riddle은 null인건 퀘스트 설명 상황이니까?
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
    //이것도 수수께끼 끝나면 이동해야하니까...
    void Shift(int Phase, int Scene)
    {
        pos++;
    }
    //Speaker의 Sprite를 소환하는 Method입니다.
    void Set_SpeakerSprite(int type, int ID)
    {
        pos++;
    }
    //Speaker를 강조하는지 결정하는 Method입니다(True면 하얗게, False면 약간 뒤로 물러나고, 회색빛으로)
    void Set_SpeakerFocus(int type, bool focus)
    {
        pos++;
    }

}

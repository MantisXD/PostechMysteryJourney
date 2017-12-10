using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;

public class RiddleHandler : MonoBehaviour {

    //수수께끼 정보
    public List<RiddleClass> RiddleList = new List<RiddleClass>();
    //수수께기 점수 & 힌트코인 개수
    public int Score,CoinCount;

    GameObject RiddleBackwall, RiddleButton, RiddleInfo, RiddleText;
    GameObject RiddleBackground, Answer;

    int CurrentRiddle;
    bool IsScriptLoaded;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //수수께끼와 관련된 Object를 찾습니다.
    public void Linker()
    {
        RiddleBackwall = GameObject.Find("RiddleBackwall");
        RiddleButton = GameObject.Find("RiddleButton");
        RiddleInfo = GameObject.Find("RiddleInfo");
        RiddleText = GameObject.Find("RiddleText");
        RiddleBackground = GameObject.Find("RiddleBackground");

        //수수꼐끼 관련 UI를 꺼준다.
        RiddleBackwall.SetActive(false);
        RiddleButton.SetActive(false);
        RiddleInfo.SetActive(false);
        RiddleText.SetActive(false);
        RiddleBackground.SetActive(false);

    }

    //수수께끼를 시작하지 (스크립트에서 불러온거니? 아니면 사용자가 이미 푼 것을 또 불러오는거니?)
    public void RiddleStart(int Number, bool Value)
    {
        //기존 UI를 격리시킨다
        RiddleBackwall.SetActive(true);
        //수수꼐끼 관련 UI를 켜준다.
        RiddleButton.SetActive(true);
        RiddleInfo.SetActive(true);
        RiddleText.SetActive(true);
        RiddleBackground.SetActive(true);
        //수수께끼의 기본 정보를 보여준다
        RiddleInfo.SetActive(true);
        RiddleInfo.transform.Find("RiddleName").GetComponent<Text>().text = RiddleList[Number].Name;
        RiddleInfo.transform.Find("LeftScore").GetComponent<Text>().text = RiddleList[Number].LeftScore.ToString();
        //수수께끼를 보여준다.
        RiddleText.transform.Find("RiddleName").GetComponent<Text>().text = RiddleList[Number].Name;
        RiddleText.transform.Find("Riddle").GetComponent<Text>().text = RiddleList[Number].Riddle;
        //기본 정보를 3초에 거쳐 꺼지게 만든다.
        StartCoroutine(InfoShutdown());

        //수수께끼의 Resign 버튼과 Answer버튼에게 IsScriptLoaded를 전해준다.
        CurrentRiddle = Number;
        IsScriptLoaded = Value;
        //현재 수수께끼 번호를 저장한다
    }


    public void ToggleText()
    {
        RiddleText.SetActive(!RiddleText.activeSelf);
    }
    IEnumerator RiddleShutDown()
    {
        for (float f = 1.0f; f >= 0; f -= 0.1f)
        {
            Color c = RiddleInfo.GetComponent<Image>().color;
            c.a = f;
            RiddleButton.GetComponent<Image>().color = c;
            RiddleInfo.GetComponent<Image>().color = c;
            RiddleBackground.GetComponent<Image>().color = c;
            c.a = 1 - f;
            RiddleBackwall.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.1f);
        }
        RiddleBackwall.SetActive(false);
        RiddleButton.SetActive(false);
        RiddleInfo.SetActive(false);
        RiddleText.SetActive(false);
        RiddleBackground.SetActive(false);
    }
    IEnumerator InfoShutdown()
    {
        for (float f = 1.0f; f >= 0; f -= 0.03f)
        {
            Color c = RiddleInfo.GetComponent<Image>().color;
            c.a = f;
            RiddleInfo.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.1f);

        }
        RiddleInfo.SetActive(false);
    }
    public void PointDeducer()
    {
        RiddleList[CurrentRiddle].LeftScore = Math.Max((int)(RiddleList[CurrentRiddle].LeftScore - RiddleList[CurrentRiddle].InitScore * 0.15f), (int)(RiddleList[CurrentRiddle].InitScore * (1f - 0.15f * RiddleList[CurrentRiddle].HintList.Count)));

    }
    //GG!!
    public void RiddleResign()
    {
        //0번 이름짓기에서 실수로 눌렀으면 무시한다
        if (CurrentRiddle != 0)
        {
            StartCoroutine(RiddleShutDown());
            //수수께끼 관련 UI를 페이드아웃 시킨다.
            //점수를 깎는다
            if (!RiddleList[CurrentRiddle].IsSolved)
                PointDeducer();
            //IsScriptLoaded가 True면 ScriptHandler에게 Fail을 보낸다(받고 그곳에서 UI를 활성화한다)
            if (IsScriptLoaded)
            {
                GameObject.Find("ScriptPrinter").GetComponent<ScriptPrinter>().SetRiddleState(2);
            }
        }
    }
    //풀었구나 기특하다
    public void RiddleSuccess()
    {
        StartCoroutine(RiddleShutDown());
        //수수께끼 관련 UI를 페이드아웃 시킨다.
        //풀었음을 표기하고, 점수를 추가하고, 남은 점수를 0으로 만든다.
        RiddleList[CurrentRiddle].IsSolved = true;
        Score += RiddleList[CurrentRiddle].LeftScore;
        RiddleList[CurrentRiddle].LeftScore = 0;
        //IsScriptLoaded가 True면 ScriptHandler에게 Success을 보낸다
        if (IsScriptLoaded)
        {

            GameObject.Find("ScriptPrinter").GetComponent<ScriptPrinter>().SetRiddleState(3);
        }

    }
    //이것이 답입니까?
    public bool RiddleCheck(string Ans)
    {
        //이름을 지정한다.
        if(CurrentRiddle == 0)
        {
            GetComponent<ScriptHandler>().SetName(Ans);
            return true;
        }
        if(RiddleList[CurrentRiddle].Answer == Ans)
        {
            RiddleSuccess();
            return true;
        }
        else
        {
            PointDeducer();
            return false;
        }
    }
    //수수께끼 번호, 남은 점수를 보여줍니다.
    void ShowRiddleInfo(int Number)
    {

    }

    public void RiddleDataLoad(List<RiddleClass> tempRiddleCache, int Sco,int Coin)
    {
        string path = "file:///" + Application.dataPath + "Resoruces/Riddle/";
        Score = Sco;
        CoinCount = Coin;

        //받은 RiddleData로부터 데이터를 설정한다.
        for (int i=0;i<tempRiddleCache.Count;i++)
        {
            RiddleList.Add(tempRiddleCache[i]);
        }
        //추가적인 데이터를 받는다.
        //0번 수수께끼에 대한 정보(더미)를 추가한다.
        RiddleList[0].Hint = 0;
        RiddleList[0].InitScore = 0;
        RiddleList[0].LeftScore = 0;
        RiddleList[0].Number = 0;
        RiddleList[0].IsSolved = false;

    }
}

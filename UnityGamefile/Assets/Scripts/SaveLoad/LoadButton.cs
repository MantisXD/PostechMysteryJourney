using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;

public class LoadButton : MonoBehaviour {

    int Number;
    //게임매니저
    GameObject GameManager;
    int Phase, Scene, Coin, Score;
    public Text NameText, RiddleText,Cointext;
    string Name;
    //수수께끼 정보
    List<RiddleClass> RiddleCache = new List<RiddleClass>();
    //수수께끼 프로필이 저장된 위치
    string Profilepath;
    //수수께끼 프로필을 웹에서 받아옵니다.
    WWW RiddleProfileWWW;
    bool NewGame;
	// Use this for initialization
	void Start () {
        GameManager = GameObject.Find("GameManager");
        NewGame = GameManager.GetComponent<SaveLoadHandler>().IsNewGame();
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    //데이터를 받아옵니다.
    public void GetLoadData(int Phase_data,int Scene_data,int Coin_data,int Score_data,string Name_data,List<RiddleClass> Riddle,int Num)
    {
        Number = Num;
        Phase = Phase_data;
        Scene = Scene_data;
        Coin = Coin_data;
        Score = Score_data;
        Name = Name_data;

        int ClearedRiddleCount = 0;
        //클리어한 수수께끼 개수를 셉니다(그리고 데이터를 옮기는것도 합니다)
        for (int i = 0; i < Riddle.Count; i++)
        {
            RiddleCache.Add(Riddle[i]);
            if (Riddle[i].IsSolved)
                ClearedRiddleCount++;
        }
        //받아온 데이터를 렌더합니다.
        NameText.text = Name;
        RiddleText.text = ClearedRiddleCount.ToString();
        Cointext.text = Coin.ToString();


        
    }
    //새 파일을 생성하기 위한 번호만 받아옵니다.
    public void GetNewLoadData(int Num)
    {
        Number = Num;
    }
    void StartNewGame()
    {        
        //더미 데이터를 추가합니다.
        RiddleCache.Add(new RiddleClass());
        Profilepath= "file:///" + Application.dataPath + "/Resources/Riddle/RiddleProfile.txt";
        GetRiddleProfile();
    }
    //수수께끼의 Profile을 받아와서 저장합니다.(수수께끼를 새로 Load할때만 사용합니다)
    IEnumerator GetRiddleProfile()
    {
        RiddleProfileWWW = new WWW(Profilepath);
        yield return RiddleProfileWWW;
        int c = 1;
        //Load가 끝났으면 프로필을 할당하자아아ㅏㅏㅏ
        string ProfileCache = RiddleProfileWWW.text;
        string[] Profile = ProfileCache.Split('\n');
        for(int i=0;i<Profile.Length;i++)
        {
            //주석은 걸러
            if (Profile[i].Length >= 2 && Profile[i].Substring(0, 2) == "//")
                continue;
            string[] SpecificRiddleProfile = Profile[i].Split('"');
            //이름을 할당한다.
            RiddleCache[c].Name = SpecificRiddleProfile[1];
            //점수도 할당한다.
            RiddleCache[c].InitScore = RiddleCache[c].LeftScore = int.Parse(SpecificRiddleProfile[2].Substring(1));
            c++;
        }
    }
    //새로운 파일을 만듭니다.
    public void NewLoad()
    {
        String path = Application.dataPath + "/Resources/Save/SaveFile_" + Number.ToString() + ".txt";
        StreamWriter Writer = new StreamWriter(path, false, Encoding.UTF8);
        Name = null;
        Writer.WriteLine("Name NULL");
        Phase = 1;
        Writer.WriteLine("P 1");
        Scene = 1;
        Writer.WriteLine("S 1");
        Score = 0;
        Writer.WriteLine("Score 0");
        Coin = 0;
        Writer.WriteLine("Coin 0");
        Writer.WriteLine("Riddle " + (RiddleCache.Count - 1).ToString());

        //새로운 게임을 시작하는거면 별도의 함수를 호출
        StartNewGame();
    }

    //호출되면 다짜고짜 로드를 시작합니다.
    public void Load()
    {

        if (NewGame)
        {
            String path = Application.dataPath + "/Resources/Save/SaveFile_" + Number.ToString() + ".txt";
            StreamWriter Writer = new StreamWriter(path,false,Encoding.UTF8);
            Name = null;
            Writer.WriteLine("Name NULL");
            Phase = 1;
            Writer.WriteLine("P 1");
            Scene = 1;
            Writer.WriteLine("S 1");
            Score = 0;
            Writer.WriteLine("Score 0");
            Coin = 0;
            Writer.WriteLine("Coin 0");
            Writer.WriteLine("Riddle " + (RiddleCache.Count-1).ToString());

            //기존의 데이터를 전부 다 지웁니다.
            for (int i=1;i<RiddleCache.Count;i++)
            {
                RiddleCache[i].LeftScore = RiddleCache[i].InitScore;
                RiddleCache[i].IsSolved = false;
                RiddleCache[i].Hint = 0;
                Writer.WriteLine(i.ToString() + " false " + RiddleCache[i].InitScore.ToString() + " " + RiddleCache[i].InitScore.ToString() + " 0");
            }
            Writer.Close();
            //새로운 게임을 시작하는거면 별도의 함수를 호출
            StartNewGame();
        }
        else
        {
            //수수께끼를 로드합니다.
            GameManager.GetComponent<RiddleHandler>().RiddleDataLoad(RiddleCache, Scene, Coin);
            //씬을 전환합니다.
            GameManager.GetComponent<SceneMove>().SceneShift(Scene, Phase);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
public class SaveButton : MonoBehaviour {


    string postDataURL;
    int SaveFileNum;
	// Use this for initialization
	void Start () {
		postDataURL = "http://mantisxp.xyz/game/save.php";
    }
	
    public void Save()
    {
        int Scene, Phase;
        String Name;
        List<RiddleClass> RiddleList;
        int Coin, Score;
        //ScriptHandler에게 이름,Scene,Phase를 받아온다.
        GameObject GameManager = GameObject.Find("GameManager");
        Scene = GameManager.GetComponent<ScriptHandler>().Scene;
        Phase = GameManager.GetComponent<ScriptHandler>().Phase;
        Name = GameManager.GetComponent<ScriptHandler>().PlayerName;
        //RiddleHandler에게 현재 점수와 Coin의 개수를 받아온다.
        Coin = GameManager.GetComponent<RiddleHandler>().CoinCount;
        Score = GameManager.GetComponent<RiddleHandler>().Score;
        RiddleList = GameManager.GetComponent<RiddleHandler>().RiddleList;
        //텍스트 만들고
        string Savedata;
        Savedata = "Name" + " " + "\"" + Name + "\"\n";
        Savedata += ("S" + " " + Scene.ToString() + "\n");
        Savedata += ("P" + " " + Phase.ToString() + "\n");
        Savedata += ("Score" + " " + Coin.ToString() + "\n");
        Savedata += ("Coin" + " " + Score.ToString() + "\n");
        Savedata += ("Riddle" + " " + (RiddleList.Count-1).ToString() + "\n");

        for(int i=1;i< RiddleList.Count;i++)
        {
            Savedata += i.ToString() + " " + RiddleList[i].IsSolved.ToString() + " " + RiddleList[i].LeftScore.ToString() + " " + RiddleList[i].InitScore.ToString() + " " + RiddleList[i].Hint.ToString() + "\n";
        }
        StartCoroutine(PostData("SaveFile_" + SaveFileNum.ToString() + ".txt", Savedata));
        //저장한다.
    }
    IEnumerator PostData(string Filename,string Filedata)
    {
        WWWForm form = new WWWForm();
        form.AddField("SavedataName", Filename);
        form.AddField("Savedata", Filedata);
        WWW dataPost = new WWW(postDataURL, form);

        while(!dataPost.isDone)
        {
            yield return dataPost;
        }


        if (dataPost.error != null)
            print("There was an error saving data: " + dataPost.error);
        else
            print(dataPost.text);
    }
    public void SetNum(int Num)
    {
        SaveFileNum = Num;
    }

	// Update is called once per frame
	void Update () {
		
	}
}

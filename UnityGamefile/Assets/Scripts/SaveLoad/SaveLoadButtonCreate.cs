using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using UnityEngine.UI;

//세이브로드 파일을 읽고, 그에 따른 버튼을 만들어줍니다.

public class SaveLoadButtonCreate : MonoBehaviour {

    int Scene, Phase, Score, Coin;
    string Name;

    //템플릿이랑 로드 버튼들
    public GameObject LoadButton_Prefab;
    public GameObject LoadNewButton_Prefab;
    public GameObject Cast;
    List<GameObject> LoadButtons = new List<GameObject>();

    //템플릿이랑 세이브 버튼들
    public GameObject SaveButton_Prefab;
    public GameObject SaveNewButton_Prefab;
    GameObject SaveCast;//Handler에서 받아옵니다
    List<GameObject> SaveButtons = new List<GameObject>();

    //버튼 크기
    public float ButtonHeight;

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //언제 호출하니

    public void CreateSaveButton(GameObject Cast)
    {
        SaveCast = Cast;
        //세이브 파일들의 위치입니다. Savefile_X.txt입니다.
        string path = "file:///" + Application.dataPath + "/Resources/Save/SaveFile_";
        int c = 1;
        GameObject TempSave;
        RectTransform TempRect;
        float Ratio;
        //Load해보고, 안되면 치우자
        while (true)
        {
            WWW SaveDataLink = new WWW(path + c.ToString() + ".txt");
            while (!(SaveDataLink.isDone))
            {

            }
            //에러가 떴나 (= 파일을 찾을 수 없나 = 파일이 없나)
            if (!string.IsNullOrEmpty(SaveDataLink.error))
            {

                break;
            }
            else//파일이 있구나 버튼을 만들려무나
            {

                //파일을 로드하면 버튼을 만들고
                TempSave = Instantiate(SaveButton_Prefab);
                // -> 상속관계 형성하고 
                TempSave.transform.SetParent(SaveCast.transform);
                TempRect = TempSave.GetComponent<RectTransform>();
                //-> 위치를 지정한다.
                TempRect.anchoredPosition = new Vector2(0, (-1.2f) * ButtonHeight * c);
                //크기도 지정한다.
                Ratio = TempRect.sizeDelta.y / TempRect.sizeDelta.x;
                TempRect.sizeDelta = new Vector2(0, ButtonHeight);

                TempRect.sizeDelta = new Vector2(TempRect.sizeDelta.y * Ratio, TempRect.sizeDelta.y);
                //받은 데이터를 분할한다.
                String[] SaveData = SaveDataLink.text.Split(new char[] { '\n', '\r' });
                //맨 앞에 생기는 정체불명의 텍스트를 제거한다
                SaveData[0] = SaveData[0].Substring(1);

                //버튼을 만든다.
                int SolvedRiddle = 0;
                for(int i=0;i<SaveData.Length;i++)
                {
                    String[] temp = SaveData[i].Split(' ');
                    if(temp[0] == "Name")
                    {
                        if (temp[1] != "NULL")
                            TempSave.transform.Find("Name").GetComponent<Text>().text = temp[1];
                        else
                            TempSave.transform.Find("Name").GetComponent<Text>().text = " ";
                    }
                    if(temp[0] == "Coin")
                    {
                        TempSave.transform.Find("CoinCount").GetComponent<Text>().text = temp[1];
                    }
                    if(temp[0] == "Riddle")
                    {
                        for(int j=1;i<=int.Parse(temp[1]);j++)
                        {
                            String[] tempRiddleInfo = SaveData[i + j].Split(' ');
                            if (tempRiddleInfo[1] == "true")
                                SolvedRiddle++; 
                        }
                        TempSave.transform.Find("RiddleCount").GetComponent<Text>().text = SolvedRiddle.ToString();
                    }
                }
                TempSave.GetComponent<SaveButton>().SetNum(c);
            }
            c++;
        }
        //새 버튼도 만들어 주자
        //파일을 로드하면 버튼을 만들고
        TempSave = Instantiate(SaveNewButton_Prefab);
        // -> 상속관계 형성하고 
        TempSave.transform.SetParent(SaveCast.transform);
        TempRect = TempSave.GetComponent<RectTransform>();
        //-> 위치를 지정한다.
        TempRect.anchoredPosition = new Vector2(0, (-1.2f) * ButtonHeight * c);
        Ratio = TempRect.sizeDelta.y / TempRect.sizeDelta.x;
        //크기도 지정한다.
        TempRect.sizeDelta = new Vector2(0, ButtonHeight);
        TempRect.sizeDelta = new Vector2(TempRect.sizeDelta.y * Ratio, TempRect.sizeDelta.y);
        TempSave.GetComponent<SaveButton>().SetNum(c);
        //Cast의 크기를 조절하자.
        Cast.GetComponent<RectTransform>().sizeDelta = new Vector2(0, ButtonHeight * c * 0.6f);
    }

    public void CreateLoadButton(bool NewGame)
    {
        //리소스 폴더 내 세이브 파일들의 위치입니다. Savefile_X.txt입니다.
        string path = Application.dataPath + "/Resources/Save/SaveFile_";
        int c = 1;
        StreamReader Reader;
        string SaveFileCache;
        while (File.Exists(path + c.ToString() + ".txt"))
        {
            Reader = new StreamReader(path + c.ToString() + ".txt", Encoding.UTF8);
            //파일을 로드하면 버튼을 만들고
            GameObject TempLoad = Instantiate(LoadButton_Prefab);
            // -> 상속관계 형성하고 
            TempLoad.transform.SetParent(Cast.transform);
            RectTransform TempRect = TempLoad.GetComponent<RectTransform>();
            //-> 위치를 지정한다.
            TempRect.anchoredPosition = new Vector2(0, (-1.1f) * ButtonHeight * c);
            //크기도 지정한다.
            TempRect.sizeDelta = new Vector2(0, ButtonHeight);
            TempRect.sizeDelta = new Vector2(TempRect.sizeDelta.x * 0.8f, TempRect.sizeDelta.y * 0.8f);

            //수수께끼를 저장할 리스트를 선언한다.
            List<RiddleClass> tempRiddleCache = new List<RiddleClass>();

            //데이터를 할당한다.
            while ((SaveFileCache = Reader.ReadLine()) != null)
            {
                try
                {
                    if (SaveFileCache.Length < 2 || SaveFileCache.Substring(0, 2) == "//")//주석이니?
                    {
                        continue;
                    }
                }
                catch (ArgumentOutOfRangeException e)//에러 잡기
                {
                    Debug.Log(e);
                }
                //수수께끼를 제외한 정보를 받습니다.
                if (SaveFileCache.Length >= 4 && SaveFileCache.Substring(0, 4) == "Name")
                {
                    string[] temp = SaveFileCache.Split(' ');
                    Name = temp[1];
                }
                else if (SaveFileCache[0] == 'S' && (SaveFileCache.Length < 5 || SaveFileCache.Substring(0, 5) != "Score"))
                {
                    string[] temp = SaveFileCache.Split(' ');
                    Scene = int.Parse(temp[1]);
                }
                else if (SaveFileCache[0] == 'P')
                {
                    string[] temp = SaveFileCache.Split(' ');
                    Phase = int.Parse(temp[1]);
                }
                else if (SaveFileCache.Length >= 5 && SaveFileCache.Substring(0, 5) == "Score")
                {
                    string[] temp = SaveFileCache.Split(' ');
                    Score = int.Parse(temp[1]);
                }
                else if (SaveFileCache.Length >= 4 && SaveFileCache.Substring(0, 4) == "Coin")
                {
                    string[] temp = SaveFileCache.Split(' ');
                    Coin = int.Parse(temp[1]);
                }
                else if (SaveFileCache.Length >= 6 && SaveFileCache.Substring(0, 6) == "Riddle")
                {
                    //0번 원소는 더미 데이터입니다.
                    RiddleClass Dummy = new RiddleClass
                    {
                        Number = 0
                    };
                    tempRiddleCache.Add(Dummy);
                    //수수께끼를 load합니다.
                    while ((SaveFileCache = Reader.ReadLine()) != null)
                    {
                        string[] temp = SaveFileCache.Split(' ');
                        RiddleClass tempRiddle = new RiddleClass
                        {
                            Number = int.Parse(temp[0]),
                            IsSolved = (temp[1] == "true"),
                            LeftScore = int.Parse(temp[2]),
                            InitScore = int.Parse(temp[3]),
                            Hint = int.Parse(temp[4])
                        };
                        tempRiddleCache.Add(tempRiddle);
                    }
                }

            }
            //데이터를 Load했으니 옮겨줍시다.
            TempLoad.GetComponent<LoadButton>().GetLoadData(Phase, Scene, Coin, Score, Name, tempRiddleCache,c);
            c++;
            Reader.Close();

           
        }
        //(만약 처음부터를 선택했다면 새로운 데이터 버튼도 생성해준다.   
        if (NewGame)
        {
            //파일을 로드하면 버튼을 만들고
            GameObject TempLoad = Instantiate(LoadNewButton_Prefab);
            // -> 상속관계 형성하고 
            TempLoad.transform.SetParent(Cast.transform);
            RectTransform TempRect = TempLoad.GetComponent<RectTransform>();
            //-> 위치를 지정한다.
            TempRect.anchoredPosition = new Vector2(0, (-1.1f) * ButtonHeight * c);
            //크기도 지정한다.
            TempRect.sizeDelta = new Vector2(0, ButtonHeight);
            TempRect.sizeDelta = new Vector2(TempRect.sizeDelta.x * 0.8f, TempRect.sizeDelta.y * 0.8f);
            //한개 추가요
            TempLoad.GetComponent<LoadButton>().GetNewLoadData(c);
            c++;
        }

        //버튼 다 만들었으면 Cast를 만들어준다.
        Cast.GetComponent<RectTransform>().sizeDelta = new Vector2(0, ButtonHeight * c);
    }
}

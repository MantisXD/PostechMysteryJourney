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
    public Sprite NewFileSprite;
    List<GameObject> LoadButtons = new List<GameObject>();

    //버튼 크기
    public float ButtonHeight;

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateLoadButton(bool NewGame)
    {
        //빈 세이브파일 텍스쳐를 불러옵니다
        Texture2D tempText = Resources.Load<Texture2D>("Images/Load/EmptyFile");
        NewFileSprite = Sprite.Create(tempText, new Rect(0.0f, 0.0f, tempText.width, tempText.height), new Vector2(0.5f, 0.5f));

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
                    RiddleClass Dummy = new RiddleClass();
                    Dummy.Number = 0;
                    tempRiddleCache.Add(Dummy);
                    //수수께끼를 load합니다.
                    while ((SaveFileCache = Reader.ReadLine()) != null)
                    {
                        string[] temp = SaveFileCache.Split(' ');
                        RiddleClass tempRiddle = new RiddleClass();
                        tempRiddle.Number = int.Parse(temp[0]);
                        tempRiddle.IsSolved = (temp[1] == "true");
                        tempRiddle.LeftScore = int.Parse(temp[2]);
                        tempRiddle.InitScore = int.Parse(temp[3]);
                        tempRiddle.Hint = int.Parse(temp[4]);

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
            GameObject TempLoad = Instantiate(LoadButton_Prefab);
            // -> 상속관계 형성하고 
            TempLoad.transform.SetParent(Cast.transform);
            RectTransform TempRect = TempLoad.GetComponent<RectTransform>();
            //-> 위치를 지정한다.
            TempRect.anchoredPosition = new Vector2(0, (-1.1f) * ButtonHeight * c);
            //그래픽을 변경해주고
            TempLoad.GetComponent<Image>().sprite = NewFileSprite;
            //크기도 지정한다.
            TempRect.sizeDelta = new Vector2(0, ButtonHeight);
            TempRect.sizeDelta = new Vector2(TempRect.sizeDelta.x * 0.8f, TempRect.sizeDelta.y * 0.8f);
            //한개 추가요
            c++;
        }

        //버튼 다 만들었으면 Cast를 만들어준다.
        Cast.GetComponent<RectTransform>().sizeDelta = new Vector2(0, ButtonHeight * c);
    }
}

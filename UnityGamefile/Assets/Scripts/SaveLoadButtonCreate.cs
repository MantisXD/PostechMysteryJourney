using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

//세이브로드 파일을 읽고, 그에 따른 버튼을 만들어줍니다.

public class SaveLoadButtonCreate : MonoBehaviour {



    //템플릿이랑 로드 버튼들
    public GameObject LoadButton_Prefab;
    public GameObject Cast;
    List<GameObject> LoadButtons = new List<GameObject>();

    //버튼 크기
    public float ButtonHeight;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateLoadButton()
    {
        //리소스 폴더 내 세이브 파일들의 위치입니다. Savefile_X.txt입니다.
        String path = "Save/SaveFile_";
        int c = 1;
        TextAsset tempSaveFile;
        while((tempSaveFile = (TextAsset)Resources.Load(path+c.ToString(),typeof(TextAsset))) != null)
        {
            Debug.Log(path + c.ToString());
            //파일을 로드하면 버튼을 만들고
            GameObject TempLoad = Instantiate(LoadButton_Prefab);
            // -> 상속관계 형성하고 
            TempLoad.transform.SetParent(Cast.transform);

            RectTransform TempRect = TempLoad.GetComponent<RectTransform>();
            //-> 위치를 지정한다.
            TempRect.anchoredPosition = new Vector2(0, (-1) * ButtonHeight * c);
            //크기도 지정한다.
            TempRect.sizeDelta = new Vector2(0, ButtonHeight);
            TempRect.sizeDelta = new Vector2(TempRect.sizeDelta.x * 0.8f, TempRect.sizeDelta.y * 0.8f);
            //데이터를 넘겨준다.
            Debug.Log(tempSaveFile.text);
            TempLoad.GetComponent<LoadButton>().GetLoadData(tempSaveFile.text);
            c++;
        }
        Cast.GetComponent<RectTransform>().sizeDelta = new Vector2(0,  ButtonHeight * c);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RiddleHintManagement : MonoBehaviour {

    List<string> Hint;
    GameObject[] HintButton = new GameObject[4];
    Sprite OpenHintSprite, LockHintSprite, DisableHintSprite;
    // Use this for initialization
    public GameObject HintText;
	void Start () {
        
    }
    IEnumerator ImageFetcher(string path,int OpenLevel)
    {
        WWW ImageLink;
        Texture2D temp;
        ImageLink = new WWW(path + "/OpenedHint.png");
        while(!(ImageLink.isDone))
        {
            yield return null;
        }
        temp = ImageLink.texture;
        OpenHintSprite = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), new Vector2(0.5f, 0.5f));

        ImageLink = new WWW(path + "/LockedHint.png");
        while (!(ImageLink.isDone))
        {
            yield return null;
        }
        temp = ImageLink.texture;
        LockHintSprite = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), new Vector2(0.5f, 0.5f));

        ImageLink = new WWW(path + "/DisabledHint.png");
        while (!(ImageLink.isDone))
        {
            yield return null;
        }
        temp = ImageLink.texture;
        DisableHintSprite = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), new Vector2(0.5f, 0.5f));

        int i;
        //버튼을 찾자
        HintButton[0] = GameObject.Find("RiddleHint1");
        HintButton[1] = GameObject.Find("RiddleHint2");
        HintButton[2] = GameObject.Find("RiddleHint3");
        HintButton[3] = GameObject.Find("RiddleHint4");
        for (i = 0; i < Hint.Count; i++)
        {
            if (OpenLevel - 1 >= i)
                HintButton[i].GetComponent<RiddleHint>().Assign(1, Hint[i], OpenHintSprite, LockHintSprite, DisableHintSprite);
            else
                HintButton[i].GetComponent<RiddleHint>().Assign(0, Hint[i], OpenHintSprite, LockHintSprite, DisableHintSprite);
        }
        for (; i < 4; i++)
        {
            HintButton[i].GetComponent<RiddleHint>().Assign(2, "NULL", OpenHintSprite, LockHintSprite, DisableHintSprite);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetHint(string Text, int Num)
    {
        HintText.SetActive(true);
        HintText.transform.Find("HintName").GetComponent<Text>().text = "Hint #" + Num.ToString();
        HintText.transform.Find("Hint").GetComponent<Text>().text = Text;
    }

    public void GetHint(List<string> HList,int OpenLevel)
    {
        Hint = HList;
        int i;
        string path = "file:///" + Application.dataPath + "/Resources/Images/Riddle";
        StartCoroutine(ImageFetcher(path,OpenLevel));
       
      
    }
}

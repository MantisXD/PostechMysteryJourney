using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnswerInputButtonScript : MonoBehaviour {

    public InputField Answerfield;
    GameObject GameManager;
	// Use this for initialization
	void Start () {
        Answerfield.text = " ";
        GameManager = GameObject.Find("GameManager");
	}

    public void ToggleText()
    {
        GameManager = GameObject.Find("GameManager");
        GameManager.GetComponent<RiddleHandler>().ToggleText();
    }
	//답을 체크해보자
    public void CheckAnswer()
    {
        GameManager = GameObject.Find("GameManager");
        Answerfield = GameObject.Find("AnswerInputField").GetComponent<InputField>();
        if (GameManager.GetComponent<RiddleHandler>().RiddleCheck(Answerfield.text))
            //맞았어!
        {
            Answerfield.text = " ";
            Answerfield.placeholder.GetComponent<Text>().text = "Correct!";
        }
        else//틀렸어!
        {
            Answerfield.text = " ";
            Answerfield.placeholder.GetComponent<Text>().text = "Wrong Answer, Point for this Riddle will be deducted";
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}

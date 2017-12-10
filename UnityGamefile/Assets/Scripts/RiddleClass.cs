using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleClass {

    public int Number, LeftScore, Hint, InitScore;
    public bool IsSolved;
    public string Name;
    public string Riddle;
    public string Answer;
    public List<string> HintList;

    public Sprite RiddleSprite;
	// Use this for initialization
	void Start () {
        HintList = new List<string>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

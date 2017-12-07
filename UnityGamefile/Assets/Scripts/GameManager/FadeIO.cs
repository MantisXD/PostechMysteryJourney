using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIO : MonoBehaviour {

    Animator ScreenTransition;

	// Use this for initialization
	void Start () {
        Linker();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Transistion()
    {
        //호출되면 Fadeout을 호출하고 뒤이어 Fadein도 호출한다.
        //Fadeout 애니메이션이 끝나면 바로 Fadein으로 넘어간다.
        FadeOut();
        Invoke("FadeIn", 0.8f);
    }

    public void FadeOut()
    {
        ScreenTransition.SetBool("FadeOut", true);
        ScreenTransition.SetBool("FadeIn", false);
    }
    public void FadeIn()
    {
        ScreenTransition.SetBool("FadeOut", false);
        ScreenTransition.SetBool("FadeIn", true);
        ShutDown();   
    }
    public void ShutDown()
    {
        Invoke("TurnOff", 1.2f);
    }
    private void TurnOff()
    {
        this.gameObject.SetActive(false);
    }

    public void Linker()
    {
        //Foreground의 Animator Component를 가져온다
        ScreenTransition = GameObject.Find("Foreground").GetComponent<Animator>();
    }
}

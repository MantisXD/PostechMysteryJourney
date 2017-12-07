using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIO : MonoBehaviour {

    Image BlackScreen;
    // Use this for initialization
    private void Awake()
    {
        Linker();
    }
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FadeIn()
    {
        StartCoroutine("In");
    }
    public void FadeOut()
    {
        StartCoroutine("Out");
    }

    public void Transistion()
    {
        //호출되면 Fadeout을 호출하고 뒤이어 Fadein도 호출한다.
        //Fadeout 애니메이션이 끝나면 바로 Fadein으로 넘어간다.
        StartCoroutine("Out");
        Invoke("In", 0.8f);
    }

    //매 프레임마다 호출되며, yield return 문을 만나면 모든 진행상황을 유지한 채 다음 프레임에서 호출됩니다(yield return문에서 시작합니다)
    IEnumerator In()
    {
        for(float f = 1.1f; f >= 0; f -= 0.05f)
        {
            Color c = BlackScreen.color;
            c.a = f;
            BlackScreen.color = c;
            yield return null;
        }
    }

     IEnumerator Out()
    {
        for (float f = 0f; f <= 1.1f; f += 0.05f)
        {
            Color c = BlackScreen.color;
            Debug.Log(c.a);
            c.a = f;
            BlackScreen.color = c;
            yield return null;
        }
    }

    public void ShutDown()
    {
        Invoke("TurnOff", 1f);
    }
    private void TurnOff()
    {
        this.gameObject.SetActive(false);
    }

    public void Linker()
    {
        //Foreground의 Animator Component를 가져온다
        BlackScreen = GameObject.Find("Foreground").GetComponent<Image>();

    }
}

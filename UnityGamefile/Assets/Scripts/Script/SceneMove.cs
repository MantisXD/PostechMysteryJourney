using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMove : MonoBehaviour {

    GameObject BlackScreen;
    int Scene, Phase;
    public void SceneShift(int S = 1, int P = 1)
    {
        //페이드 아웃 걸어주고
        BlackScreen.SetActive(true);
        BlackScreen.GetComponent<FadeIO>().FadeOut();
        //페이드 아웃 끝날때 쯤 씬을 넘긴다.(S와 P 둘 다 0이면 새로운 씬으로 넘어간다)
        if (S == 0 && P == 0)
            Invoke("TitleToNew", 2.0f);
        else
            Invoke("TitleToMain", 2.0f);
        //넘어갈 씬 지정
        Scene = S;
        Phase = P;
    }
    void TitleToMain()
    {
        StartCoroutine(LoadMainScene(Scene,Phase));
    }
    void TitleToNew()
    {
        StartCoroutine(LoadNewScene());
    }
    IEnumerator LoadNewScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2);

        while (!asyncLoad.isDone)
        {

            yield return null;
        }
        GetComponent<ScriptHandler>().Linker(0, 0);
        GetComponent<SaveLoadHandler>().Linker();
    }
    IEnumerator LoadMainScene(int S=1,int P=1)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        while (!asyncLoad.isDone)
        {

            yield return null;
        }
        GetComponent<ScriptHandler>().Linker(S,P);
    }

    // Use this for initialization
    void Start () {
        BlackScreen = GameObject.Find("Foreground");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

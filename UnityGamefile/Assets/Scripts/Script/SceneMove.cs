using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMove : MonoBehaviour {

    GameObject BlackScreen;
    int Scene, Phase;
    public void SceneShift(int S=1,int P=1)
    {
        //페이드 아웃 걸어주고
        BlackScreen.SetActive(true);
        BlackScreen.GetComponent<FadeIO>().FadeOut();
        //페이드 아웃 끝날때 쯤 씬을 넘긴다.
        Invoke("TitleToMain", 2f);
        //넘어갈 씬 지정
        Scene = S;
        Phase = P;
    }
    void TitleToMain()
    {
        StartCoroutine(LoadMainScene(Scene,Phase));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMove : MonoBehaviour {

    GameObject BlackScreen;

    public void onClick()
    {
        //페이드 아웃 걸어주고
        BlackScreen.SetActive(true);
        BlackScreen.GetComponent<FadeIO>().FadeOut();
        //페이드 아웃 끝날때 쯤 씬을 넘긴다.
        Invoke("TitleToMain", 2f);
    }
    void TitleToMain()
    {
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");

        while (!asyncLoad.isDone)
        {

            yield return null;
        }
        GetComponent<ScriptHandler>().Linker(1, 1);
    }

    // Use this for initialization
    void Start () {
        BlackScreen = GameObject.Find("Foreground");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

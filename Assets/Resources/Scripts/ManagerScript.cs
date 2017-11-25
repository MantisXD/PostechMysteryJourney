using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);//이 오브젝트는 씬 이동을 해도 파괴되지 않는다
	}
	
    public void NewGame()
    {
        SceneManager.LoadSceneAsync(1);//빌드 순서 1번 씬으로 넘어간다. (a.k.a. SceneManager.LoadSync("Main") )
    }


	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
	[SerializeField] Button StartGame_Btn;
	[SerializeField] Button Settings_Btn;
	[SerializeField] Button Exit_Btn;
	
    // Start is called before the first frame update
    void Start()
    {
        StartGame_Btn.onClick.AddListener(StartNewGame);
    }

    private void StartNewGame(){

		if(ScenesManager.Instance.enabled){
			Debug.Log("SceneManager Instance is enabled");
        }
        ScenesManager.Instance.LoadNewGame();
	
	}
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	[SerializeField]
	private List<Button> AllLevelButtons;

	private void OnEnable()
	{
		int level = PlayerDataPrefs.Level;
		Debug.Log("Total level = " + level);
		for (int i = 0; i <= level; i++)
		{
			AllLevelButtons[i].interactable = true;
		}
	}

	public void OnButtonClicked(int levelIndex)
	{
		PlayerDataPrefs.ButtonClickLevel = levelIndex;
		SceneManager.LoadScene(2);
	}

	public void HomeBtn()
	{
		SceneManager.LoadScene(0);
	}
}

using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private GameObject GameOverPanel;

	[SerializeField]
	private GameObject WinPanel;

	[SerializeField]
	private GameObject LevelParent;

	[SerializeField]
	private List<GameObject> AllLevels;

	[SerializeField]
	private TextMeshProUGUI LevelIndexText;

	[SerializeField]
	private TextMeshProUGUI GameWinText;

	[SerializeField]
	private Button DayChangeBtn;

	public int currentLevelIndex;

	public Action action_GameOver;

	public Action action_GameWin;

	public float newMinEV = 5f;

	public Light sun;

	public Color dayColor;

	public Color nightColor;

	private bool isDay = true;

	private void OnEnable()
	{
		action_GameOver = (Action)Delegate.Combine(action_GameOver, new Action(GameOverPanelOpen));
		action_GameWin = (Action)Delegate.Combine(action_GameWin, new Action(WinPanelOpen));
	}

	private void OnDisable()
	{
		action_GameOver = (Action)Delegate.Remove(action_GameOver, new Action(GameOverPanelOpen));
		action_GameWin = (Action)Delegate.Remove(action_GameWin, new Action(WinPanelOpen));
	}

	private void Awake()
	{
		for (int i = 0; i < LevelParent.transform.childCount; i++)
		{
			AllLevels.Add(LevelParent.transform.GetChild(i).gameObject);
		}
		currentLevelIndex = PlayerDataPrefs.ButtonClickLevel;
		for (int j = 0; j < AllLevels.Count; j++)
		{
			if (j == currentLevelIndex)
			{
				AllLevels[j].SetActive(value: true);
			}
			else
			{
				AllLevels[j].SetActive(value: false);
			}
		}
		string text = "Level " + (currentLevelIndex + 1);
		LevelIndexText.text = text;
		Debug.Log(text);
	}

	private void Start()
	{
	}

	public void ChangeMode()
	{
		isDay = !isDay;
		if (isDay)
		{
			sun.DOColor(dayColor, 2f);
			DayChangeBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Day";
		}
		else
		{
			sun.DOColor(nightColor, 2f);
			DayChangeBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Night";
		}
	}

	public void GameOverPanelOpen()
	{
		GameOverPanel.SetActive(value: true);
		WinPanel.SetActive(value: false);
	}

	public void WinPanelOpen()
	{
		Debug.Log("Current level index = " + PlayerDataPrefs.ButtonClickLevel);
		WinPanel.SetActive(value: true);
		GameWinText.text = "Level " + (currentLevelIndex + 1) + " Completed";
		GameOverPanel.SetActive(value: false);
	}

	public void OnClickRetryBtn()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(2);
	}

	public void OnClickNextBtn()
	{
		Time.timeScale = 1f;
		currentLevelIndex = PlayerDataPrefs.ButtonClickLevel;
		currentLevelIndex++;
		PlayerDataPrefs.ButtonClickLevel = currentLevelIndex;
		Debug.Log("<color=green>Btn = " + PlayerDataPrefs.ButtonClickLevel + "</color>  level =" + PlayerDataPrefs.Level);
		if (PlayerDataPrefs.ButtonClickLevel > PlayerDataPrefs.Level)
		{
			Debug.Log("If called");
			PlayerDataPrefs.Level++;
		}
		if (currentLevelIndex == 10)
		{
			currentLevelIndex = 0;
		}
		SceneManager.LoadScene(2);
	}

	public void OnClickHomeBtn()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
	}
}

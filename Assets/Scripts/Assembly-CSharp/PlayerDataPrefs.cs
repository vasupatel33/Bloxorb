using UnityEngine;

public class PlayerDataPrefs
{
	public static int Level
	{
		get
		{
			return PlayerPrefs.GetInt("LevelIndex", 0);
		}
		set
		{
			PlayerPrefs.SetInt("LevelIndex", value);
		}
	}

	public static int ButtonClickLevel
	{
		get
		{
			return PlayerPrefs.GetInt("LevelIndexButton", 0);
		}
		set
		{
			PlayerPrefs.SetInt("LevelIndexButton", value);
		}
	}

	public static int Diamonds
	{
		get
		{
			return PlayerPrefs.GetInt("Coins", 0);
		}
		set
		{
			PlayerPrefs.SetInt("Coins", value);
		}
	}

	public static int SelectedCaptionIndex
	{
		get
		{
			return PlayerPrefs.GetInt("captainIndex", 0);
		}
		set
		{
			PlayerPrefs.SetInt("captainIndex", value);
		}
	}

	public static int SelectedShipIndex
	{
		get
		{
			return PlayerPrefs.GetInt("shipIndex", 0);
		}
		set
		{
			PlayerPrefs.SetInt("shipIndex", value);
		}
	}
}

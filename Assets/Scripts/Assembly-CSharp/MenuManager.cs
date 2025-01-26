using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public void OnPlayBtnClicked()
	{
		SceneManager.LoadScene(1);
	}

	public void OnQuitBtnClicked()
	{
		Application.Quit();
	}
}

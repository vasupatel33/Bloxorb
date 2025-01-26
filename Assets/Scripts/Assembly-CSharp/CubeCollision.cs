using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
	public List<GameObject> CollidedTileObjects;

	[SerializeField]
	private GameManager gameManager;

	public bool isAbleToWin;

	public static bool isAbleToMove;

	private bool isTriggered;

	private void OnEnable()
	{
		GameManager obj = gameManager;
		obj.action_GameOver = (Action)Delegate.Combine(obj.action_GameOver, new Action(PlayerDownWhenGameOver));
	}

	private void OnDisable()
	{
		GameManager obj = gameManager;
		obj.action_GameOver = (Action)Delegate.Remove(obj.action_GameOver, new Action(PlayerDownWhenGameOver));
	}

	public void PlayerDownWhenGameOver()
	{
		base.transform.DOMoveY(base.transform.position.y - 10f, 1f);
	}

	private void OnTriggerEnter(Collider other)
	{
		isTriggered = true;
		CollidedTileObjects.Add(other.gameObject);
		if (other.gameObject.tag == "win1block" && base.transform.position.y >= 1f)
		{
			gameManager.action_GameWin?.Invoke();
			Invoke("WinWait", 0.2f);
		}
		else if (other.gameObject.tag == "win2block" && base.transform.position.y < 1f)
		{
			gameManager.action_GameWin?.Invoke();
			Invoke("WinWait", 0.2f);
		}
	}

	private void OnTriggerStay(Collider other)
	{
	}

	public void WinWait()
	{
		Time.timeScale = 0f;
		isAbleToMove = false;
		Debug.Log("<color=yellow>Game WIN</color>");
	}

	private IEnumerator TimeScaleDefault()
	{
		yield return new WaitForSeconds(1f);
		Time.timeScale = 1f;
		Debug.Log("Time scale is 1");
	}

	private void OnTriggerExit(Collider other)
	{
		isTriggered = false;
	}

	public void CheckCubeCollision()
	{
		TileBlockManager.StartingPoint.GetComponent<BoxCollider>().enabled = true;
		if (CollidedTileObjects.Count == 2)
		{
			CollidedTileObjects.Clear();
			return;
		}
		if (!isTriggered && CollidedTileObjects.Count == 0)
		{
			Debug.Log("<color=yellow><b>Game Over</b></color>");
			gameManager.action_GameOver?.Invoke();
			isAbleToMove = false;
		}
		else if (CollidedTileObjects.Count == 1 && base.transform.position.y < 1f)
		{
			Debug.Log("<color=yellow><b>Game Over</b></color>");
			gameManager.action_GameOver?.Invoke();
			isAbleToMove = false;
		}
		CollidedTileObjects.Clear();
	}
}

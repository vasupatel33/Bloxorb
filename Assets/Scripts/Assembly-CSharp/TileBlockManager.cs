using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class TileBlockManager : MonoBehaviour
{
	private int nameCount;

	public static GameObject StartingPoint;

	[SerializeField]
	private List<GameObject> AllTileBlocks;

	[SerializeField]
	private List<Vector3> AllTileBlocksPosition;

	[SerializeField]
	private GameObject Cube;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private async void OnEnable()
	{
		StartingPoint = base.transform.GetChild(0).gameObject;
		Debug.Log(StartingPoint.name);
		StartingPoint.GetComponent<BoxCollider>().enabled = false;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).name = nameCount.ToString();
			AllTileBlocks.Add(base.transform.GetChild(i).gameObject);
			AllTileBlocksPosition.Add(AllTileBlocks[i].transform.position);
			nameCount++;
		}
		for (int j = 0; j < base.transform.childCount; j++)
		{
			base.transform.GetChild(j).localScale = new Vector3(0f, 0f, 0f);
		}
		await WaitForMove();
		Debug.Log("Completed");
		Cube.SetActive(value: true);
		ShortcutExtensions.DOMove(endValue: new Vector3(StartingPoint.transform.position.x, 1.1f, StartingPoint.transform.position.z), target: Cube.transform, duration: 0.5f).SetEase(Ease.InQuad);
		Cube.transform.DOScale(new Vector3(1f, 2f, 1f), 1f).OnComplete(delegate
		{
			CubeCollision.isAbleToMove = true;
		});
	}

	private async Task WaitForMove()
	{
		for (int i = 0; i < AllTileBlocks.Count; i++)
		{
			await Task.Delay(50);
			AllTileBlocks[i].transform.DOScale(new Vector3(1f, 0.2f, 1f), 0.1f);
			AllTileBlocks[i].transform.position = AllTileBlocksPosition[i];
		}
	}
}

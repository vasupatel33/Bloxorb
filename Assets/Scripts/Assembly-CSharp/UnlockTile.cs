using DG.Tweening;
using UnityEngine;

public class UnlockTile : MonoBehaviour
{
	[SerializeField]
	private GameObject TileToBeUnlock;

	[SerializeField]
	private GameObject TileToBeUnlock2;

	private bool isCubeTrigger;

	private void Awake()
	{
		TileToBeUnlock.SetActive(value: false);
		TileToBeUnlock2.SetActive(value: false);
	}

	private void OnTriggerEnter(Collider other)
	{
		isCubeTrigger = !isCubeTrigger;
		if (isCubeTrigger)
		{
			TileToBeUnlock.SetActive(value: true);
			TileToBeUnlock.transform.DOScale(new Vector3(1f, 0.2f, 1f), 0.7f).SetEase(Ease.OutBack);
			TileToBeUnlock.transform.DORotate(new Vector3(0f, 360f, 0f), 0.7f, RotateMode.FastBeyond360).SetEase(Ease.OutCubic);
			if (TileToBeUnlock2 != null)
			{
				TileToBeUnlock2.SetActive(value: true);
				TileToBeUnlock2.transform.DOScale(new Vector3(1f, 0.2f, 1f), 0.7f).SetEase(Ease.OutBack);
				TileToBeUnlock2.transform.DORotate(new Vector3(0f, 360f, 0f), 0.7f, RotateMode.FastBeyond360).SetEase(Ease.OutCubic);
			}
			return;
		}
		TileToBeUnlock.transform.DOScale(Vector3.zero, 0.7f).SetEase(Ease.InBack).OnComplete(delegate
		{
			TileToBeUnlock.SetActive(value: false);
		});
		TileToBeUnlock.transform.DORotate(new Vector3(0f, -360f, 0f), 0.7f, RotateMode.FastBeyond360).SetEase(Ease.InCubic);
		if (TileToBeUnlock2 != null)
		{
			TileToBeUnlock2.transform.DOScale(Vector3.zero, 0.7f).SetEase(Ease.InBack).OnComplete(delegate
			{
				TileToBeUnlock2.SetActive(value: false);
			});
			TileToBeUnlock2.transform.DORotate(new Vector3(0f, -360f, 0f), 0.7f, RotateMode.FastBeyond360).SetEase(Ease.InCubic);
		}
	}
}

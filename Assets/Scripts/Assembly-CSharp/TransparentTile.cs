using DG.Tweening;
using UnityEngine;

public class TransparentTile : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Cube") && other.transform.position.y > 1f)
		{
			Debug.Log("<color=red>Player Down</color>");
			Sequence s = DOTween.Sequence();
			s.Append(base.transform.DOMoveY(base.transform.position.y - 20f, 2f));
			s.Insert(0.1f, other.transform.DOMoveY(other.transform.position.y - 20f, 2f)).OnComplete(delegate
			{
				Object.Destroy(base.transform.gameObject);
				Object.Destroy(other.gameObject);
			});
		}
	}
}

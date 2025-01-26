using System.Collections;
using TMPro;
using UnityEngine;

public class TextBlinkAnim : MonoBehaviour
{
	public TextMeshProUGUI textMeshPro;

	public float blinkDuration = 1f;

	public Color startColor = Color.red;

	public Color endColor = Color.white;

	private Coroutine blinkCoroutine;

	private bool isEnable;

	private void Start()
	{
	}

	private void OnEnable()
	{
		isEnable = true;
		StartBlinking();
	}

	private void OnDisable()
	{
		isEnable = false;
	}

	private void StartBlinking()
	{
		if (blinkCoroutine != null)
		{
			StopCoroutine(blinkCoroutine);
		}
		blinkCoroutine = StartCoroutine(Blink());
	}

	private IEnumerator Blink()
	{
		float timer = 0f;
		bool forward = true;
		while (isEnable)
		{
			float t = timer / blinkDuration;
			Color value = (forward ? Color.Lerp(startColor, endColor, t) : Color.Lerp(endColor, startColor, t));
			textMeshPro.fontMaterial.SetColor("_OutlineColor", value);
			timer += Time.deltaTime;
			if (timer >= blinkDuration)
			{
				timer = 0f;
				forward = !forward;
			}
			yield return null;
		}
	}
}

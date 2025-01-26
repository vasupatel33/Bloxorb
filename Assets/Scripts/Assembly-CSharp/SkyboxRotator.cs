using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
	public float RotationPerSecond = 2f;

	protected void Update()
	{
		RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotationPerSecond);
	}
}

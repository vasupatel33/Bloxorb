using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubeController : MonoBehaviour
{
	public float rotationPeriod = 0.3f;

	private Vector3 scale;

	private bool isRotate;

	private float directionX;

	private float directionZ;

	private float startAngleRad;

	private float rotationTime;

	private float radius = 1f;

	private Vector3 startPos;

	private Quaternion fromRotation;

	private Quaternion toRotation;

	private Vector2 touchStart;

	public float swipeThreshold;

	private bool isSwipe;

	private bool isAbleToTurn;

	private float tiltMovement;

	[SerializeField]
	private TextMeshProUGUI swipeCountText;

	private int swipeCount;

	[SerializeField]
	private CubeCollision cubeCollision;

	private float x;

	private float y;

	private bool isAbleToClick;

	private float nextFire;

	private void Start()
	{
		swipeCount = 0;
		isAbleToClick = true;
		scale = base.transform.lossyScale;
		Debug.Log("[x, y, z] = [" + scale.x + ", " + scale.y + ", " + scale.z + "]");
	}

	private void Update()
	{
		if (EventSystem.current.currentSelectedGameObject != null)
		{
			return;
		}
		if (CubeCollision.isAbleToMove)
		{
			SwipeControl();
			x = Input.GetAxisRaw("Horizontal");
			if (x == 0f)
			{
				y = Input.GetAxisRaw("Vertical");
			}
		}
		if ((x != 0f || y != 0f) && !isRotate)
		{
			RotateAndMoveCube();
		}
	}

	public void RotateAndMoveCube()
	{
		if (isAbleToClick && CubeCollision.isAbleToMove)
		{
			isAbleToClick = false;
			swipeCount++;
			swipeCountText.text = swipeCount.ToString();
			directionX = y;
			directionZ = x;
			startPos = base.transform.position;
			fromRotation = base.transform.rotation;
			base.transform.Rotate(directionZ * 90f, 0f, directionX * 90f, Space.World);
			toRotation = base.transform.rotation;
			base.transform.rotation = fromRotation;
			setRadius();
			rotationTime = 0f;
			isRotate = true;
			StartCoroutine(WaitUntillClick());
		}
	}

	private IEnumerator WaitUntillClick()
	{
		yield return new WaitForSeconds(0.45f);
		cubeCollision.CheckCubeCollision();
		isAbleToClick = true;
	}

	private void SwipeControl()
	{
		if (Input.touchCount <= 0)
		{
			return;
		}
		Touch touch = Input.GetTouch(0);
		if (touch.phase == TouchPhase.Began)
		{
			touchStart = touch.position;
			isSwipe = true;
		}
		else
		{
			if (touch.phase != TouchPhase.Ended || !isSwipe)
			{
				return;
			}
			Vector2 vector = touch.position - touchStart;
			if (!(vector.magnitude > swipeThreshold))
			{
				return;
			}
			if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
			{
				if (vector.x < 0f)
				{
					x = -1f;
					Debug.Log("Left swipe");
					RotateAndMoveCube();
				}
				else
				{
					x = 1f;
					Debug.Log("Right swipe");
					RotateAndMoveCube();
				}
				return;
			}
			if (vector.y > 0f)
			{
				y = 1f;
				Debug.Log("Up swipe");
				RotateAndMoveCube();
			}
			if (vector.y < 0f)
			{
				y = -1f;
				Debug.Log("Down swipe");
				RotateAndMoveCube();
			}
		}
	}

	private void FixedUpdate()
	{
		if (isRotate)
		{
			rotationTime += Time.fixedDeltaTime;
			float num = Mathf.Lerp(0f, 1f, rotationTime / rotationPeriod);
			float num2 = Mathf.Lerp(0f, MathF.PI / 2f, num);
			float num3 = (0f - directionX) * radius * (Mathf.Cos(startAngleRad) - Mathf.Cos(startAngleRad + num2));
			float num4 = radius * (Mathf.Sin(startAngleRad + num2) - Mathf.Sin(startAngleRad));
			float num5 = directionZ * radius * (Mathf.Cos(startAngleRad) - Mathf.Cos(startAngleRad + num2));
			base.transform.position = new Vector3(startPos.x + num3, startPos.y + num4, startPos.z + num5);
			base.transform.rotation = Quaternion.Lerp(fromRotation, toRotation, num);
			if (num == 1f)
			{
				isRotate = false;
				directionX = 0f;
				directionZ = 0f;
				rotationTime = 0f;
			}
		}
	}

	private void setRadius()
	{
		Vector3 rhs = new Vector3(0f, 0f, 0f);
		Vector3 up = Vector3.up;
		if (directionX != 0f)
		{
			rhs = Vector3.right;
		}
		else if (directionZ != 0f)
		{
			rhs = Vector3.forward;
		}
		if ((double)Mathf.Abs(Vector3.Dot(base.transform.right, rhs)) > 0.99)
		{
			if ((double)Mathf.Abs(Vector3.Dot(base.transform.up, up)) > 0.99)
			{
				radius = Mathf.Sqrt(Mathf.Pow(scale.x / 2f, 2f) + Mathf.Pow(scale.y / 2f, 2f));
				startAngleRad = Mathf.Atan2(scale.y, scale.x);
			}
			else if ((double)Mathf.Abs(Vector3.Dot(base.transform.forward, up)) > 0.99)
			{
				radius = Mathf.Sqrt(Mathf.Pow(scale.x / 2f, 2f) + Mathf.Pow(scale.z / 2f, 2f));
				startAngleRad = Mathf.Atan2(scale.z, scale.x);
			}
		}
		else if ((double)Mathf.Abs(Vector3.Dot(base.transform.up, rhs)) > 0.99)
		{
			if ((double)Mathf.Abs(Vector3.Dot(base.transform.right, up)) > 0.99)
			{
				radius = Mathf.Sqrt(Mathf.Pow(scale.y / 2f, 2f) + Mathf.Pow(scale.x / 2f, 2f));
				startAngleRad = Mathf.Atan2(scale.x, scale.y);
			}
			else if ((double)Mathf.Abs(Vector3.Dot(base.transform.forward, up)) > 0.99)
			{
				radius = Mathf.Sqrt(Mathf.Pow(scale.y / 2f, 2f) + Mathf.Pow(scale.z / 2f, 2f));
				startAngleRad = Mathf.Atan2(scale.z, scale.y);
			}
		}
		else if ((double)Mathf.Abs(Vector3.Dot(base.transform.forward, rhs)) > 0.99)
		{
			if ((double)Mathf.Abs(Vector3.Dot(base.transform.right, up)) > 0.99)
			{
				radius = Mathf.Sqrt(Mathf.Pow(scale.z / 2f, 2f) + Mathf.Pow(scale.x / 2f, 2f));
				startAngleRad = Mathf.Atan2(scale.x, scale.z);
			}
			else if ((double)Mathf.Abs(Vector3.Dot(base.transform.up, up)) > 0.99)
			{
				radius = Mathf.Sqrt(Mathf.Pow(scale.z / 2f, 2f) + Mathf.Pow(scale.y / 2f, 2f));
				startAngleRad = Mathf.Atan2(scale.y, scale.z);
			}
		}
	}
}

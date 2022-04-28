using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour

{
	public Transform Player;


	public Camera MainCamera;


	public bool RotateWithPlayer = true;


	public void Start()
	{
		SetPosition();
		SetRotation();
	}

	void LateUpdate()
	{
		if (Player != null)
		{
			SetPosition();

			if (RotateWithPlayer && MainCamera)
			{
				SetRotation();
			}
		}
	}

	private void SetRotation()
	{
		transform.rotation =
		Quaternion.Euler(90.0f, MainCamera.transform.eulerAngles.y, 0.0f);
	}

	private void SetPosition()
	{
		var newPos = Player.position;
		newPos.y = transform.position.y;

		transform.position = newPos;
	}
}
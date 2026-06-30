using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RTSCameraController : MonoBehaviour
{
	private Vector2 _dragOrigin;
	private bool isDragging;
	private Vector3 _cameraPosition;
	[Header("Camera Settings")]
	public float cameraSpeed = 20;
	public int mouseSpeed = 20;
	public float dragSpeed = 20;
	public float screenBoundary = 50;
	private int _screenHeight;
	private int _screenWidth;

	void Start()
	{
		_cameraPosition = this.transform.position;
		_screenHeight = Screen.height;
		_screenWidth = Screen.width;
		Cursor.lockState = CursorLockMode.Confined;
	}

private void DragMovement()
	{
		if (!isDragging)
		{
			isDragging = true;

			_dragOrigin = Input.mousePosition;
			Debug.Log(_dragOrigin);
		}
		else
		{	
			Vector2 newOrigin = Input.mousePosition;
			Vector2 direction = newOrigin - _dragOrigin;
			direction.Normalize();

			_cameraPosition.x += direction.x * dragSpeed * Time.deltaTime;
			_cameraPosition.z += direction.y * dragSpeed * Time.deltaTime;
		}

	}
		
		
	void Update()
	{
		if (Input.GetMouseButton(2))
		{
			// Scroll wheel movement
			DragMovement();
		}
		else
		{
			isDragging = false;

			// WASD Keyboard Movement
			KeyboardMovement();

			// Edge Scrolling with Mouse
			MouseEdgeScroll();	
		}
		this.transform.position = _cameraPosition;
	}

	void KeyboardMovement()
	{
		if (Input.GetKey(KeyCode.W))
		{
			_cameraPosition.z += cameraSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S))
		{
			_cameraPosition.z -= cameraSpeed * Time.deltaTime;		
		}
		if (Input.GetKey(KeyCode.A))
		{
			_cameraPosition.x -= cameraSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D))
		{
			_cameraPosition.x += cameraSpeed * Time.deltaTime;		
		}
	}

	void MouseEdgeScroll()
	{
		if (Input.mousePosition.x > _screenWidth - screenBoundary)
		{
			_cameraPosition.x += mouseSpeed * Time.deltaTime;
		}
		if (Input.mousePosition.x < 0 + screenBoundary)
		{
			_cameraPosition.x -= mouseSpeed * Time.deltaTime;
		}
		if (Input.mousePosition.y > _screenHeight - screenBoundary)
		{
			_cameraPosition.z += mouseSpeed * Time.deltaTime;
		}
		if (Input.mousePosition.y < 0 + screenBoundary)
		{
			_cameraPosition.z -= mouseSpeed * Time.deltaTime;
		}
	}
}


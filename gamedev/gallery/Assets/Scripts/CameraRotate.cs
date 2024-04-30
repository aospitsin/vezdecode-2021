using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
	public VariableJoystick variableJoystick;
	public static CameraRotate cameraRotate;
	
	[SerializeField]
	private float rotYSpeed = 3f;
	[SerializeField]
	private float rotXSpeed = 3f;
	[SerializeField][Header("Чувствительность к касанию пальца")]
	private float sens = 5f;
	
	
	private float rotY, tmpRotY, rotX, tmpRotX;
	private float widthResolution;
	private GameObject player;
	private GameObject cam;
	private Touch touch; 

	void Start()
	{
		cameraRotate = this;
		player = transform.parent.gameObject;
		cam = gameObject;
		rotY = transform.eulerAngles.y;
		tmpRotY = rotY;
		widthResolution = Screen.width;
	}

#if UNITY_EDITOR_WIN
	//Для осмотра карты на компьютере
	private void Update()
	{
		if (Input.GetMouseButton(1) && Input.touchCount == 0)
		{
			//Вращение тела игрока
			tmpRotY += Input.GetAxis("Mouse X") * 5f;
			//Вертикальное вращение камеры
			tmpRotX += Input.GetAxis("Mouse Y") * 5f;
			
			ChangeAngle();
		}
	}
#endif

	void FixedUpdate()
	{
		if (variableJoystick.AxisOptions == AxisOptions.Both && Input.touchCount > 1)
		{
			Debug.Log("+++");
			Touch[] touches = Input.touches;                          
			if (touches[0].position.x > 500f)       
				touch = touches[0];                                   
			else if (touches[1].position.x > 500f)  
				touch = touches[1];
			
			tmpRotY += touch.deltaPosition.x * Time.deltaTime * rotYSpeed;
			tmpRotX -= touch.deltaPosition.y * Time.deltaTime * rotXSpeed; 
		}
		else if (Input.touchCount == 1 && variableJoystick.Vertical == 0 && variableJoystick.Horizontal == 0)
		{
			touch = Input.touches[0];
			if (touch.deltaPosition.magnitude > sens)                      
			{                                                              
				tmpRotY += touch.deltaPosition.x * Time.deltaTime * rotYSpeed;    
				tmpRotX -= touch.deltaPosition.y * Time.deltaTime * rotXSpeed;    
			}                                                              
		}
		ChangeAngle();
	}                  
	
	void ChangeAngle()
	{    
		rotY = Mathf.Lerp(rotY, tmpRotY, 0.1f);
		player.transform.rotation = Quaternion.Euler(0, rotY, 0);
		
		tmpRotX = Mathf.Clamp (tmpRotX, -30f, 60f); 
		rotX = Mathf.LerpAngle(rotX, tmpRotX, 0.1f);
		cam.transform.rotation = Quaternion.Euler(rotX, rotY, 0f);
	}
}


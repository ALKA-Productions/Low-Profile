using UnityEngine;
using System.Collections;

public class cameraMovement : MonoBehaviour {
	public struct BoxLimit {
		public float LeftLimit;
		public float RightLimit;
		public float TopLimit;
		public float ForwardLimit;
		public float BackLimit;
	}		
	
	public static BoxLimit cameraLimit  = new BoxLimit();
	
	public int border = 20;
	public float speed = 20;
	public int shiftBonus = 20;
	public float zoomFactor = 10.0f;
	public bool  BoundaryBug ;

	private int zero = 0;
	private bool shiftCheck;
	private bool WASDCheck;
	public bool OverBoundaries;
	Vector3 move_forward = new Vector3(0, 0, 1);    //(0,0,1)
	Vector3 move_back = new Vector3(0, 0, -1); //(0,0,-1)
	Vector3 move_left = new Vector3(-1, 0,0);//(-1,0,0)
	Vector3 move_right = new Vector3(1, 0, 0);//(1,0,0)
	Vector3 move_up  = new Vector3 (0,1,0);
	Vector3 move_down  = new Vector3 (0,-1,0);

	void Start(){
	cameraLimit.RightLimit = 2000f;	
	cameraLimit.ForwardLimit = 2000;
	cameraLimit.BackLimit = 0f;
	cameraLimit.LeftLimit = 0f;		
	}
	void Update ()
	{
		mouseCamMove();
		wasdCamMove();
		shiftBool();
		wasdBool();
		scrollMove();
		ChecksIfCameraIsWithinBoundariesX();
		ChecksIfCameraIsWithinBoundariesZ();
		BoundaryBugFix();
	}
	void shiftBool(){
				if(Input.GetKey(KeyCode.LeftShift)){
			shiftCheck = true;
		}else{
			shiftCheck = false;
		}
	}
	void wasdBool(){
				if((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D))){			
		WASDCheck = true;
		}
		if((Input.GetKeyUp(KeyCode.W)) || (Input.GetKeyUp(KeyCode.A)) || (Input.GetKeyUp(KeyCode.S)) || (Input.GetKeyUp(KeyCode.D))){
		WASDCheck = false;
		}
	}
	void mouseCamMove(){
		if ((Input.mousePosition.y >= Screen.height - border) && (shiftCheck == false) && (WASDCheck == false) && (OverBoundaries == false)&& (BoundaryBug == false)) {
			transform.position += move_forward * Time.deltaTime * speed;
		}
		if((Input.mousePosition.y >= Screen.height - border) && (shiftCheck == true)&& (WASDCheck == false)&& (OverBoundaries == false)&& (BoundaryBug == false)){		
			transform.position += move_forward * Time.deltaTime * (speed + shiftBonus);
		}
		
		if ((Input.mousePosition.y <= border) && (shiftCheck == false)&& (WASDCheck == false)&& (OverBoundaries == false)&& (BoundaryBug == false)){
			transform.position += move_back * Time.deltaTime * speed ;
		}
		
		if ((Input.mousePosition.y <= border) && (shiftCheck == true)&& (WASDCheck == false)&& (OverBoundaries == false)&& (BoundaryBug == false)){
			transform.position += move_back * Time.deltaTime *(speed + shiftBonus);
		}
		
		if((Input.mousePosition.x <= border)&& (shiftCheck == false)&& (WASDCheck == false)&& (OverBoundaries == false)&& (BoundaryBug == false)){	
			transform.position += move_left * Time.deltaTime * speed;
		}
		
		if ((Input.mousePosition.x <= border)&& (shiftCheck == true)&& (WASDCheck == false)&& (OverBoundaries == false)&& (BoundaryBug == false)){	
			transform.position += move_left * Time.deltaTime * (speed + shiftBonus);
		}	
		if((Input.mousePosition.x >= Screen.width - border) && (shiftCheck == true)&& (WASDCheck == false)&& (OverBoundaries == false)&& (BoundaryBug == false)){
			transform.position += move_right * Time.deltaTime * (speed + shiftBonus);
		}
		if((Input.mousePosition.x >= Screen.width - border) && (shiftCheck == false)&& (WASDCheck == false)&& (OverBoundaries == false)&& (BoundaryBug == false)){
			transform.position += move_right * Time.deltaTime * speed;
		}
	}
	void wasdCamMove(){
		if((Input.GetKey(KeyCode.W)) && (WASDCheck == true) && (shiftCheck == false)&& (OverBoundaries == false) && (BoundaryBug == false)){
			transform.position += move_forward * Time.deltaTime * speed;
		}
		if((Input.GetKey(KeyCode.W)) && (WASDCheck == true) && (shiftCheck == true)&& (OverBoundaries == false)&& (BoundaryBug == false)){
			transform.position += move_forward * Time.deltaTime * (speed + shiftBonus);
		}
		if((Input.GetKey(KeyCode.S)) && (WASDCheck == true) && (shiftCheck == false)&& (OverBoundaries == false)&& (BoundaryBug == false)){
			transform.position += move_back * Time.deltaTime * speed;
		}
		if((Input.GetKey(KeyCode.S)) && (WASDCheck == true) && (shiftCheck == true)&& (OverBoundaries == false)&& (BoundaryBug == false)){
			transform.position += move_back * Time.deltaTime * (speed + shiftBonus);
		}
		if((Input.GetKey(KeyCode.A)) && (WASDCheck == true) && (shiftCheck == false)&& (OverBoundaries == false)&& (BoundaryBug == false)){
			transform.position += move_left * Time.deltaTime * speed;
		}
		if((Input.GetKey(KeyCode.A)) && (WASDCheck == true) && (shiftCheck == true)&& (OverBoundaries == false)&& (BoundaryBug == false)){
			transform.position += move_left * Time.deltaTime * (speed + shiftBonus);
		}
		if((Input.GetKey(KeyCode.D)) && (WASDCheck == true) && (shiftCheck == false)&& (OverBoundaries == false)&& (BoundaryBug == false)){
			transform.position += move_right * Time.deltaTime * speed;
		}
		if((Input.GetKey(KeyCode.D)) && (WASDCheck == true) && (shiftCheck == true)&& (OverBoundaries == false)&& (BoundaryBug == false)){
			transform.position += move_right * Time.deltaTime * (speed + shiftBonus);
		}
	}
	void scrollMove(){
	if(Input.GetAxis("Mouse ScrollWheel") < zero){
	transform.position += move_up * zoomFactor * Time.deltaTime;
		}	
	if(Input.GetAxis("Mouse ScrollWheel") > zero){
	transform.position += move_down * zoomFactor * Time.deltaTime;
		}
	}
	void ChecksIfCameraIsWithinBoundariesX(){
		
		if((Camera.main.transform.position.x > cameraLimit.RightLimit - 5)&&(BoundaryBug == false)){
			OverBoundaries = false;
			if(Input.GetKey(KeyCode.A)){
				transform.position += move_left * Time.deltaTime * speed;
			}
				if(Input.GetKey(KeyCode.W)){
					transform.position += move_forward * Time.deltaTime * speed;
			}			
				if(Input.GetKey(KeyCode.S)){
					transform.position += move_back * Time.deltaTime * speed;
			}
		}
		
		if(Camera.main.transform.position.x < cameraLimit.RightLimit - 5){
			OverBoundaries = false;
				}
		
		
		if((Camera.main.transform.position.x < cameraLimit.LeftLimit + 5)&&(BoundaryBug == false)){
				OverBoundaries = true;
				
				if(Input.GetKey(KeyCode.D)){
				transform.position += move_right * Time.deltaTime * speed;
			}
				if(Input.GetKey(KeyCode.W)){
					transform.position += move_forward * Time.deltaTime * speed;
			}			
				if(Input.GetKey(KeyCode.S)){
					transform.position += move_back * Time.deltaTime * speed;
			}
			if(Camera.main.transform.position.x > cameraLimit.RightLimit - 5){
			OverBoundaries = false;
				}
	
		
		}
	}	
	void ChecksIfCameraIsWithinBoundariesZ(){
			if((Camera.main.transform.position.z > cameraLimit.ForwardLimit - 5)&&(BoundaryBug == false)){
			OverBoundaries = true;
			if(Input.GetKey(KeyCode.A)){
				transform.position += move_left * Time.deltaTime * speed;
				}
				if(Input.GetKey(KeyCode.D)){
					transform.position += move_right * Time.deltaTime * speed;
				}			
				if(Input.GetKey(KeyCode.S)){
					transform.position += move_back * Time.deltaTime * speed;
				}
			
			if(Camera.main.transform.position.z < cameraLimit.ForwardLimit - 5){
			OverBoundaries = false;
			}
			if((Camera.main.transform.position.z < cameraLimit.BackLimit + 5)&&(BoundaryBug == false)){
				OverBoundaries = true;
			if(Input.GetKey(KeyCode.A)){
				transform.position += move_left * Time.deltaTime * speed;
				}
				if(Input.GetKey(KeyCode.D)){
					transform.position += move_right * Time.deltaTime * speed;
				}			
				if(Input.GetKey(KeyCode.S)){
					transform.position += move_back * Time.deltaTime * speed;
				}
			
			if(Camera.main.transform.position.z > cameraLimit.BackLimit + 5){
			OverBoundaries = false;
		}	
	}	
	

}
	
}
	void BoundaryBugFix(){
		if((Camera.main.transform.position.x > 1995) && (Camera.main.transform.position.z > 1995)){
			BoundaryBug = true;
			if(Input.GetKey(KeyCode.A)){
			transform.position += move_left * Time.deltaTime * speed;
				}
			if(Input.GetKey(KeyCode.S)){
			transform.position += move_back * Time.deltaTime * speed;
				}
		}else{
			BoundaryBug = false;
		}
		if((Camera.main.transform.position.x < 5) && (Camera.main.transform.position.z > 1995)){
			BoundaryBug = true;
			if(Input.GetKey(KeyCode.D)){
			transform.position += move_right * Time.deltaTime * speed;
				}
			if(Input.GetKey(KeyCode.S)){
			transform.position += move_back * Time.deltaTime * speed;
				}
		}else{
			BoundaryBug = false;
		}
	}	

}		
 

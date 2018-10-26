using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBall : MonoBehaviour {

	private Vector3 posBall;
	private float count;
	// Use this for initialization
	void Start () {
		
		count=0;
		float x = gameObject.transform.position.x;
		float y = gameObject.transform.position.y;
		float z = gameObject.transform.position.z;
		posBall = new Vector3(x,y,z);
		
	}
	
	// Update is called once per frame
	void Update () {
		count=count+0.05f;
		//circular animation

		posBall.x = posBall.x+((Mathf.Cos (count))/4);
		posBall.z = posBall.z+((Mathf.Sin (count))/4);
		gameObject.transform.position = posBall; //rotation around tower
		gameObject.transform.Rotate(Vector3.up*4);//rotation around itself
		
	}
}

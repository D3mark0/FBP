using UnityEngine;
using System.Collections;

public class cameraMove : MonoBehaviour {
	public float dampTime = 0.5f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	Camera cam;

	void Start()
	{
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target)
		{
			Vector3 point = cam.WorldToViewportPoint(new Vector3(target.position.x, target.position.y+0.5f,target.position.z));
			Vector3 delta = new Vector3(target.position.x, target.position.y+0.5f,target.position.z) - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;

			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
		
	}
}
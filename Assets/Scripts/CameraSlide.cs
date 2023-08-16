using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlide : MonoBehaviour {

	public float _speed;
	private Vector3 hold;
	public GameObject _focusobject;
	// Use this for initialization
	void Start () {
		hold = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//slide ();
		follow();
	}
	void follow()
	{
		if (Mathf.Abs (gameObject.transform.position.x - _focusobject.transform.position.x) > .5f) {
			float dir = Mathf.Abs (gameObject.transform.position.x - _focusobject.transform.position.x) / (gameObject.transform.position.x - _focusobject.transform.position.x);
			_speed += 0.0003f;
			if (_speed == 0) {
				_speed = 0.001f;}
			_speed = Mathf.Min (6f, _speed);
			hold += new Vector3 (Mathf.Round (_speed * 10 * -dir) / 10f, 0, 0);
			gameObject.transform.position += new Vector3 (Mathf.Round ((hold.x - gameObject.transform.position.x) * 10) / 10f, 0, 0);
		} else {
			if (_speed > 0) {
				_speed -= 0.5f;
				_speed = Mathf.Max (0, _speed);
			}
		}
	}
	void slide ()
	{
		hold += new Vector3 (_speed, 0, 0);
		if (Mathf.Abs(hold.x - gameObject.transform.position.x) > .1f) {
			float temp = Mathf.Round((hold.x - gameObject.transform.position.x)*10)/10f;
			gameObject.transform.position += new Vector3(temp,0,0);
		}
	}
}

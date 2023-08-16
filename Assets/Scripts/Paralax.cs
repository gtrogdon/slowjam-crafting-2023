using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Internal;

public class Paralax : MonoBehaviour {

	public Camera _mainview;//if the camera you use is not Camera.main you need to fix it here in Start()!
	public Vector3 _focusposition;
	private Vector3 _startposition;
	private Vector3 _camstartposition;
	public int _paralaxshift;

	// Use this for initialization
	void Start () {
		_mainview = Camera.main;
		_startposition = gameObject.transform.position;
		_camstartposition = _mainview.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//divide camera location by shift
		float camcomp = (_mainview.gameObject.transform.position.x - _focusposition.x) / _paralaxshift;
		float selfcomp = _startposition.x - gameObject.transform.position.x;

		//if gameobject position is not equal to start position + shift result change position
		if (Mathf.Abs (camcomp) != Mathf.Abs (selfcomp)) {
			float hold = camcomp * 10;
			hold = Mathf.Round (hold);
			camcomp = hold / 10;
			gameObject.transform.position = _startposition + new Vector3 (camcomp, 0, 0);
			//Debug.Log (gameObject.transform.position.x);
		}
	}
}

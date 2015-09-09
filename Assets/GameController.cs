using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	GameObject[] _holes = new GameObject[9];
	public GameObject _goal;

	public Transform cam;

	GameObject _activeGoal = null;

	float _repeatTime = 2.0f;
	
	void Start () {

		int _cnum = transform.childCount; 

		for (int i = 0; i < _cnum; i++){
			_holes[i] = transform.GetChild(i).gameObject;
			_holes[i].name = "Hole" + i;
		}

		Invoke("CreateRandom", _repeatTime);
	}

	void Update () {
		Debug.DrawLine(cam.position, cam.transform.forward, Color.red);

		RaycastHit _goalHit = new RaycastHit ();
		if (Physics.Raycast (cam.position, cam.transform.forward, out _goalHit)) {
//			Debug.DrawRay (_goalHit.point, Vector3.up, Color.white);
//			Debug.Log("Hit: " + _goalHit.transform.name);
			if (_goalHit.transform.tag == "GoalTag"){
				DestroyGoal();
			}
		}


	}
	
	public void DestroyGoal(){

		CreateRandom();

		_repeatTime -= 0.1f;

		if (_repeatTime < 0.0f) {_repeatTime = 0.0f;}

		if (_repeatTime == 0){
			_repeatTime = 2.0f;
			Debug.Log("Win!");
			CancelInvoke();
			Invoke("CreateRandom", _repeatTime);
			return;
		}

		CancelInvoke();
		Invoke("CreateRandom", _repeatTime);
	}

	void CreateRandom(){

		GameObject _tmp;

		if (_activeGoal != null){
			_tmp = _activeGoal;
		}
		else{
			_tmp = (GameObject)Instantiate(_goal as Object);
		}

		int _ran = (int)Random.Range(0, 9);

		_tmp.transform.SetParent(_holes[_ran].transform);
		_tmp.transform.localScale = new Vector3(1.5f , 1.5f, 1.5f);
		_tmp.transform.localPosition = Vector3.zero;

		_activeGoal = _tmp;

		Invoke("CreateRandom", _repeatTime);
	}
}

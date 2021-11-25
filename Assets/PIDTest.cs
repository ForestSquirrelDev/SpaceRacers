using UnityEngine;
using System.Collections;

public class PIDTest : MonoBehaviour {

	//public PID pid;
	public float speed;
	public Transform actual;
	public Transform setpoint;

	void Update () {
		setpoint.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, 0);
		//actual.Translate(pid.Update(setpoint.position.x, actual.position.x, Time.deltaTime), 0, 0);
	}
}
	
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ccAction;
public class GameMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CCAction.Update(Time.deltaTime);
	}
}

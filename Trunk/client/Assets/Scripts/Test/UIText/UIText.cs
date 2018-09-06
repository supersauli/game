using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIText : MonoBehaviour {
    public Vector2 _pos ;
	// Use this for initialization
	void Start () {
            
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<RectTransform>().anchoredPosition = _pos;
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Zoom : MonoBehaviour {

	public void ZoomInOut() {
		var camera = Camera.main;
		var brain = (camera == null) ? null : camera.GetComponent<CinemachineBrain>();
		var vcam = (brain == null) ? null : brain.ActiveVirtualCamera as CinemachineVirtualCamera;
		if (vcam != null){
			vcam.m_Lens.OrthographicSize = 20;
		}
	}
}

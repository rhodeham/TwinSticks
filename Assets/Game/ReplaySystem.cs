using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour {

	private const int bufferFrames = 300;
	private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
	private Rigidbody rigidBody;
	private GameManager gameManager;
	private int numberFramesRecorded = 0;
	private int numberFramesPlayed = 0;
	private bool startRecord = false;


	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		gameManager = GameObject.FindObjectOfType<GameManager>();
	}

	// Update is called once per frame
	void Update () {

		if(!startRecord && gameManager.recording) {
			numberFramesRecorded = 0;
			numberFramesPlayed = 0;
		}

		if (gameManager.recording) {
			Record();
			startRecord = true;
		} else {
			PlayBack();
			startRecord = false;
		}

	}

	void PlayBack () {
		rigidBody.isKinematic = true;
		int startFrame = Mathf.Max(0, numberFramesRecorded - bufferFrames) % bufferFrames;
		//print ("Start Frame " + startFrame);
		//print ("Number Frames Played " + numberFramesPlayed);
		int frame = (startFrame + numberFramesPlayed) % Mathf.Min(Mathf.Max(numberFramesRecorded,1), bufferFrames);
		//print ("Reading Frame " + frame);
		transform.position = keyFrames[frame].position;
		transform.rotation = keyFrames[frame].rotation;
		numberFramesPlayed++;

	}

	void Record ()
	{
		int frame = numberFramesRecorded % bufferFrames;
		float time = Time.time;
		rigidBody.isKinematic = false;
		//print ("Writing Frame " + frame);
		keyFrames [frame] = new MyKeyFrame (time, transform.position, transform.rotation);
		numberFramesRecorded++;
	}
}

/// <summary>
/// A structure for storting time, position and rotation.
/// </summary>
public struct MyKeyFrame {

	public float frameTime;
	public Vector3 position;
	public Quaternion rotation;

	public MyKeyFrame (float aTime, Vector3 aPos, Quaternion aRot) {
		frameTime = aTime;
		position = aPos;
		rotation = aRot;
	}

}
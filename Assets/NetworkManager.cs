using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public Camera standbyCamera;
	SpawnSpot[] spawnSpots;
	public GameObject whiteBoard;
	public Texture2D mywhiteBoardTexture;
	WWW www;
	//string url = "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg";

	// Use this for initialization
	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
		whiteBoard = GameObject.Find("Whiteboard");
		//Texture2D mytexture=(Texture2D) Resources.Load("file:///Users/WORLANYO/Documents/RealProject/Assets/Resources/Textures.logo.png",typeof(Texture2D));
		//whiteBoard.renderer.material.mainTexture = mytexture;

		StartCoroutine("Loadfromweb");
		//mywhiteBoardTexture = (Texture2D)Resources.Load("logo.png");
		connect ();

			//WWW www = new WWW(url);
			//yield return www;
			//renderer.material.mainTexture = www.texture;
			//string url = "http://upload.wikimedia.org/wikipedia/en/4/4a/Unity_3D_logo.png";
			//www = new WWW(url);
			//StartCoroutine(WaitForRequest(www));
	}

	IEnumerator Loadfromweb(){
		www = new WWW( "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg" );
		yield return www;
		whiteBoard.renderer.material.mainTexture = www.texture;
	}
	//void OnGUI(){
	//	GUI.DrawTexture(new Rect(0,0,100,100), www.texture, ScaleMode.StretchToFill);
	//}

	//public void DownloadImage(string url)
	//{   
		// StartCoroutine(coDownloadImage(url));
	//}
	
	//IEnumerator coDownloadImage(string imageUrl)
	//{
		
		//WWW www = new WWW( imageUrl );
		
		//yield return www;
		
		//whiteBoard = new Texture2D(www.texture.width, www.texture.height, TextureFormat.DXT1, false);
		//www.LoadImageIntoTexture(whiteBoard as Texture2D);
		//www.Dispose();
		//www = null;
	//}

	void connect(){
		PhotonNetwork.ConnectUsingSettings ("First Version v01");
	}

	void OnGui(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby(){
		Debug.Log("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log ("OnPhotonRandomFailed");
		PhotonNetwork.CreateRoom (null);
	}

	void OnJoinedRoom(){
		Debug.Log ("OnJoinedLobby");

		SpawnMyPlayer ();
	}

	void SpawnMyPlayer(){
		if (spawnSpots == null) {
			Debug.LogError ("WTF");
			return;
		}
		SpawnSpot mySpawnSpot = spawnSpots[Random.Range (0, spawnSpots.Length)];
		GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate ("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);				
		standbyCamera.enabled = false;

		((MonoBehaviour)myPlayerGO.GetComponent("FPSInputController")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent ("MouseLook")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent ("CharacterMotor")).enabled = true;
		 myPlayerGO.transform.FindChild ("Main Camera").gameObject.SetActive (true);
	}


	//___________________________________________________________________________________________________________________
	// My own code 

	//void Example() {
	//	renderer.material.mainTexture = texture;
	//}


	//Here is my botton for uploading the slides
	/*
	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), "Activity Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "Upload Slides")) {
			Application.LoadLevel(1);
		}
		
		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), "Chat")) {
			Application.LoadLevel(2);
		}
	}*/








	//_______________________________________________________________________________________________________________________
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour {
	
	#region Fields
	
	List<GameObject> charList = new List<GameObject>();
	string cListStr;
	
	public GameObject current;
	int charCount = 0;
	
	bool MenuActive = true;

	Rect MenuRect = new Rect(0, 0, 250, 250);
	
	float rotateStep = 15f;
	bool tiltOnOff = false;
	float tiltStep = 5f;
	float maxTilt = 10;
	string fileLoc = @"C:\Images";
	bool sphOnOff = false;
	float aniStep = 0.05f;
	
	Vector2 scrollPosition = Vector2.zero;
	#endregion Fields
	
	#region Methods
	
	// Use this for initialization
	void Start() {
		// Call Load
		LoadFromResources();
	}
	
	void LoadFromResources()
	{
		charList.Clear();
		cListStr = "-Characters-\r\n";
		
		// Load up all current characters/prefabs in resources
		Object[] prefabs = Resources.LoadAll("Characters", typeof(GameObject));
		
		// This is just for names
		Object[] anims = Resources.LoadAll("Animations", typeof(AnimationClip));
		
		// Add them to a main list
		foreach (Object o in prefabs)
		{
			charList.Add((GameObject)o);
			cListStr += o.name + "\r\n";
		}
		
		cListStr += "\r\n-Animations-\r\n";
		
		// Add the Animation clips to the display
		foreach (Object a in anims)
			cListStr += a.name + "\r\n";
	}
	
	// Display the menus
	void OnGUI()
	{
		if (MenuActive)
		{
			GUI.Window(0, MenuRect, MenuWindow, "Menu");
		}
	}
	
	void MenuWindow(int ID)
	{
		GUILayout.BeginArea (new Rect(10, 30, 230, 230));
		
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Height(180));
		
		// Rotation
		GUILayout.BeginHorizontal();
		GUILayout.Label("Rotation Step: " + rotateStep.ToString("f0").PadLeft(2, '0'), GUILayout.Width(120));
		rotateStep = GUILayout.HorizontalSlider(rotateStep, 1f, 30f);
		GUILayout.EndHorizontal();
		
		// Tilt
		GUILayout.BeginHorizontal();
		GUILayout.Label("Tilt On/Off", GUILayout.Width(120));
		tiltOnOff = GUILayout.Toggle(tiltOnOff, " Add Tilts");
		GUILayout.EndHorizontal();
		
		if (tiltOnOff)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("Tilt Step: " + tiltStep.ToString("f0").PadLeft(2, '0'), GUILayout.Width(120));
			tiltStep = GUILayout.HorizontalSlider(tiltStep, 1f, 10f);
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Max Tilt: " + maxTilt.ToString("f0").PadLeft(2, '0'), GUILayout.Width(120));
			maxTilt = GUILayout.HorizontalSlider(maxTilt, tiltStep, 30f); 
			GUILayout.EndHorizontal();
		}
		
		// Save Location
		GUILayout.BeginHorizontal();
		GUILayout.Label("Save Location", GUILayout.Width(120));
		fileLoc = GUILayout.TextField(fileLoc);
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		GUILayout.Label("Spheres On/Off", GUILayout.Width(120));
		sphOnOff = GUILayout.Toggle(sphOnOff, " Add points");
		GUILayout.EndHorizontal();
		
		// Animation
		GUILayout.BeginHorizontal();
		GUILayout.Label("Animation: " + aniStep.ToString("f3"), GUILayout.Width(120));
		aniStep = GUILayout.HorizontalSlider(aniStep, 0.001f, 0.1f);
		GUILayout.EndHorizontal();
		
		// Character/Animation List
		GUILayout.BeginVertical();
		GUI.color = Color.yellow;
		GUILayout.Label(cListStr);
		GUI.color = Color.white;
		GUILayout.EndVertical();
		
		GUILayout.EndScrollView();
		
		// Buttons
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Start", GUILayout.Width(80)))
		{
			// Close windows
			MenuActive = false;
			
			// Set first character
			ChangeChar();
			
			// Call Start on filecount 0
			StartChar(0);
		}
		
		if(GUILayout.Button ("Reset", GUILayout.Width(80)))
		{
			rotateStep = 15f;
			tiltOnOff = false;
			tiltStep = 5f;
			maxTilt = 10;
			fileLoc = @"C:\Images";
			sphOnOff = false;
			aniStep = 0.05f;
			LoadFromResources();
		}
		GUILayout.EndHorizontal();
		
		GUILayout.EndArea();
	}
	
	// Overloaded to prevent repeat code and for the first character
	public void ChangeChar()
	{
		Destroy(current);
		current = (GameObject)Instantiate(charList[charCount]);
		current.AddComponent("CharacterScript");
		
		charCount++;
	}
	
	// For new chars
	public void ChangeChar(int newFileCount)
	{
		if (charCount < charList.Count)
		{
			ChangeChar();
	
			StartChar(newFileCount);
		}
		else
		{
			charCount = 0;
			MenuActive = true;
		}
		
	}
	
	public void StartChar(int fC)
	{
		CharacterScript newScript = current.GetComponent<CharacterScript>();
		
		if (tiltOnOff)
		{
			newScript.Initialise(rotateStep, tiltOnOff, tiltStep, maxTilt, fileLoc, sphOnOff, aniStep, fC);
		}
		else
		{
			newScript.Initialise(rotateStep, tiltOnOff, fileLoc, sphOnOff, aniStep, fC);
		}
	}
			
	// Update is called once per frame
	void Update() {
	
	}
	
	#endregion Methods
}

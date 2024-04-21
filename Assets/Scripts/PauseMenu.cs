using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject[] pauseObjects;

	void Start()
	{
		Time.timeScale = 1;
		HidePaused();
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Time.timeScale == 1)
			{
				Time.timeScale = 0;
				ShowPaused();
				Cursor.lockState = CursorLockMode.None;
			}
			else if (Time.timeScale == 0)
			{
				Time.timeScale = 1;
				HidePaused();
				Cursor.lockState = CursorLockMode.Locked;
			}
		}
	}
	public void PauseControl()
	{
		if (Time.timeScale == 1)
		{
			Time.timeScale = 0;
			ShowPaused();
		}
		else if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
			HidePaused();
		}
	}

	public void ShowPaused()
	{
		foreach (GameObject g in pauseObjects)
		{
			g.SetActive(true);
		}
	}

	public void HidePaused()
	{
		foreach (GameObject g in pauseObjects)
		{
			g.SetActive(false);
		}
	}
}

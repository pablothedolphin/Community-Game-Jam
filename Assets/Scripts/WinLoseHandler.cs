using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseHandler : MonoBehaviour
{
	public GameObject winloseScreen;
	public GameObject winScreen;
	public GameObject loseScreen;

	public Animator chad;
	public Animator jenny;

	public AudioClip winSound;
	public AudioClip loseSound;

	public AudioSource source;

	private void Start ()
	{
		source = GetComponent<AudioSource> ();
	}

	public void Win ()
	{
		winloseScreen.SetActive (true);
		winScreen.SetActive (true);
		chad.SetTrigger ("Win");
		jenny.SetTrigger ("Win");
		source.PlayOneShot (winSound);
	}

	public void Lose ()
	{
		winloseScreen.SetActive (true);
		loseScreen.SetActive (true);
		jenny.SetTrigger ("Lose");
		source.PlayOneShot (loseSound);
	}

	public void Restart ()
	{
		SceneManager.LoadScene (0);
	}
}

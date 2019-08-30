using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
	public GameObject heart;
	public GameObject questionMark;
	public GameObject angryFace;

	public AudioClip like;
	public AudioClip dislike;
	public AudioClip contradict;

	private AudioSource source;

	private void Start ()
	{
		source = GetComponent<AudioSource> ();
	}

	public void LikeResponse ()
	{
		Destroy (Instantiate (heart), 4);
		source.PlayOneShot (like);
	}

	public void DislikeResponse ()
	{
		Destroy (Instantiate (angryFace), 4);
		source.PlayOneShot (dislike);
	}

	public void ContradictResponse ()
	{
		Destroy (Instantiate (questionMark), 4);
		source.PlayOneShot (contradict);
	}
}

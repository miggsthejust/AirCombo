using UnityEngine;
using System.Collections;

public class AnnouncerSounds : MonoBehaviour 
{

	public AudioSource itsExpo;
	public AudioSource wellSee;
	public AudioSource contribute;
	public AudioSource letsGet;
	public AudioSource schooled;
	public AudioSource sleepOn;
	
	public AudioSource musicTrack;
	
	public float speechVolume;
	
	void Start()
	{
		itsExpo.volume = speechVolume;
		wellSee.volume = speechVolume;
		contribute.volume = speechVolume;
		letsGet.volume = speechVolume;
		schooled.volume = speechVolume;
		sleepOn.volume = speechVolume;
	}
	
	public void PlayIntro()
	{
		itsExpo.Play();
	}
	
	public void PlaySelected()
	{
		wellSee.Play();
	}
	
	public void PlayVs()
	{
		letsGet.Play();
	}
	
	public void PlayGameOverLose()
	{
		sleepOn.Play();
	}
	public void PlayGameOverWin()
	{
		schooled.Play();
	}
	
	public void PlayMusic()
	{
		musicTrack.Play();
	}
	
	public void StopMusic()
	{
		musicTrack.Stop();
	}
	
	
}
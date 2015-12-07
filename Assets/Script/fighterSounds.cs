using UnityEngine;
using System.Collections;

public class fighterSounds : MonoBehaviour 
{

	public AudioSource RegIntroSpeech;
	public AudioSource RegEndingSpeech;
	public AudioSource RegKoSpeech;
	public AudioSource KazIntroSpeech;
	public AudioSource KazEndingSpeech;
	public AudioSource KazKoSpeech;
	public AudioSource MatIntroSpeech;
	public AudioSource MatEndingSpeech;
	public AudioSource MatKoSpeech;
	
	public AudioSource special01Speech;
	public AudioSource special02Speech;
	public AudioSource special03Speech;
	public AudioSource special04Speech;
	
	
	public AudioSource gotHitL;
	public AudioSource gotHitM;
	public AudioSource gotHitH;
	public AudioSource groundHit;
	public AudioSource block;
	public AudioSource swingL;
	public AudioSource swingH;
	
	public float speechVolume;
	public float sfxVolume;
	
	void Start()
	{
		ResetVolumes();
	}
	
	public void ResetVolumes()
	{
		RegIntroSpeech.volume = RegIntroSpeech.volume * speechVolume;
		RegEndingSpeech.volume = RegEndingSpeech.volume * speechVolume;
		RegKoSpeech.volume = RegKoSpeech.volume * speechVolume;
		
		KazIntroSpeech.volume = KazIntroSpeech.volume * speechVolume;
		KazEndingSpeech.volume = KazEndingSpeech.volume * speechVolume;
		KazKoSpeech.volume = KazKoSpeech.volume * speechVolume;
		
		MatIntroSpeech.volume = MatIntroSpeech.volume * speechVolume;
		MatEndingSpeech.volume = MatEndingSpeech.volume * speechVolume;
		MatKoSpeech.volume = MatKoSpeech.volume * speechVolume;
		
		gotHitL.volume = gotHitL.volume * sfxVolume;
		gotHitM.volume = gotHitM.volume * sfxVolume;
		gotHitH.volume = gotHitH.volume * sfxVolume;
		
		
		groundHit.volume = groundHit.volume *sfxVolume;
		swingL.volume = swingH.volume * sfxVolume;
		swingH.volume = swingH.volume * sfxVolume;
		block.volume = block.volume * sfxVolume;
	}
	
	public void PlayRegIntro()
	{
		RegIntroSpeech.Play();
	}
	public void PlayKazIntro()
	{
		KazIntroSpeech.Play();
	}
	public void PlayMatIntro()
	{
		MatIntroSpeech.Play();
	}
	
	public void PlayRegEnding()
	{
		RegEndingSpeech.Play();
	}
	public void PlayKazEnding()
	{
		//RegEndingSpeech.Play();
	}
	public void PlayMatEnding()
	{
		MatEndingSpeech.Play();
	}
	
	public void PlayMatKo()
	{
		MatKoSpeech.Play();
	}
	public void PlayRegKo()
	{
		RegKoSpeech.Play();
	}
	public void PlayKazKo()
	{
		KazKoSpeech.Play();
	}
	
	public void PlaySpecial01()
	{
		special01Speech.Play();
	}
	public void PlaySpecial02()
	{
		special02Speech.Play();
	}
	public void PlaySpecial03()
	{
		special03Speech.Play();
	}
	public void PlaySpecial04()
	{
		special04Speech.Play();
	}
	public void PlayGotHitL()
	{
		gotHitL.Play();
	}
	public void PlayGotHitM()
	{
		gotHitM.Play();
	}
	public void PlayGotHitH()
	{
		//var randNum = Random.Range(0.8f,1.2f);
		//gotHitH.pitch = randNum;
		gotHitH.Play();
	}
	public void PlayGotGroundHit()
	{
		//var randNum = Random.Range(0.8f,1.2f);
		//groundHit.pitch = randNum;
		groundHit.Play();
	}
	public void PlayGotSwingL()
	{
		//var randNum = Random.Range(0.8f,1.2f);
		//swingH.pitch = randNum;
		swingL.Play();
	}
	public void PlayGotSwingH()
	{
		//var randNum = Random.Range(0.8f,1.2f);
		//swingH.pitch = randNum;
		swingH.Play();
	}
	public void PlayBlock()
	{
		//var randNum = Random.Range(0.8f,1.2f);
		//swingH.pitch = randNum;
		block.Play();
	}
}
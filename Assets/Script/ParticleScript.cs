using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour 
{
	public float time = 0.0f;
	public float speed = 0.0f;
	public ParticleSystem balls;
	public ParticleSystem fire;
	public ParticleSystem firebits;
	
	void Awake()
	{
		Destroy(gameObject, time);
		this.balls.playbackSpeed = speed;
		this.fire.playbackSpeed = speed;
		this.firebits.playbackSpeed = speed;
		
	}
}
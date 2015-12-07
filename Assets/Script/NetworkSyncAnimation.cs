using UnityEngine;
using System.Collections;
using System;

public class NetworkSyncAnimation : MonoBehaviour 
{
	public enum AniStates 
	{
		crouch = 0,
		crouchBlock,
		crouchHKick,
		crouchHPunch,
		crouchLKick,
		crouchLPunch,
		stand,
		standBlock,
		standHKick,
		standHPunch,
		standLKick,
		standLPunch,
		jump,
		jumpHKick,
		jumpHPunch,
		jumpLPunch,
		jumpLKick,
		walkBackward,
		walkForward,
		jumping,
		knockdown,
		down,
		blueRay,
		moveUpper,
		standParry,
		standHitLow,
		SpGrill,
		SpPsProj,
		gotThrown,
		throwStart,
		throwFinish,
		
	}
	
	public AniStates currentAnimation = AniStates.stand;
	public AniStates lastAnimation = AniStates.stand;
	
	public void SyncAnimation(String animationValue)
	{
		currentAnimation = (AniStates)Enum.Parse(typeof(AniStates), animationValue);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (lastAnimation != currentAnimation)
		{
			lastAnimation = currentAnimation;
			GetComponent<Animation>().CrossFade(Enum.GetName(typeof(AniStates), currentAnimation));
		}
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if (stream.isWriting)
		{
			char ani = (char)currentAnimation;
			stream.Serialize(ref ani);
		}
		else
		{
			char ani = (char)0;
			stream.Serialize(ref ani);
			
			currentAnimation = (AniStates)ani;
		}	
	
	}

}

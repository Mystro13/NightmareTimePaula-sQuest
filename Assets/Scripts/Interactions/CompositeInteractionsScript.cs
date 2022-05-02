using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeInteractionsScript : MonoBehaviour
{
	public void Execute()
	{
		DoPlayAnimation();
		DoPlayAudioSource();
		DoPlayDialogue();
	}
	void DoPlayAnimation()
	{
		PlayAnimationScript playAnimationScript = gameObject.GetComponent<PlayAnimationScript>();
		if (playAnimationScript)
		{
			playAnimationScript.Execute();
		}
	}
	void DoPlayDialogue()
	{
		PlayDialogScript playDialogScript = gameObject.GetComponent<PlayDialogScript>();
		if (playDialogScript)
		{
			playDialogScript.Execute();
		}
	}

	void DoPlayAudioSource()
	{
		PlayAudioScript playAudioScript = gameObject.GetComponent<PlayAudioScript>();
		if (playAudioScript)
		{
			playAudioScript.Execute();
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
public class InteractionObject : MonoBehaviour
{
	public InteractionType interactionType;
	public GameObject neededInteractionObject;
	public Text interactionFeedback;
	public string interactionDialog;
	public bool enableInteraction;
	public bool entryIsLocked;
	public bool entryIsOpened;

	private void Start()
	{
		enableInteraction = true;
	}

	//what happen when you interact with 
	public void OnInteract()
	{
		//picked up and put in inventory
		gameObject.SetActive(false);
	}

	//playing opening entry animation
	public void OpenEntry()
	{
		OpenEntryScript openEntryScript = gameObject.GetComponent<OpenEntryScript>();
		if(openEntryScript)
		{
			openEntryScript.Open = entryIsOpened;
			openEntryScript.Execute();
      }
	}

	public void DoAdditionalInteraction()
	{
		CompositeInteractionsScript compositeInteractionsScript = gameObject.GetComponent<CompositeInteractionsScript>();
		if (compositeInteractionsScript)
		{
			compositeInteractionsScript.Execute();
		}
   }
   public void DoFlashAndDie()
	{
		FlashAndDieScript flashAndDieScript = gameObject.GetComponent<FlashAndDieScript>();
		if(flashAndDieScript)
		{
			flashAndDieScript.Execute();
      }
	}
}





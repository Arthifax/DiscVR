using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IHasChanged {
	[SerializeField] Transform slots;
	[SerializeField] Text inventoryText;

	// Use this for initialization
	void Start () {
		HasChanged ();
	}
	
	#region IHasChanged implementation
	public void HasChanged ()
	{
		System.Text.StringBuilder builder = new System.Text.StringBuilder();
		foreach (Transform slotTransform in slots)
		{
			Item item = slotTransform.GetComponentInChildren<Item>();
			if (item)
			{
				builder.Append (item.ItemID);
			}
		}
		Debug.Log(builder.ToString());
		if(builder.ToString().Equals("BDHMNORT"))
		{
			builder.Append (" Correct Order: The unlock code is 3056.");

			Debug.Log("Correct Order: The unlock code is 3056.");
		}
		inventoryText.text = builder.ToString ();
	}
	#endregion
}


namespace UnityEngine.EventSystems {
	public interface IHasChanged : IEventSystemHandler {
		void HasChanged();

	}
}
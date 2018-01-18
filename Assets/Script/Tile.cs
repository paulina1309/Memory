using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Tile : MonoBehaviour 
{
	public bool Active = true; 
	public bool aktywny= true; 
	public Sprite frontFace;

	 
	void Start () 
	{
		transform.rotation = GetTargetRotation ();
		var frontObject = transform.FindChild("front");
		var spriteRenderer = frontObject.transform.GetComponent<SpriteRenderer> (); 
		spriteRenderer.sprite = frontFace; 
	}

	void Update ()
	{
		var targetRotation = GetTargetRotation ();
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime*5f);

		if (aktywny == false)
			Destroy (gameObject);
		
	}


	Quaternion GetTargetRotation()
	{
		var rotation = Active ? Vector3.zero : (Vector3.up * 180f);
		return Quaternion.Euler (rotation);
	}


	private void OnMouseDown()
	{
		var board = FindObjectOfType<Boardd> ();

		if (board.CanMowe == false)
			return;
		
		Active = !Active;
		board.CheckPair ();
	}


}

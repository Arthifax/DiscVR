using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HallwayNametag : MonoBehaviour {

	public Text displayText;
	int current = 0;
	string[] names = new string[10] {  " ", "Terry\nBakker", "Taylah\nCrowther", "Raheel\nCarrillo", "Irving\nDevlin", "Erik\nRamsay", "Ria\nWilkinson", "Braydon\nReader", "Leopold\nKrueger", "Wilfred\nCote" };
	// Use this for initialization
	public int correct;
	public bool correctAnswer;
	void Start () {
		displayText.text = " ";
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void HitByPlayer()
	{
		current++;
		if (current > 9)
			current = 0;
		displayText.text = names[current];
		if(current == correct)
		{
			correctAnswer = true;
		}
		else
		{
			correctAnswer = false;
		}
	}
}

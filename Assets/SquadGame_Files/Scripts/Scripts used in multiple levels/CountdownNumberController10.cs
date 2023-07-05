using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownNumberController10 : MonoBehaviour
{
    public List<GameObject> Number0Red;
    public List<GameObject> Number0Grey;

    public List<GameObject> Number1Red;
    public List<GameObject> Number1Grey;

    public List<GameObject> Number2Red;
    public List<GameObject> Number2Grey;

    public List<GameObject> Number3Red;
    public List<GameObject> Number3Grey;

    public List<GameObject> Number4Red;
    public List<GameObject> Number4Grey;

    public List<GameObject> Number5Red;
    public List<GameObject> Number5Grey;

    public List<GameObject> Number6Red;
    public List<GameObject> Number6Grey;

    public Color ColorRed;
    public Color ColorGrey;

    private int currentValue = -1;

    public void GetNumber(int value)
    {
        if (value != currentValue)
        {
            switch (value)
            {
                case 0:
                    setColor(Number0Red, Number0Grey);
                    break;
                case 1:
                    setColor(Number1Red, Number1Grey);
                    break;
                case 2:
                    setColor(Number2Red, Number2Grey);
                    break;
                case 3:
                    setColor(Number3Red, Number3Grey);
                    break;
                case 4:
                    setColor(Number4Red, Number4Grey);
                    break;
                case 5:
                    setColor(Number5Red, Number5Grey);
                    break;
                case 6:
                    setColor(Number6Red, Number6Grey);
                    break;
            }
            currentValue = value;
        }
    }

    private void setColor(List<GameObject> Red, List<GameObject> Grey)
    {
        foreach (GameObject G in Red)
        {
            if (G.GetComponent<Renderer>() != null)
            {
                G.GetComponent<Renderer>().material.color = ColorRed;
            }
        }
        foreach (GameObject G in Grey)
        {
            if (G.GetComponent<Renderer>() != null)
            {
                G.GetComponent<Renderer>().material.color = ColorGrey;
            }
        }
    }
}

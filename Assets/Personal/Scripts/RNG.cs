using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RNG
{
    private static System.Random random = new System.Random();

	public static int GimmeNumber(int min, int max, bool inclusive)
    {
        if(inclusive) return random.Next(min, max + 1);
        else return random.Next(min, max);
    }

    public static float GimmeNumber(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}

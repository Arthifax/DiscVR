using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Jextensions
{
    public static void Do<T>(this T item, Action<T> action) => action(item);
}

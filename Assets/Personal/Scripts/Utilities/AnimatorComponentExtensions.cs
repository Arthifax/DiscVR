using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorComponentExtensions
{
    /// <summary>
    /// Checks if AnimatorComonent has a parameter by the name of paramterName
    /// </summary>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    public static bool HasParameter(this Animator animator, string parameterName)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name == parameterName) { return true; }
        }

        return false;
    }
}
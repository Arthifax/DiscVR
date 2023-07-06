using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayPuzzle : Singleton<HallwayPuzzle> {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Combination[] combinations;
    [SerializeField]
    private List<LampData> lamps = new List<LampData>();
    [SerializeField]
    private Material lampMaterial;
    [SerializeField]
    private GameObject disableOnWin;

    protected override void Awake()
    {
        base.Awake();
    }

    public static void CheckIfPuzzleIsCompleted()
    {
        foreach (var combination in Instance.combinations)
            if (!combination.IsCorrect)
                return;
        // TODO: OnPuzzleCompleted
        foreach (var lamp in Instance.lamps)
            lamp.DoStuffWithLamp();

        Instance.disableOnWin.SetActive(false);
        Debug.Log("Yataa OwO");
    }

    [Serializable]
    private struct Combination
    {
        [SerializeField]
        private Painting painting;
        [SerializeField]
        private Suitcase suitcase;

        public bool IsCorrect
        {
            get
            {
                return painting.AttachedSuitcase == suitcase;
            }
        }
    }

    [Serializable]
    private struct LampData
    {
        [SerializeField]
        private Renderer renderer;
        [SerializeField]
        private Light light;

        public void DoStuffWithLamp()
        {
            renderer.material = Instance.lampMaterial;
            //light.gameObject.SetActive(false);
        }
    }
}

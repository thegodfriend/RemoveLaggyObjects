﻿using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RemoveLaggyObjects
{
    public class RemoveLaggyObjects : Mod
    {
        internal static RemoveLaggyObjects Instance;

        public RemoveLaggyObjects() : base("RemoveLaggyObjects") { }
        public override string GetVersion() => "1.0.1";

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Initializing");

            Instance = this;

            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneChanged;

            Log("Initialized");
        }

        private void SceneChanged(Scene arg0, Scene arg1)
        {
            // Grouped removals
            if (arg1.name.Contains("Room_Colosseum") || arg1.name.Contains("GG_"))
            {
                GameManager.instance.StartCoroutine(RemoveCrowd());
            }

            // Slightly more specific removals
            if (arg1.name == "GG_Sly")
            {
                GameManager.instance.StartCoroutine(RemoveCandles());
            }
            else if (arg1.name == "GG_Hornet_2")
            {
                GameManager.instance.StartCoroutine(RemoveWind());
            }
            else if (arg1.name == "GG_Broken_Vessel" || arg1.name == "Room_Final_Boss_Core")
            {
                GameManager.instance.StartCoroutine(RemoveSmoke());
            }
            else if (arg1.name == "GG_Hollow_Knight")
            {
                GameManager.instance.StartCoroutine(RemoveAbyssParticles());
            }
        }

        // Meant for use in Sly's fight
        private IEnumerator RemoveCandles()
        {
            yield return new WaitForFinishedEnteringScene();
            //yield return null;

            foreach (GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {

                if (go.name.Contains("candle") || go.name.Contains("Candle")) UnityEngine.Object.Destroy(go);
                
            }
        }

        // Meant for use in Hornet Sentinel
        private IEnumerator RemoveWind()
        {
            yield return new WaitForFinishedEnteringScene();

            foreach (GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {

                if (go.name.Contains("blizzard") || go.name.Contains("Blizzard")) UnityEngine.Object.Destroy(go);

            }
        }

        // Meant for use in Colosseum and Godhome
        private IEnumerator RemoveCrowd()
        {
            yield return new WaitForFinishedEnteringScene();

            foreach (GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {
                if (!go.name.Contains("Godseeker Crowd")) // Check to keep Godseeker in
                {
                    // Removes colo's Crowd NPC, Crowd Audio, and Godhome's various instances of Godseeker Crowd
                    if (go.name.Contains("crowd") || go.name.Contains("Crowd")) UnityEngine.Object.Destroy(go);
                }
            }
        }

        // Meant for use in Broken Vessel and THK
        private IEnumerator RemoveSmoke()
        {
            yield return new WaitForFinishedEnteringScene();

            foreach (GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {
                if (go.name.Contains("wispy smoke")) UnityEngine.Object.Destroy(go);
            }
        }

        // Meant for use in PV
        private IEnumerator RemoveAbyssParticles()
        {
            yield return new WaitForFinishedEnteringScene();

            foreach (GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {
                if (go.name.Contains("abyss particles")) UnityEngine.Object.Destroy(go);
            }
        }

    }
}
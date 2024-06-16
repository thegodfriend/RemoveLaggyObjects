using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UObject = UnityEngine.Object;

namespace RemoveLaggyObjects
{
    public class RemoveLaggyObjects : Mod
    {
        internal static RemoveLaggyObjects Instance;

        public override string GetVersion()
        {
            return "1.0.0.13";
        }

        //public override List<ValueTuple<string, string>> GetPreloadNames()
        //{
        //    return new List<ValueTuple<string, string>>
        //    {
        //        new ValueTuple<string, string>("White_Palace_18", "White Palace Fly")
        //    };
        //}

        //public RemoveLaggyObjects() : base("RemoveLaggyObjects")
        //{
        //    Instance = this;
        //}

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
                GameManager.instance.StartCoroutine(RemoveGlowResponseParticles());
            }
        }

        // Meant for use in Sly's fight
        private IEnumerator RemoveCandles()
        {
            yield return new WaitForFinishedEnteringScene();
            yield return null;

            foreach (GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {

                if (go.name.Contains("candle") || go.name.Contains("Candle")) UnityEngine.Object.Destroy(go);
                
            }
        }

        // Meant for use in Hornet Sentinel
        private IEnumerator RemoveWind()
        {
            yield return new WaitForFinishedEnteringScene();
            yield return null;

            foreach (GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {

                if (go.name.Contains("blizzard") || go.name.Contains("Blizzard")) UnityEngine.Object.Destroy(go);

            }
        }

        // Meant for use in Colosseum and Godhome
        private IEnumerator RemoveCrowd()
        {
            yield return new WaitForFinishedEnteringScene();
            yield return null;

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
            yield return null;

            foreach (GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {
                if (go.name.Contains("wispy smoke")) UnityEngine.Object.Destroy(go);
            }
        }

        // Meant for use in PV
        private IEnumerator RemoveAbyssParticles()
        {
            yield return new WaitForFinishedEnteringScene();
            yield return null;

            foreach (GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {
                if (go.name.Contains("abyss particles")) UnityEngine.Object.Destroy(go);
            }
        }

        // For PV; testing
        private IEnumerator RemoveGlowResponseParticles()
        {
            yield return new WaitForFinishedEnteringScene();
            yield return null;

            foreach (GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {
                if (go.name.Contains("Particle System"))
                    UnityEngine.Object.Destroy(go);
            }
        }
    }
}
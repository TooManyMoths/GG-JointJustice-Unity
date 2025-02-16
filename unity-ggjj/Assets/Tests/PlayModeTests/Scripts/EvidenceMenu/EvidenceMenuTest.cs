using System.Collections;
using System.Linq;
using Tests.PlayModeTests.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayModeTests.Scripts.EvidenceMenu
{
    public class EvidenceMenuTest
    {
        protected InputTestTools InputTestTools { get; } = new InputTestTools();
        protected EvidenceController EvidenceController { get; private set; }
        protected NarrativeScriptPlayerComponent NarrativeScriptPlayerComponent { get; private set; }
        protected global::EvidenceMenu EvidenceMenu { get; private set; }
        protected Transform CanvasTransform { get; private set; }
        protected Menu Menu { get; private set; }
        protected StoryProgresser StoryProgresser { get; private set; }

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return SceneManager.LoadSceneAsync("Game");
            TestTools.StartGame("AddRecordTest");

            StoryProgresser = new StoryProgresser();
            EvidenceController = Object.FindObjectOfType<EvidenceController>();
            NarrativeScriptPlayerComponent = Object.FindObjectOfType<NarrativeScriptPlayerComponent>();
            EvidenceMenu = TestTools.FindInactiveInScene<global::EvidenceMenu>()[0];
            Menu = EvidenceMenu.GetComponent<Menu>();
            CanvasTransform = Object.FindObjectOfType<Canvas>().transform;
            var dialogueController = Object.FindObjectOfType<global::AppearingDialogueController>();
            yield return TestTools.WaitForState(() => !dialogueController.IsPrintingText);
        }

        protected ActorData[] AddProfiles()
        {
            ActorData[] actors = Resources.LoadAll<ActorData>("Actors");

            foreach (var actorData in actors)
            {
                EvidenceController.AddRecord(actorData);
            }

            return actors;
        }
        
        protected Evidence[] AddEvidence()
        {
            var evidenceOfCurrentScript = NarrativeScriptPlayerComponent.NarrativeScriptPlayer.ActiveNarrativeScript.ObjectStorage.GetObjectsOfType<Evidence>().ToArray();
            foreach (var evidence in evidenceOfCurrentScript)
            {
                EvidenceController.AddEvidence(evidence);
            }

            return evidenceOfCurrentScript;
        }
        
        protected IEnumerator PressZ()
        {
            yield return InputTestTools.PressForFrame(InputTestTools.Keyboard.zKey);
        }
    }
}


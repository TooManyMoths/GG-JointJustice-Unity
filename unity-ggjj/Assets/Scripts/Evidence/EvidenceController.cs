using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EvidenceController : MonoBehaviour, IEvidenceController
{
    [Tooltip("Attach the action decoder object here")]
    [SerializeField] DirectorActionDecoder _directorActionDecoder;

    [Tooltip("This event is called when the PRESENT_EVIDENCE action is called.")]
    [SerializeField] private UnityEvent _onRequirePresentEvidence;

    private NarrativeScriptPlayer _narrativeScriptPlayer;
    
    public List<Evidence> CurrentEvidence { get; } = new List<Evidence>();
    public List<ActorData> CurrentProfiles { get; } = new List<ActorData>();
    
    void Awake()
    {
        _directorActionDecoder.Decoder.EvidenceController = this;
        _narrativeScriptPlayer = GetComponentInParent<NarrativeScriptPlayer>();
    }

    /// <summary>
    /// Adds a piece of evidence to the evidence menu. Gets an Evidence object
    /// from _masterEvidenceDictionary and adds it to _currentEvidenceDictionary
    /// </summary>
    /// <param name="evidenceName">The name of the evidence to add.</param>
    public void AddEvidence(string evidenceName)
    {
        CurrentEvidence.Add(_narrativeScriptPlayer.ActiveNarrativeScript.ObjectStorage.GetObject<Evidence>(evidenceName));
    }

    /// <summary>
    /// Overload which allows evidence to be added using a direct reference.
    /// </summary>
    /// <param name="evidence">The evidence to add.</param>
    public void AddEvidence(Evidence evidence)
    {
        CurrentEvidence.Add(evidence);
    }

    /// <summary>
    /// Removes a piece of evidence from the evidence menu.
    /// </summary>
    /// <param name="evidenceName">The name of the evidence to remove.</param>
    public void RemoveEvidence(string evidenceName)
    {
        CurrentEvidence.Remove(_narrativeScriptPlayer.ActiveNarrativeScript.ObjectStorage.GetObject<Evidence>(evidenceName));
    }

    /// <summary>
    /// Adds an actor to the court record.
    /// </summary>
    /// <param name="actorName">The name of the actor to add.</param>
    public void AddToCourtRecord(string actorName)
    {
        CurrentProfiles.Add(_narrativeScriptPlayer.ActiveNarrativeScript.ObjectStorage.GetObject<ActorData>(actorName));
    }
    
    /// <summary>
    /// Overload which allows profiles to be added using a direct reference.
    /// </summary>
    /// <param name="actorData">The ActorData to add.</param>
    public void AddToCourtRecord(ActorData actorData)
    {
        CurrentProfiles.Add(actorData);
    }

    /// <summary>
    /// Method called by DirectorActionDecoder to open the evidence menu and require the user to present a piece of evidence.
    /// Calls an event which should open (and disable closing of) the evidence menu.
    /// </summary>
    public void RequirePresentEvidence()
    {
        _onRequirePresentEvidence.Invoke();
    }

    /// <summary>
    /// Substitutes a piece of evidence with its assigned alternate evidence.
    /// </summary>
    /// <param name="evidenceName">The name of the evidence to be substituted with its alt</param>
    public void SubstituteEvidenceWithAlt(string evidenceName)
    {
        int evidenceIndex = CurrentEvidence.IndexOf(_narrativeScriptPlayer.ActiveNarrativeScript.ObjectStorage.GetObject<Evidence>(evidenceName));
        CurrentEvidence[evidenceIndex] = CurrentEvidence[evidenceIndex].AltEvidence;
    }

    /// <summary>
    /// Overload which allows evidence to be substituted using a direct reference.
    /// </summary>
    /// <param name="evidence">The evidence that should be substituted with its alt</param>
    public void SubstituteEvidenceWithAlt(Evidence evidence)
    {
        int evidenceIndex = CurrentEvidence.IndexOf(evidence);
        CurrentEvidence[evidenceIndex] = CurrentEvidence[evidenceIndex].AltEvidence;
    }
}

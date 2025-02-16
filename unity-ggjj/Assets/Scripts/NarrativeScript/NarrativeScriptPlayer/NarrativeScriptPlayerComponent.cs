using UnityEngine;

public class NarrativeScriptPlayerComponent : MonoBehaviour, INarrativeScriptPlayerComponent
{
    [SerializeField] private NarrativeGameState _narrativeGameState;
    
    private NarrativeScriptPlayer _narrativeScriptPlayer;

    public INarrativeScriptPlayer NarrativeScriptPlayer
    {
        get
        {
            if (_narrativeScriptPlayer == null)
            {
                _narrativeScriptPlayer = new NarrativeScriptPlayer(_narrativeGameState)
                {
                    ActiveNarrativeScript = _narrativeGameState.NarrativeScriptStorage.NarrativeScript
                };
            }

            return _narrativeScriptPlayer;
        }
    }

    public bool Waiting
    {
        get => NarrativeScriptPlayer.Waiting;
        set => NarrativeScriptPlayer.Waiting = value;
    }

    /// <summary>
    /// Exposes NarrativeScriptPlayer's Continue method for use in UnityEvents
    /// </summary>
    public void Continue()
    {
        NarrativeScriptPlayer.Continue();
    }

    /// <summary>
    /// Exposes NarrativeScriptPlayer's TryPressWitness method for use in UnityEvents
    /// </summary>
    public void TryPressWitness()
    {
        NarrativeScriptPlayer.TryPressWitness();
    }

    /// <summary>
    /// Loads a narrative script, ending the current narrative script
    /// and continuing the beginning of the loaded script
    /// </summary>
    /// <param name="narrativeScriptName">The name of the narrative script to load</param>
    public void LoadScript(string narrativeScriptName)
    {
        var narrativeScriptText = Resources.Load<TextAsset>($"InkDialogueScripts/{narrativeScriptName}");
        NarrativeScriptPlayer.ActiveNarrativeScript = new NarrativeScript(narrativeScriptText);
        _narrativeGameState.BGSceneList.ClearBGScenes();
        _narrativeGameState.BGSceneList.InstantiateBGScenes(NarrativeScriptPlayer.ActiveNarrativeScript);
        NarrativeScriptPlayer.Continue();
    }
}

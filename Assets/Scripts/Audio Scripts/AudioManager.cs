using UnityEngine;
using AK;

/*
1. Attach this script to the WwiseGlobal GameObject.
2. Assign the correct Wwise events to their respective fields in the Unity Inspector.
3. Use methods like AudioManager.Instance.PlayPlayerJump(playerGameObject) in your gameplay scripts.
*/
public class AudioManager : MonoBehaviour
{
    // Singleton instance for easy access across scripts.
    public static AudioManager Instance { get; private set; }

    [Header("Player Events")]
    public AK.Wwise.Event playerSwim;
    public AK.Wwise.Event playerJump;
    public AK.Wwise.Event playerDive;
    public AK.Wwise.Event playerDeath;

    [Header("Interaction Events")]
    public AK.Wwise.Event doorOpen;
    public AK.Wwise.Event doorClose;
    public AK.Wwise.Event rockImpact;
    public AK.Wwise.Event rockRoll;

    [Header("Environment Events")]
    public AK.Wwise.Event waterfallFlow;
    public AK.Wwise.Event chapter1Ambient;

    private void Awake()
    {
        // Ensure only one instance exists (Singleton pattern).
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates.
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist between scenes.

        // Set the "Music_State" state to "TitleScreen"
        AkSoundEngine.SetState("Music_State", "TitleScreen");

        // Trigger the Play_AllScenes_Music event
        var eventResult = AkSoundEngine.PostEvent("Play_AllScenes_Music", this.gameObject);
    }

    public void RestartMusic() {
        // Trigger the Play_AllScenes_Music event
        var eventResult = AkSoundEngine.PostEvent("Play_AllScenes_Music", this.gameObject);
    }


    /// <summary>
    /// Plays a Wwise event.
    /// </summary>
    /// <param name="akEvent">The Wwise event to play.</param>
    /// <param name="gameObject">The GameObject triggering the event (optional, defaults to AudioManager).</param>
    public void PlayEvent(AK.Wwise.Event akEvent, GameObject gameObject = null)
    {
        if (akEvent == null)
        {
            Debug.LogError("[AudioManager] Attempted to play a null event.");
            return;
        }

        // Default to AudioManager GameObject if none is provided.
        gameObject = gameObject != null ? gameObject : this.gameObject;

        akEvent.Post(gameObject);
    }

    // Wrapper methods for specific game audio events.
    public void PlayPlayerSwim(GameObject player) => PlayEvent(playerSwim, player);
    public void PlayPlayerJump(GameObject player) => PlayEvent(playerJump, player);
    public void PlayPlayerDive(GameObject player) => PlayEvent(playerDive, player);
    public void PlayPlayerDeath(GameObject player) => PlayEvent(playerDeath, player);

    public void PlayDoorOpen(GameObject door) => PlayEvent(doorOpen, door);
    public void PlayDoorClose(GameObject door) => PlayEvent(doorClose, door);

    public void PlayRockImpact(GameObject rock) => PlayEvent(rockImpact, rock);
    public void PlayRockRoll(GameObject rock) => PlayEvent(rockRoll, rock);

    public void PlayWaterfallFlow(GameObject waterfall) => PlayEvent(waterfallFlow, waterfall);

    public void PlayChapter1Ambient(GameObject ambient) => PlayEvent(chapter1Ambient, ambient);

    /// <summary>
    /// Debugging utility to log all assigned Wwise events.
    /// </summary>
    public void LogAssignedEvents()
    {
        Debug.Log("[AudioManager] Assigned Events:");
        Debug.Log($"Player Swim: {playerSwim}");
        Debug.Log($"Player Jump: {playerJump}");
        Debug.Log($"Player Dive: {playerDive}");
        Debug.Log($"Player Death: {playerDeath}");
        Debug.Log($"Door Open: {doorOpen}");
        Debug.Log($"Door Close: {doorClose}");
        Debug.Log($"Rock Impact: {rockImpact}");
        Debug.Log($"Rock Roll: {rockRoll}");
        Debug.Log($"Waterfall Flow: {waterfallFlow}");
        Debug.Log($"Chapter 1 Ambient: {chapter1Ambient}");
    }
}

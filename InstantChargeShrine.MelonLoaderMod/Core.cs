using Il2Cpp;
using MelonLoader;

[assembly: MelonInfo(typeof(InstantChargeShrine.MelonLoaderMod.Core), "InstantChargeShrine", "1.0.0", "Slimaeus", null)]
[assembly: MelonGame("Ved", "Megabonk")]

namespace InstantChargeShrine.MelonLoaderMod;

public class Core : MelonMod
{
    private const string _startScenceName = "GeneratedMap";
    private Il2CppSystem.Action _onChargeShrineSpawnedAction;
    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        if (sceneName != _startScenceName) return;
        SubscribeToShrineEvents();
    }

    public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
    {
        if (sceneName != _startScenceName) return;
        UnsubscribeFromShrineEvents();
    }

    private void OnChargeShrineSpawned()
    {
        var chargeShines = UnityEngine.Object.FindObjectsOfType<ChargeShrine>();

        foreach (var shrine in chargeShines)
        {
            if (shrine is null)
                continue;
            shrine.chargeTime = 0f;
            shrine.currentChargeTime = 0f;
        }
    }

    public override void OnDeinitializeMelon()
    {
        UnsubscribeFromShrineEvents();
    }

    public override void OnApplicationQuit()
    {
        UnsubscribeFromShrineEvents();
    }

    public override void OnInitializeMelon()
    {
        LoggerInstance.Msg("Initialized.");
    }
    private void SubscribeToShrineEvents()
    {
        UnsubscribeFromShrineEvents();
        _onChargeShrineSpawnedAction = (Il2CppSystem.Action)OnChargeShrineSpawned;
        ChargeShrine.A_ChargeShrineSpawned += _onChargeShrineSpawnedAction;
        LoggerInstance.Msg("Subscribed to ChargeShrine spawn events.");
    }
    private void UnsubscribeFromShrineEvents()
    {
        if (_onChargeShrineSpawnedAction == null) return;
        try
        {
            ChargeShrine.A_ChargeShrineSpawned -= _onChargeShrineSpawnedAction;
            LoggerInstance.Msg("Unsubscribed from ChargeShrine spawn events.");
        }
        catch (Exception ex)
        {
            LoggerInstance.Error($"Error unsubscribing from shrine events: {ex.Message}");
        }
        finally
        {
            _onChargeShrineSpawnedAction = null;
        }
    }
}
using System;
using System.ComponentModel;
using System.Text.Json;

namespace MCBE_WSS
{
    //https://gist.github.com/jocopa3/5f718f4198f1ea91a37e3a9da468675c
    public enum EventType
    {
        AdditionalContentLoaded,
        AgentCommand,
        AgentCreated,
        ApiInit,
        AppPaused,
        AppResumed,
        AppSuspended,
        AwardAchievement,
        BlockBroken,
        BlockPlaced,
        BoardTextUpdated,
        BossKilled,
        CameraUsed,
        CauldronUsed,
        ChunkChanged,
        ChunkLoaded,
        ChunkUnloaded,
        ConfigurationChanged,
        ConnectionFailed,
        CraftingSessionCompleted,
        EndOfDay,
        EntitySpawned,
        FileTransmissionCancelled,
        FileTransmissionCompleted,
        FileTransmissionStarted,
        FirstTimeClientOpen,
        FocusGained,
        FocusLost,
        GameSessionComplete,
        GameSessionStart,
        HardwareInfo,
        HasNewContent,
        ItemAcquired,
        ItemCrafted,
        ItemDestroyed,
        ItemDropped,
        ItemEnchanted,
        ItemSmelted,
        ItemUsed,
        JoinCanceled,
        JukeboxUsed,
        LicenseCensus,
        MascotCreated,
        MenuShown,
        MobInteracted,
        MobKilled,
        MultiplayerConnectionStateChanged,
        MultiplayerRoundEnd,
        MultiplayerRoundStart,
        NpcPropertiesUpdated,
        OptionsUpdated,
        performanceMetrics,
        PackImportStage,
        PlayerBounced,
        PlayerDied,
        PlayerJoin,
        PlayerLeave,
        PlayerMessage,
        PlayerTeleported,
        PlayerTransform,
        PlayerTravelled,
        PortalBuilt,
        PortalUsed,
        PortfolioExported,
        PotionBrewed,
        PurchaseAttempt,
        PurchaseResolved,
        RegionalPopup,
        RespondedToAcceptContent,
        ScreenChanged,
        ScreenHeartbeat,
        SignInToEdu,
        SignInToXboxLive,
        SignOutOfXboxLive,
        SpecialMobBuilt,
        StartClient,
        StartWorld,
        TextToSpeechToggled,
        UgcDownloadCompleted,
        UgcDownloadStarted,
        UploadSkin,
        VehicleExited,
        WorldExported,
        WorldFilesListed,
        WorldGenerated,
        WorldLoaded,
        WorldUnloaded
    }

    public class EventBuilder
    {
        private EventStructure eventStructure = new();

        public EventBuilder SetMessagePurpose(string messagePurpose)
        {
            eventStructure.header.messagePurpose = messagePurpose;
            return this;
        }

        public EventBuilder SetEventType (EventType eventType)
        {
            eventStructure.body.eventName = eventType.ToString();
            return this;
        }

        public string Build()
        {
            if (string.IsNullOrWhiteSpace(eventStructure.body.eventName))
                throw new Exception("Error. EventType must be set!");

            string convert = JsonSerializer.Serialize(eventStructure);
            return convert;
        }

        private class EventStructure
        {
            public EventHeaders header { get; set; } = new();
            public EventBody body { get; set; } = new();
        }

        private class EventHeaders
        {
            public string requestId { get; set; } = Guid.NewGuid().ToString();
            public string messagePurpose { get; set; } = "subscribe";
            public int version { get; set; } = 1;
        }

        private class EventBody
        {
            public string eventName { get; set; } = "";
        }
    }
}

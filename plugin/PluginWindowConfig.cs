﻿using Dalamud.Interface.Windowing;
using ImGuiNET;
using System;
using System.Numerics;

namespace PatMe
{
    public class PluginWindowConfig : Window, IDisposable
    {
        public PluginWindowConfig() : base("Pat Config")
        {
            IsOpen = false;

            SizeConstraints = new WindowSizeConstraints() { MinimumSize = new Vector2(100, 0), MaximumSize = new Vector2(300, 3000) };
            Flags = ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoScrollbar;
        }

        public void Dispose()
        {
        }

        public override void Draw()
        {
            bool showSpecialPats = Service.pluginConfig.showSpecialPats;
            bool showFlyText = Service.pluginConfig.showFlyText;
            bool showCounterOnScreen = Service.pluginConfig.showCounterUI;
            bool canTrackDotes = Service.pluginConfig.canTrackDotes;
            bool hasChanges = false;

            hasChanges = ImGui.Checkbox("Show notify on reaching pat goals", ref showSpecialPats) || hasChanges;
            hasChanges = ImGui.Checkbox("Show fly text counter on each emote", ref showFlyText) || hasChanges;
            hasChanges = ImGui.Checkbox("Show pat counter on screen", ref showCounterOnScreen) || hasChanges;

            ImGui.Separator();
            hasChanges = ImGui.Checkbox("Track emote: dote", ref canTrackDotes) || hasChanges;

            if (showCounterOnScreen != Service.pluginConfig.showCounterUI)
            {
                Service.plugin.OnShowCounterConfigChanged(showCounterOnScreen);
            }

            if (hasChanges)
            {
                Service.pluginConfig.showSpecialPats = showSpecialPats;
                Service.pluginConfig.showFlyText = showFlyText;
                Service.pluginConfig.showCounterUI = showCounterOnScreen;
                Service.pluginConfig.canTrackDotes = canTrackDotes;

                Service.pluginConfig.Save();

                Service.doteCounter.isActive = canTrackDotes;
            }
        }
    }
}

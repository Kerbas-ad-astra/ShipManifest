﻿using System;
using ShipManifest.InternalObjects;
using ShipManifest.Process;
using UnityEngine;

namespace ShipManifest.Windows.Tabs
{
  internal static class TabScienceLab
  {
    internal static string ToolTip = "";
    internal static bool ToolTipActive;
    internal static bool ShowToolTips = true;

    internal static void Display(Vector2 displayViewerPosition)
    {
      var scrollX = WindowControl.Position.x + 10;
      var scrollY = WindowControl.Position.y + 50 - displayViewerPosition.y;

      // Reset Tooltip active flag...
      ToolTipActive = false;

      GUILayout.BeginVertical();
      GUI.enabled = true;
      GUILayout.Label("Science Lab Control Center ", SMStyle.LabelTabHeader);
      GUILayout.Label("____________________________________________________________________________________________",
        SMStyle.LabelStyleHardRule, GUILayout.Height(10), GUILayout.Width(350));
      var step = "start";
      try
      {
        // Display all Labs
        foreach (var iLab in SMAddon.SmVessel.Labs)
        {
          var isEnabled = true;

          step = "gui enable";
          GUI.enabled = isEnabled;
          var label = iLab.name + " - (" + (iLab.IsOperational() ? "Operational" : "InOp") + ")";
          GUILayout.Label(label, GUILayout.Width(260), GUILayout.Height(40));
        }
      }
      catch (Exception ex)
      {
        Utilities.LogMessage(
          string.Format(" in Solar Panel Tab at step {0}.  Error:  {1} \r\n\r\n{2}", step, ex.Message, ex.StackTrace),
          Utilities.LogType.Error, true);
      }
      GUILayout.EndVertical();
    }

    internal static void ExtendAllPanels()
    {
      // TODO: for realism, add a closing/opening sound
      foreach (var iPanel in SMAddon.SmVessel.SolarPanels)
      {
        var iModule = (ModuleDeployableSolarPanel) iPanel.PanelModule;
        if (iModule.panelState == ModuleDeployableSolarPanel.panelStates.RETRACTED)
        {
          iModule.Extend();
        }
      }
    }

    internal static void RetractAllPanels()
    {
      // TODO: for realism, add a closing/opening sound
      foreach (var iPanel in SMAddon.SmVessel.SolarPanels)
      {
        var iModule = (ModuleDeployableSolarPanel) iPanel.PanelModule;
        if (iModule.panelState == ModuleDeployableSolarPanel.panelStates.EXTENDED)
        {
          iModule.Retract();
        }
      }
    }
  }
}
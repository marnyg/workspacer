
// Production
#r "C:\Program Files\workspacer\workspacer.Shared.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.Bar\workspacer.Bar.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.Gap\workspacer.Gap.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.ActionMenu\workspacer.ActionMenu.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.FocusIndicator\workspacer.FocusIndicator.dll"

using System;
using workspacer;
using workspacer.Bar;
using workspacer.ActionMenu;
using workspacer.FocusIndicator;

Action<IConfigContext> doConfig = (context) =>
{
    context.AddBar(new BarPluginConfig()
    {
        BarTitle = "workspacer.Bar",
        BarHeight = 20,
        FontSize = 12,
        FontName = "Font Awesome",
        DefaultWidgetForeground = Color.Blue,
        DefaultWidgetBackground = Color.White,
        Background = Color.White,
    });

    context.AddFocusIndicator(new FocusIndicatorPluginConfig()
    {
        BorderColor = Color.Lime,
        TimeToShow = 150,
    });

    var actionMenu = context.AddActionMenu(new ActionMenuPluginConfig()
    {
        Foreground = Color.Blue,
    });

    var sticky = new StickyWorkspaceContainer(context, StickyWorkspaceIndexMode.Local);
    context.WorkspaceContainer = sticky;

    // workspaces and layouts
    sticky.CreateWorkspaces("Chat", "Web", "Working", "Code", "Games", "Music", "Email");
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Outlook") ? context.WorkspaceContainer["Email"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Visual Studio") ? context.WorkspaceContainer["Code"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("PowerShell") ? context.WorkspaceContainer["Code"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Spotify") ? context.WorkspaceContainer["Music"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Firefox") ? context.WorkspaceContainer["Web"] : null);
    // filters
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Dota 2"));

    // keybinds
    var mod = KeyModifiers.Alt;
    context.Keybinds.Subscribe(mod, Keys.F, () => System.Diagnostics.Process.Start("explorer.exe", @"C:\Users\Desktop"));
    context.Keybinds.Subscribe(mod, Keys.B, () => System.Diagnostics.Process.Start(@"C:\Program Files\Mozilla Firefox\firefox.exe"));

};
return doConfig;
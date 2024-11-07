using System;
using Avalonia;

namespace DragonScribe;

/// <summary>
/// Program entry point.
/// </summary>
static class Program
{
	[STAThread]
	static void Main(string[] args) => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

	// Separate configuration method required by the visual designer.
	static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>().UsePlatformDetect().LogToTrace();
}

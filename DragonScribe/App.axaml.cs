using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using DragonScribe.UI;

namespace DragonScribe;

/// <summary>
/// Main Avalonia application.
/// </summary>
/// <seealso cref="Application"/>
class App : Application
{
	public override void Initialize() => AvaloniaXamlLoader.Load(this);

	public override void OnFrameworkInitializationCompleted()
	{
		if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return; // Visual designer requires partial initialisation.

		IList<IDataValidationPlugin> validators = GetDataValidators();
		validators.Remove(validators.OfType<DataAnnotationsValidationPlugin>().First()); // MVVM Community Toolkit already uses data annotations for validations.
		desktop.MainWindow = new MainView { DataContext = new MainViewModel() };

		return;

		[UnconditionalSuppressMessage("Trimming", "IL2026", Justification = $"Property accessors in {nameof(BindingPlugins)} do not seem to require unreferenced code despite the annotation.")]
		IList<IDataValidationPlugin> GetDataValidators() => BindingPlugins.DataValidators;
	}
}

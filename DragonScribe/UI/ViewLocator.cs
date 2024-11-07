using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DragonScribe.UI;

/// <summary>
/// Enables Avalonia to automatically render views based on view model references.
/// </summary>
class ViewLocator : IDataTemplate
{
	public Control? Build(object? viewModel)
	{
		if (viewModel is null) return null;

		Type? type = GetViewType();

		if (type == null) return new TextBlock { Text = $"The view corresponding to {viewModel.GetType().FullName} is missing." };

		Control view = CreateView();
		view.DataContext = viewModel;
		return view;

		[UnconditionalSuppressMessage("Trimming", "IL2057", Justification = "View types should never be trimmed.")]
		Type? GetViewType() => Type.GetType(viewModel.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal));

		[UnconditionalSuppressMessage("Trimming", "IL2072", Justification = $"Annotating {nameof(GetViewType)} with {nameof(DynamicallyAccessedMembersAttribute)} is unnecessarily strict.")]
		Control CreateView() => (Control)Activator.CreateInstance(type)!;
	}

	public bool Match(object? data) => data is ObservableObject;
}

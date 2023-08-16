// © CutleryDrawer.
// Licensed under the MIT license.

namespace SpoonTracker;

using PropertyChanged.SourceGenerator;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;
using System;

public partial class Goals : UserControl
{
	private readonly List<Goal> goalsList = new()
	{
		new Goal("2PM", "Doctor", -4),
		new Goal("8PM", "Meds", 0),
		new Goal("Laundry", -1),
		new Goal("Shower", -2),
		new Goal("Nap", 4),
	};

	[Notify] private ListCollectionView timedGoals;
	[Notify] private ListCollectionView anytimeGoals;
	[Notify] private double startingSpoons = 12;

	public Goals()
	{
		this.InitializeComponent();
		this.DataContext = this;

		this.timedGoals = new(this.goalsList);
		this.timedGoals.SortDescriptions.Add(new(nameof(Goal.HasTime), ListSortDirection.Descending));
		this.timedGoals.SortDescriptions.Add(new(nameof(Goal.Time), ListSortDirection.Ascending));
		this.timedGoals.SortDescriptions.Add(new(nameof(Goal.Name), ListSortDirection.Ascending));
		this.timedGoals.IsLiveSorting = true;
		this.timedGoals.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Goal.IsComplete)));
		this.timedGoals.IsLiveGrouping = true;
		this.timedGoals.Filter = new((g) => ((Goal)g).HasTime);
		this.timedGoals.IsLiveFiltering = true;

		this.anytimeGoals = new(this.goalsList);
		this.anytimeGoals.SortDescriptions.Add(new(nameof(Goal.HasTime), ListSortDirection.Descending));
		this.anytimeGoals.SortDescriptions.Add(new(nameof(Goal.Time), ListSortDirection.Ascending));
		this.anytimeGoals.SortDescriptions.Add(new(nameof(Goal.Name), ListSortDirection.Ascending));
		this.anytimeGoals.IsLiveSorting = true;
		this.anytimeGoals.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Goal.IsComplete)));
		this.anytimeGoals.IsLiveGrouping = true;
		this.anytimeGoals.Filter = new((g) => ((Goal)g).HasTime == false);
		this.anytimeGoals.IsLiveFiltering = true;
	}

	public double UsedSpoons
	{
		get
		{
			return 0;
		}
	}

	public double ReplenishedSpoons
	{
		get
		{
			return 0;
		}
	}

	public double RemainingSpoons
	{
		get
		{
			return 12;
		}
	}
}

public partial class Goal
{
	[Notify] private bool isComplete = false;
	[Notify] private string? time;
	[Notify] private string? name;
	[Notify] private string? description = "Lorem Ipsum Do a thing";
	[Notify] private double spoons;
	[Notify] private bool isRepeating = true;

	public Goal()
	{
	}

	public Goal(string time, string name, int spoons)
	{
		this.Time = time;
		this.Name = name;
		this.Spoons = spoons;
	}

	public Goal(string name, int spoons)
	{
		this.Name = name;
		this.Spoons = spoons;
	}

	public bool HasTime => this.time != null;
}
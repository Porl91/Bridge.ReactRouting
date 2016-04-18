﻿using System;
using Bridge.React;
using ProductiveRage.Immutable;
using ProductiveRage.ReactRouting.Tests.Support.Actions;
using ProductiveRage.ReactRouting.Tests.Support.RouteDataTypes;

namespace ProductiveRage.ReactRouting.Tests.Support
{
	public sealed class RootPlusHotelAndRestaurantSectionsNavigator : Navigator
	{
		private readonly Action _navigateToRoot;
		public RootPlusHotelAndRestaurantSectionsNavigator(IInteractWithBrowserRouting historyHandler, AppDispatcher dispatcher)
			: base(Set<NonBlankTrimmedString>.Empty, historyHandler, dispatcher)
		{
			if (dispatcher == null)
				throw new ArgumentNullException("dispatcher");

			AddRelativeRoute(
				Set<string>.Empty,
				new NavigateToRoot()
			);
			_navigateToRoot = () => NavigateTo();

			Hotels = new RootPlusDynamicIdItemPagesNavigator<Hotel>(
				Set.Of(new NonBlankTrimmedString("hotel")),
				historyHandler,
				dispatcher
			);
			foreach (var route in Hotels.Routes)
				AddRelativeRoute(route);

			Restaurants = new RootPlusDynamicIdItemPagesNavigator<Restaurant>(
				Set.Of(new NonBlankTrimmedString("restaurant")),
				historyHandler,
				dispatcher
			);
			foreach (var route in Restaurants.Routes)
				AddRelativeRoute(route);
		}

		public void Root() { _navigateToRoot(); }
		public RootPlusDynamicIdItemPagesNavigator<Hotel> Hotels { get; private set; }
		public RootPlusDynamicIdItemPagesNavigator<Restaurant> Restaurants { get; private set; }
	}
}

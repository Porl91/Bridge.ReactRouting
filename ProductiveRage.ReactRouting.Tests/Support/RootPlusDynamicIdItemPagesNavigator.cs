﻿using System;
using Bridge.React;
using ProductiveRage.Immutable;
using ProductiveRage.Immutable.Extensions;
using ProductiveRage.ReactRouting.Tests.Support.Actions;

namespace ProductiveRage.ReactRouting.Tests.Support
{
	public sealed class RootPlusDynamicIdItemPagesNavigator<T> : Navigator
	{
		private readonly Func<UrlDetails> _getRoot;
		private readonly Func<NonBlankTrimmedString, UrlDetails> _getItem;
		public RootPlusDynamicIdItemPagesNavigator(Set<NonBlankTrimmedString> parentSegments, IInteractWithBrowserRouting historyHandler, AppDispatcher dispatcher)
			: base(parentSegments, historyHandler, dispatcher)
		{
			if (dispatcher == null)
				throw new ArgumentNullException("dispatcher");

			AddRelativeRoute(
				Set<string>.Empty,
				new NavigateToRoot<T>()
			);
			_getRoot = () => GetPath();

			AddRelativeRoute(
				RouteBuilder.Empty.Fixed("item").String(),
				matchedValue => new NavigateToItem<T>(matchedValue.Item1)
			);
			_getItem = segment => GetPath("item", segment.Value);
		}
		public RootPlusDynamicIdItemPagesNavigator(IInteractWithBrowserRouting historyHandler, AppDispatcher dispatcher)
			: this(Set<NonBlankTrimmedString>.Empty, historyHandler, dispatcher) { }

		public UrlDetails Root() { return _getRoot(); }
		public UrlDetails Item(NonBlankTrimmedString name) { return _getItem(name); }
	}
}

﻿// <copyright file="IUnitOrderQueue.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.GetValue;

    public interface IUnitOrderQueue : IOrderIssuer
    {
        #region Public Properties

        bool IsWorking { get; set; }

        DataProvider<IUnitOrder> NewOrderQueued { get; }

        Notifier QueueEmpty { get; }

        GetValue<bool,bool> DrawOrder { get; set; }

        DataProvider<IUnitOrder> StartedExecution { get; }

        #endregion

        #region Public Methods and Operators

        void EnqueueOrder(IUnitOrder order);

        #endregion

        // IUnitOrder DequeueFirstOrder();
    }
}
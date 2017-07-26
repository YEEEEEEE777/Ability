﻿// <copyright file="AbilityModuleManager.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityModule
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.AbilityManager;
    using Ability.Core.AbilityModule.Metadata;
    using Ability.Core.AbilityModule.ModuleBase;

    using Ensage;
    using Ensage.Common.Menu;

    [Export(typeof(IAbilityModuleManager))]
    public class AbilityModuleManager : IAbilityModuleManager
    {
        #region Public Properties

        public List<IAbilityModule> ActiveModules { get; } = new List<IAbilityModule>();

        public bool GenerateMenu { get; }

        public DataProvider<IAbilityModule> ModuleActivated { get; set; } = new DataProvider<IAbilityModule>();

        #endregion

        #region Properties

        // [ImportMany]
        // internal IEnumerable<Lazy<IAbilityUnitModule, IAbilityUnitModuleMetadata>> UnitModules { get; set; }
        [Import(typeof(IAbilityManager))]
        internal Lazy<IAbilityManager> AbilityManager { get; set; }

        /// <summary>Gets or sets the ability utility modules.</summary>
        [ImportMany]
        internal IEnumerable<Lazy<IAbilityUtilityModule>> AbilityUtilityModules { get; set; }

        [ImportMany]
        internal IEnumerable<Lazy<IAbilityHeroModule, IAbilityHeroModuleMetadata>> HeroModules { get; set; }

        #endregion

        #region Public Methods and Operators

        public Menu GetMenu()
        {
            return null;
        }

        public void OnClose()
        {
            foreach (var module in this.ActiveModules)
            {
                module.OnClose();
            }
        }

        public void OnLoad()
        {
            Console.WriteLine("heroModules: " + this.HeroModules.Count());
            Console.WriteLine("utilityModules: " + this.AbilityUtilityModules.Count());

            foreach (var heroModule in this.HeroModules)
            {
                heroModule.Value.LocalHero = this.AbilityManager.Value.LocalHero;

                if (
                    heroModule.Metadata.HeroIds.Contains(
                        (uint)(this.AbilityManager.Value.LocalHero.SourceUnit as Hero).HeroId))
                {
                    if (!heroModule.Value.LoadOnGameStart)
                    {
                        continue;
                    }

                    Console.WriteLine("loading heroModule " + heroModule.Value.HeroName);
                    heroModule.Value.OnLoad();
                    this.ModuleActivated.Next(heroModule.Value);
                    this.ActiveModules.Add(heroModule.Value);

                    var unitModule = heroModule.Value as IAbilityUnitModule;
                    if (unitModule != null)
                    {
                        foreach (var valueControllableUnit in this.AbilityManager.Value.ControllableUnits)
                        {
                            unitModule.UnitAdded(valueControllableUnit.Value);
                        }

                        this.AbilityManager.Value.UnitAdded += args =>
                            {
                                if (args.AbilityUnit.IsCreep && args.AbilityUnit.SourceUnit.IsControllable)
                                {
                                    unitModule.UnitAdded(args.AbilityUnit);
                                }
                            };

                        this.AbilityManager.Value.UnitRemoved += args =>
                            {
                                if (args.AbilityUnit.IsCreep && args.AbilityUnit.SourceUnit.IsControllable)
                                {
                                    unitModule.UnitRemoved(args.AbilityUnit);
                                }
                            };
                    }
                }
            }

            // foreach (var unitModule in this.UnitModules)
            // {
            // unitModule.Value.LocalHero = this.AbilityManager.Value.LocalHero;
            // var hasUnit = false;

            // foreach (var valueControllableUnit in this.AbilityManager.Value.ControllableUnits)
            // {
            // if (unitModule.Metadata.UnitNames.Contains(valueControllableUnit.Value.Name))
            // {
            // unitModule.Value.UnitAdded(valueControllableUnit.Value);
            // hasUnit = true;
            // }
            // }

            // if (hasUnit)
            // {
            // unitModule.Value.OnLoad();
            // this.ModuleActivated.Next(unitModule.Value);
            // this.ActiveModules.Add(unitModule.Value);
            // Console.WriteLine("loading unitModule " + unitModule.Metadata.UnitNames.First() + "...");

            // this.AbilityManager.Value.UnitAdded += args =>
            // {
            // if (args.AbilityUnit.IsCreep && args.AbilityUnit.SourceUnit.IsControllable)
            // {
            // unitModule.Value.UnitAdded(args.AbilityUnit);
            // }
            // };

            // this.AbilityManager.Value.UnitRemoved += args =>
            // {
            // if (args.AbilityUnit.IsCreep && args.AbilityUnit.SourceUnit.IsControllable)
            // {
            // unitModule.Value.UnitRemoved(args.AbilityUnit);
            // }
            // };
            // }
            // else
            // {
            // this.AbilityManager.Value.UnitAdded += args =>
            // {
            // if (args.AbilityUnit.IsCreep && args.AbilityUnit.SourceUnit.IsControllable)
            // {
            // unitModule.Value.UnitAdded(args.AbilityUnit);

            // if (!hasUnit)
            // {
            // unitModule.Value.OnLoad();
            // this.ModuleActivated.Next(unitModule.Value);
            // this.ActiveModules.Add(unitModule.Value);
            // Console.WriteLine(
            // "loading unitModule " + unitModule.Metadata.UnitNames.First() + "...");
            // hasUnit = true;

            // this.AbilityManager.Value.UnitRemoved += args2 =>
            // {
            // if (args2.AbilityUnit.IsCreep && args2.AbilityUnit.SourceUnit.IsControllable)
            // {
            // unitModule.Value.UnitRemoved(args2.AbilityUnit);
            // }
            // };
            // }
            // }
            // };
            // }
            // }
            foreach (var abilityUtilityModule in this.AbilityUtilityModules)
            {
                abilityUtilityModule.Value.LocalHero = this.AbilityManager.Value.LocalHero;
                if (abilityUtilityModule.Value.LoadOnGameStart)
                {
                    abilityUtilityModule.Value.OnLoad();
                    this.ActiveModules.Add(abilityUtilityModule.Value);
                    this.ModuleActivated.Next(abilityUtilityModule.Value);
                }
            }
        }

        #endregion
    }
}
﻿// <copyright file="RunePosition.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData
{
    using System;

    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    public class RunePosition<T>
        where T : IAbilityRune
    {
        #region Fields

        private T currentRune;

        private float nextSpawnTime;

        private DrawText text;

        #endregion

        #region Constructors and Destructors

        public RunePosition(Vector3 position, string name, Team team)
        {
            this.Position = position;
            this.text = new DrawText { Shadow = true, Color = Color.White, Size = new Vector2(20 * HUDInfo.Monitor) };
            this.Name = name;
            this.Team = team;
        }

        #endregion

        #region Public Properties

        public string Name { get; }

        public Team Team { get; }

        public bool HasRune { get; set; }

        public T CurrentRune
        {
            get
            {
                return this.currentRune;
            }

            internal set
            {
                this.currentRune = value;
                if (this.currentRune == null)
                {
                    this.HasRune = false;
                    return;
                }

                this.NewRuneProvider.Next(this.currentRune);
                this.HasRune = true;
                this.currentRune.RuneDisposed.Subscribe(
                    () => this.HasRune = this.currentRune != null && !this.currentRune.Disposed);
            }
        }

        public DataProvider<T> NewRuneProvider { get; } = new DataProvider<T>();

        public float NextSpawnTime
        {
            get
            {
                return this.nextSpawnTime;
            }

            internal set
            {
                this.nextSpawnTime = value;
            }
        }

        public Vector3 Position { get; }

        #endregion

        #region Methods

        internal void Draw()
        {
            this.text.Position = Drawing.WorldToScreen(this.Position);
            if (this.text.Position.Equals(Vector2.Zero))
            {
                return;
            }

            if (this.HasRune)
            {
                this.text.Text = "[" + this.Name + "] current: " + this.CurrentRune?.TypeName + " next in "
                                 + Math.Ceiling(this.nextSpawnTime - Game.GameTime);
            }
            else
            {
                this.text.Text = "[" + this.Name + "] next in " + Math.Ceiling(this.nextSpawnTime - Game.GameTime);
            }

            this.text.Draw();
        }

        #endregion
    }
}
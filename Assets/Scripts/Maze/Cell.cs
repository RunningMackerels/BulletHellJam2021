using System;
using UnityEngine;
using RM;

namespace Maze
{
    public class Cell 
    {
        [Flags]
        public enum Wall
        {
            None = 0,
            North = 1 << 0,
            East = 1 << 1,
            West = 1 << 2,
            South = 1 << 3,
            NorthAndSouth = North | South,
            EastAndEst = East | West,
            All = NorthAndSouth | EastAndEst
        }

        public Wall Walls => _wall;
        private Wall _wall = Wall.None;

        public bool Visited { get; set; } = false;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Scripts.GameTriggers
{
    public abstract class PuzzleManager
    {
        public abstract void Puzzle0(Vector3 StartPosition);
        public abstract void Puzzle1(Vector3 StartPosition);
        public abstract void Puzzle2(Vector3 StartPosition);
        public abstract void Puzzle3(Vector3 StartPosition);
        public abstract void Puzzle4(Vector3 StartPosition);
        public abstract void Puzzle5(Vector3 StartPosition);
    }
}

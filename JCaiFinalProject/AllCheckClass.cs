using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using C3.XNA;

namespace JCaiFinalProject
{
    public class AllCheckClass
    {
        private int level = 0;

        public int Level { get { return level; } set { level = value; } }

        private bool isOpen = false;

        public bool IsOpen { get { return isOpen; } set { isOpen = value; } }

        private bool isPicked = false;       

        public bool IsPicked { get{ return isPicked; } set{ isPicked = value; } }

        private int lifeCount = 3;

        public int LifeCount { get { return lifeCount; } set { lifeCount = value; } }

        //private int emptyHeartCount = 0;

        //public int EmptyHeartCount { get{ return emptyHeartCount; }set{ emptyHeartCount = value; } }
    }
}

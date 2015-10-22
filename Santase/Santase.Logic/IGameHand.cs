using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase.Logic
{
    public interface IGameHand
    {
        void Start();
        public PlayerPosition Winner { get; }
    }
}

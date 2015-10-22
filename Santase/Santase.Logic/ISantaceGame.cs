using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase.Logic
{
    public interface ISantaceGame
    {
        void Start();
        int FirstPlayerTotalPoints { get; }
        int SecondPlayerTotalPoints { get; }
    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santase.Logic.Extensions
{
    public static class RandomProvider
    {
        private static Random instance;

        public static Random Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Random();
                }
                return instance;
            }
        }
    }
}

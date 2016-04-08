using Nop.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HGenealogy.Data
{
 
    public class HGStartUpTask : IStartupTask
    {
        public void Execute()
        {
            // Test
            string myTest = "Test";
        }

        public int Order
        {
            //ensure that this task is run first 
            get { return 0; }
        }
    }
}
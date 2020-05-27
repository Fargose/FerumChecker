using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Infrastructure
{
    public class OperationDetails
    {

        public OperationDetails(bool succedeed, string message, string prop, string warning = null)
        {
            Succedeed = succedeed;
            Messages = new List<string>() { message };
            Property = prop;
            Warnings = new List<string>() { warning };
        }
        public OperationDetails(bool succedeed, List<string> message, string prop, List<string> warning = null)
        {
            Succedeed = succedeed;
            Messages = message;
            Property = prop;
            Warnings = warning;
        }
        public bool Succedeed { get; private set; }
        public List<string> Messages { get; set; }

        public List<string> Warnings { get; private set; }
        public string Property { get; private set; }
        public object Data { get; private set; }

        public int RamFree { get; set; }

        public int? MemoryFree { get; set; }
    }
}

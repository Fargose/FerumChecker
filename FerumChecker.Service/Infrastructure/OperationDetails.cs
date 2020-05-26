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
            Message = message;
            Property = prop;
            Warning = warning;
        }
        public bool Succedeed { get; private set; }
        public string Message { get; private set; }

        public string Warning { get; private set; }
        public string Property { get; private set; }
        public object Data { get; private set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Infrastructure
{
    public class OperationDetails
    {
        public OperationDetails(bool succedeed, string message, string prop)
        {
            Succedeed = succedeed;
            Message = message;
            Property = prop;
        }
        public bool Succedeed { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }
        public object Data { get; private set; }
    }
}

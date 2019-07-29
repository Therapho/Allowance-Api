using System;
using System.Collections.Generic;
using System.Text;

namespace AllowanceFunctions
{
    public abstract class Record
    {
        public string Id { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }

    }
}

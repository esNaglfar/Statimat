using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using Xunit.Sdk;

namespace DBTransfer.Tests2
{
    public class TransferTests
    {
        [Fact]
        public void FirstTestMethod()
        {
            Assert.Matches("kek", "lol");
        }
    }
}

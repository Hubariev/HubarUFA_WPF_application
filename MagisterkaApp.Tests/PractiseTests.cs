using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Windows.Media;

namespace MagisterkaApp.Tests
{
    public class PractiseTests
    {
        [Fact]
        public void Test()
        {
            var foreground = new BrushConverter().ConvertFromString("#FFFFA500") as Brush;
            Console.WriteLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUrban.TestTask.Generator.Generators
{
    internal interface IGenerator
    {
        Task GenerateAsync(int sizeInBytes, string output);
    }
}

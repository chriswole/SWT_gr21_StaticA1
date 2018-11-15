using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_App2.Interfaces
{
    public interface IStringParser
    {
        string[] ParseDataString(string datastr, char separatorChar = ';');
    }
}

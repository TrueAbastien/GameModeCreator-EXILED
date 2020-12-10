using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModeCreator.API.Config.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Description : Attribute
    {
        public Description(string _text) => Text = _text;
        public string Text;
    }
}

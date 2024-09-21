using System;
using System.Collections.Generic;
using System.Text;
using EleCho.Compiler;
using EleCho.Yaml.Parsing.Syntaxes;

namespace EleCho.Yaml.Parsing.Grammars
{
    public class CharSequenceToYamlValue : Grammar<CharSequence, YamlValue>
    {
        public override bool CanConstruct(CharSequence input)
        {
            
        }

        public override YamlValue Construct(CharSequence input)
        {
            return 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace qplwork
{
    abstract class Param
    {
        protected string _paramName;
        protected string _paramShowedName;
        protected string _paramValue;
        protected string _paramComments = "";
        protected string _oldLine;

        public void SetName(string newName)
        {
            _paramName = newName;
        }

        public void SetShowedName(string newName)
        {
            _paramShowedName = newName;
        }

        public void SetValue(string newValue)
        {
            _paramValue = newValue;
        }

        public void AddComments(string newComments)
        {
            _paramComments += newComments;
        }

        public string GetName()
        {
            return _paramName;
        }

        public void SetOldLine(string line)
        {
            _oldLine = line;
        }

        public string GetOldLine()
        {
            return _oldLine;
        }

        public string GetValue()
        {
            return _paramValue;
        }

        public abstract string GetNewValue();

        public abstract int DrawParam(int y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace GameTypes
{
    public struct Name
    {
        public string firstName;
        public string lastName;

        public Name(string first, string last) 
        {
            this.firstName = first;
            this.lastName = last;
        }
        public Name(string first)
        {
            this.firstName = first;
            this.lastName = "";
        }
        public Name(string fullName, bool parse) 
        {
            if (!parse)
            {
                this.firstName = fullName;
                this.lastName = "";
            }
            else
            {
                this.firstName = "";
                this.lastName = "";
                SplitName(fullName);
            }

        }
        public string CombineName()
        {
            return this.firstName + " " + this.lastName;
        }

        private void SplitName(string fullName)
        {
            bool secondName = false;
            foreach (char c in fullName)
            {
                if(c == ' ')
                {
                    secondName = true;
                    continue;
                } 
                if (!secondName)
                    firstName += c;
                else
                    lastName += c;
            }
        }
        private void CapitalizeNames()
        {
            this.firstName[0].ToString().ToUpper();
            this.lastName[0].ToString().ToUpper();
        }
    }

}
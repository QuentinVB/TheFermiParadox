using PlanetBuilder.Interfaces;
using System.Collections.Generic;

namespace PlanetBuilder
{
    public class PlanetaryModifer : List<IPlanetaryFeature>
    {
        public override string ToString()
        {
            string answer = "Caracteristics :\n";
            if(this.Count!=0)
            {
                foreach (IPlanetaryFeature item in this)
                {
                    answer += item.ToString() + "\n";
                }
                return answer ;
            }
            return answer + "none";
        }

    }
}


using covid_data.Entities;

namespace covid_data.Data
{
    /// <summary>
    /// This class is meant to be used as a dummy value, so I can add my name to lists of type CovidData.
    /// </summary>
    public class KarlObject : CovidData
    {
        public KarlObject()
        {
            prname = "Karl Rezansoff";
            prnameFR = "Displaying my name as per assignment requirments";
        }

        public override string toString()
        {
            return prname + "\n" + prnameFR + "\n";
        }
    }
}
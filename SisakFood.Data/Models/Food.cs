using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SisakFood.Data.Models
{
    public class Food
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Nutrition Nutrients { get; set; }

        [JsonIgnore]
        public IEnumerable<double> QuantitiesList
        {

            get
            {
                if (Quantities == null)
                    return Enumerable.Empty<double>();
                return Quantities.Split(',')
                    .ToList()
                    .Where(x =>
                    {
                        double o;
                        return double.TryParse(x, out o);
                    })
                    .Select(x => double.Parse(x));
            }
        }

        public string Quantities { get; set; }
    }
}
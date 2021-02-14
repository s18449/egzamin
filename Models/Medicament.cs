using System.Collections.Generic;

namespace egzamin.Models
{
    public class Medicament
    {
        public string IdMedicament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public List<Prescription> PrescriptionList { get; set; }

    }
}
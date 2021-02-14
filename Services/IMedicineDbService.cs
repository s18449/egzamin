using egzamin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace egzamin.Services
{
    public interface IMedicineDbService
    {
        Medicament GetMedicament(string idMedicament);
        void DeletePatient(string idPatient);
    }
}
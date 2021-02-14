using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using egzamin.Models;
using egzamin.Services;

namespace egzamin.Controllers
{
    [ApiController]
    public class HealthcareController : ControllerBase
    {

        private IMedicineDbService _service;

        public HealthcareController(IMedicineDbService service)
        {
            this._service = service;
        }

        [HttpGet("{idMedicament}")]
        [Route("api/medicaments")]
        public IActionResult GetMedicament(string idMedicament)
        {

            Medicament medicament = _service.GetMedicament(idMedicament);

            if(medicament == null)
            {
                return BadRequest("Medicament with id " + idMedicament + " does not exist");
            }

            return Ok(medicament);

        }

        [HttpDelete("{idPatient}")]
        [Route("api/patients")]
        public IActionResult DeletePatient(string idPatient)
        {

            _service.DeletePatient(idPatient);

            return Ok();

        }


    }
}

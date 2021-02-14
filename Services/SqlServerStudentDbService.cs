using egzamin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace egzamin.Services
{
    public class SqlServerStudentDbService : IMedicineDbService
    {


        public Medicament GetMedicament(string idMedicament)
        {
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18449;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();


                com.CommandText = "SELECT IdMedicament, Name, Description, Type FROM medicament  WHERE idmedicament = @idmedicament";
                com.Parameters.AddWithValue("idmedicament", idMedicament);

                var dr = com.ExecuteReader();

                Medicament medicament = new Medicament();
                var prescriptions = new List<Prescription>();

                if (dr.Read())
                {
                    medicament.IdMedicament = dr["IdMedicament"].ToString();
                    medicament.Name = dr["Name"].ToString();
                    medicament.Description = dr["Description"].ToString();
                    medicament.Type = dr["Type"].ToString();

                }
                else
                {
                    return null;
                }

                com.CommandText = "SELECT IdPrescription, Date, DueDate FROM medicament" +
                   "INNER JOIN prescription_medicament ON medicament.IdMedicament = prescription_medicament.IdMedicament " +
                   "INNER JOIN prescription ON prescription_medicament.IdPrescription = prescription.IdPresciprion " +
                   "WHERE idmedicament = @idmedicament";
                com.Parameters.AddWithValue("idmedicament", idMedicament);

                var dr1 = com.ExecuteReader();


                while (dr.Read()) {
                    Prescription prescription = new Prescription();

                    prescription.IdPrescription = dr["IdPrescription"].ToString();
                    prescription.Date = dr1.GetDateTime("Date");
                    prescription.DueDate = dr1.GetDateTime("DueDate");

                    prescriptions.Add(prescription);
                }

                prescriptions.Sort((x, y) => y.Date.CompareTo(x.Date));

                medicament.PrescriptionList = prescriptions;

                return medicament;
            }

        }

        public void DeletePatient(string idPatient)
        {

            try
            {
                using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18449;Integrated Security=True"))
                using (var com = new SqlCommand())
                {
                    com.Connection = con;

                    con.Open();
                    var tran = con.BeginTransaction();


                    com.CommandText = "DELETE FROM patient WHERE idpatient = @idpatient";
                    com.Parameters.AddWithValue("idpatient", idPatient);
                    com.ExecuteNonQuery();

                }
            } catch (Exception e)
            {
                
            }

        }


    }
}
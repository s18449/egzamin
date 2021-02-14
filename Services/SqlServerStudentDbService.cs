using egzamin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Wyklad5.Services
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

                try
                {

                    com.CommandText = "select * from medicament where idmedicament = @idmedicament";
                    com.Parameters.AddWithValue("idmedicament", idMedicament);

                    var dr1 = com.ExecuteReader();

                    if (!dr1.Read())
                    {
                        throw new HttpResponseException()
                    }


                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }
            }

            return Ok(ble);
        }

        public void DeletePatient(string idPatient)
        {
            var st = new Student();
            st.FirstName = request.FirstName;

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18449;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();

                try
                {

                    com.CommandText = "select IdStudies from studies where indexnumber = @index";

                    com.Parameters.AddWithValue("index", request.IndexNumber);

                    var dr = com.ExecuteReader();

                    if (dr.Read())
                    {
                        tran.Rollback();
                        return BadRequest("Numer indeksu nie jest unikatowy");

                    }

                    com.CommandText = "select IdStudies from studies where name = @name";

                    com.Parameters.AddWithValue("name", request.Studies);

                    var dr1 = com.ExecuteReader();

                    if (!dr1.Read())
                    {
                        tran.Rollback();
                        return BadRequest("Studia nie istnieja");

                    }

                    int idstudies = (int)dr1["IdStudies"];

                    com.CommandText = "INSERT INTO Student(IndexNumber, FirstName) VALUES(@Index, @Fname)";

                    com.Parameters.AddWithValue("index", request.IndexNumber);

                    com.ExecuteNonQuery();

                    tran.Commit();

                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }



            }

            throw new NotImplementedException();
        }



      
    }
}
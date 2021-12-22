using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using MvcHospitalCrudLinq.Models;

namespace MvcHospitalCrudLinq.Data
{
    public class HospitalesContext
    {
        private string cadenaconexion;
        private SqlDataAdapter adhospitales;
        private DataTable TablaHospitales;
        private SqlConnection cn;
        private SqlCommand com;

        public HospitalesContext()
        {
            this.cadenaconexion = @"";
            this.cn = new SqlConnection(cadenaconexion);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.RefreshData();
        }

        private void RefreshData()
        {
            string sql = "select * from hospital";
            this.adhospitales = new SqlDataAdapter(sql, cadenaconexion);
            this.TablaHospitales = new DataTable();
            this.adhospitales.Fill(this.TablaHospitales);
        }

        public List<Hospital> GetHospitales()
        {
            var consulta = from datos in this.TablaHospitales.AsEnumerable()
                           select datos;
            List<Hospital> hospitales = new List<Hospital>();
            foreach (var row in consulta)
            {
                Hospital hospital = new Hospital();
                hospital.IdHospital = row.Field<int>("HOSPITAL_COD");
                hospital.Nombre = row.Field<string>("NOMBRE");
                hospital.Direccion = row.Field<string>("DIRECCION");
                hospital.Telefono = row.Field<string>("TELEFONO");
                hospital.Camas = row.Field<int>("NUM_CAMA");
                hospitales.Add(hospital);
            }
            return hospitales;
        }

        public Hospital FindHospital(int idhospital)
        {
            var consulta = from datos in this.TablaHospitales.AsEnumerable()
                           where datos.Field<int>("HOSPITAL_COD") == idhospital
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                var row = consulta.First();
                Hospital hospital = new Hospital();
                hospital.IdHospital = row.Field<int>("HOSPITAL_COD");
                hospital.Nombre = row.Field<string>("NOMBRE");
                hospital.Direccion = row.Field<string>("DIRECCION");
                hospital.Telefono = row.Field<string>("TELEFONO");
                hospital.Camas = row.Field<int>("NUM_CAMA");
                return hospital;
            }
        }

        public int InsertHospital(int idhospital, string nombre
            , string direccion, string telefono, int camas)
        {
            string sql = "insert into hospital values (@HOSPITAL_COD, @NOMBRE, "
                + "@DIRECCION, @TELEFONO, @CAMAS)";
            this.com.Parameters.AddWithValue("@HOSPITAL_COD", idhospital);
            this.com.Parameters.AddWithValue("@NOMBRE", nombre);
            this.com.Parameters.AddWithValue("@DIRECCION", direccion);
            this.com.Parameters.AddWithValue("@TELEFONO", telefono);
            this.com.Parameters.AddWithValue("@CAMAS", camas);
            this.com.CommandText = sql;
            this.com.CommandType = CommandType.Text;
            this.cn.Open();
            int results = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return results;
        }

        public int UpdateHospital(int idhospital, string nombre, string direccion
            , string telefono, int camas)
        {
            string sql = "update hospital set nombre=@NOMBRE, direccion=@DIRECCION"
                + ", telefono=@TELEFONO, NUM_CAMA=@CAMAS "
                + " WHERE HOSPITAL_COD=@HOSPITAL";
            this.com.Parameters.AddWithValue("@NOMBRE", nombre);
            this.com.Parameters.AddWithValue("@DIRECCION", direccion);
            this.com.Parameters.AddWithValue("@TELEFONO", telefono);
            this.com.Parameters.AddWithValue("@CAMAS", camas);
            this.com.Parameters.AddWithValue("@HOSPITAL", idhospital);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int results = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return results;
        }

        public int DeleteHospital(int idhospital)
        {
            string sql = "delete from hospital where hospital_cod=@IDHOSPITAL";
            this.com.Parameters.AddWithValue("@IDHOSPITAL", idhospital);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int results = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return results;
        }
    }
}

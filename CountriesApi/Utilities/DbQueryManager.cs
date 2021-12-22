using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CountriesApi.Models;

namespace CountriesApi.Utilities
{
    public class MongoDbDataService : IDataService
    {
        public IEnumerable<Country> GetAll()
        {
            throw new NotImplementedException();
        }

        public Country Get(string id)
        {
            throw new NotImplementedException();
        }
    }
    public class DbQueryManager : IDataService
    {
        private static readonly string ConnectionString = @"Data Source=ANGELO\SQLEXPRESS;Initial Catalog=mydatabase;Integrated Security=True";

        public IEnumerable<Country> GetAll()
        {
            List<Country> countryList = new();

            var queryString = "SELECT *\n" +
                                 "FROM tb_country";
            try
            {
                using SqlConnection con = new(ConnectionString);
                SqlCommand cmd = new(queryString, con);

                con.Open();
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Country country = new()
                    {
                        Id = Convert.ToInt32(rdr[0]),
                        Name = Convert.ToString(rdr[1]),
                        Active = Convert.ToBoolean(rdr[2])
                    };
                    countryList.Add(country);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAll error occurred: {ex.Message}");
            }
            return countryList;
        }

        public Country Get(string id)
        {
            return GetAll().Where(c => c.Id == Convert.ToInt32(id)).FirstOrDefault();
        }

        public static Country GetA(string id)
        {
            List<Country> countryList = new();
            var queryString = "SELECT *\n" +
                              "FROM tb_country\n" + 
                              $"WHERE id={id}";
            try
            {
                using SqlConnection con = new(ConnectionString);
                SqlCommand cmd = new(queryString, con);

                con.Open();
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Country country = new()
                    {
                        Id = Convert.ToInt32(rdr[0]),
                        Name = Convert.ToString(rdr[1]),
                        Active = Convert.ToBoolean(rdr[2])
                    };
                    countryList.Add(country);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAll error occurred: {ex.Message}");
            }
            return countryList.FirstOrDefault();    // Note:    Returns default value if list is empty;
                                                    //          This default value is taken with regard to List type <T>;
                                                    //          Objects (e.g. country) are null by default;
        }

        public static void Post(Country country)
        {
            var active = country.Active ? "1" : "0";
            var queryString = $"INSERT INTO tb_country (country, active)\n" +
                                 $"VALUES(N\'{country.Name}\', {active})";

            try
            {
                using SqlConnection con = new(ConnectionString);
                SqlCommand cmd = new(queryString, con);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Post error occurred: {ex.Message}");
            }
            country.Id = GetNewestId();
        }

        private static int GetNewestId()
        {
            var queryString = "SELECT TOP 1 id\n" + 
                          "FROM tb_country\n" + 
                          "ORDER BY id DESC";
            try
            {
                using SqlConnection con = new(ConnectionString);
                SqlCommand cmd = new(queryString, con);

                con.Open();
                var rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    return Convert.ToInt32(rdr[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetNewestId error occurred: {ex.Message}");
            }

            return -1;
        }

        public static void Put(string id, Country country)
        {
            var active = country.Active ? "1" : "0";
            var queryString = $"UPDATE tb_country\n" +
                              $"SET country=N\'{country.Name}\', active={active}\n" +
                              $"WHERE id={id}";

            try
            {
                using SqlConnection con = new(ConnectionString);
                SqlCommand cmd = new(queryString, con);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Put error occurred: {ex.Message}");
            }
            country.Id = Convert.ToInt32(id);
        }

        public static void Delete(string id)
        {
            string queryString = $"DELETE FROM tb_country WHERE id={id}";

            try
            {
                using SqlConnection con = new(ConnectionString);
                SqlCommand cmd = new(queryString, con);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete error occurred: {ex.Message}");
            }
        }
    }
}

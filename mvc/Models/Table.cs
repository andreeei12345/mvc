using System;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace mvc.Models
{
    public class Table
    {
        public int id { get; set; }
        public string FIO { get; set; }
        public DateTime date { get; set; }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<Table> Users { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseMySql("server=localhost;user=root;password=;database=test;Convert Zero Datetime=true;",
                    new MySqlServerVersion(new Version(8, 2, 12)));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
           
        }
        
         public void addrequest()
        {
            WebRequest request = WebRequest.Create("https://andrei123.okdesk.ru/api/v1/employees/list?api_token=b0c3624630cc03f9b77f4d6f1ab2a5f13e18b0a9&page[direction]=forward");
            WebResponse respons = request.GetResponse();
            string line = " ";
            using (StreamReader stream = new StreamReader(respons.GetResponseStream()))
            {
                if ((line = stream.ReadLine()) != null)
                {
                     Database.ExecuteSqlRaw("DELETE FROM users WHERE 1");
                    dynamic jsonDe = JsonConvert.DeserializeObject(line);
                    foreach (var a in jsonDe)
                    {
                        string name = a.last_name + " " + a.first_name + " " + a.patronymic;                     
                        if (a.last_seen == null) a.last_seen = new DateTime();
                        Table user1 = new Table { id = a.id, FIO = name, date =  Convert.ToDateTime(a.last_seen) };
                        Users.AddRange(user1);
                    }
                    SaveChanges();
                } 
                         
            }          
                       
        }

    }
    
}
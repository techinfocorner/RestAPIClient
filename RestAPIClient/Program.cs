using System;
using System.Net;
using System.Net.Http;

namespace RestAPIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Invoke GET method
            string response = InvokeGet();
            Console.WriteLine(response);
            Console.ReadLine();

            ////Invoke POST method
            //string response = InvokePost();
            //Console.WriteLine("New Employee Id: " + response);
            //Console.ReadLine();

            ////Invoke PUT method
            //string response = InvokePut();
            //Console.WriteLine(response);
            //Console.ReadLine();

            //Invoke DELETE method
            //string response = InvokeDelete();
            //Console.WriteLine(response);
            //Console.ReadLine();
        }

        static string InvokeGet()
        {
            Uri uri = new Uri("https://localhost:44358/api/employees/getemployees");
            //Uri uri = new Uri("https://localhost:44358/api/employees/getemployee?id=1&name=John");

            var response = GetResource(uri);
            return response;
        }

        static string GetResource(Uri uri)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "basic YWRtaW46YWRtaW4=");
                HttpResponseMessage result = client.GetAsync(uri).Result;

                if (result.IsSuccessStatusCode)
                {
                    response = result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    response = Convert.ToInt32(result.StatusCode).ToString() + ":" + result.ReasonPhrase;
                }
            }
            return response;
        }

        static string InvokePost()
        {
            Uri uri = new Uri("https://localhost:44358/api/employees/addemployee");

            Employee objEmp = new Employee { Name = "Raj", Country = "United States" };

            var response = PostResource(uri, objEmp);
            return response;
        }

        static string PostResource(Uri uri, Employee emp)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "basic YWRtaW46YWRtaW4=");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                // client.DefaultRequestHeaders.Add("Content-type", "application/json");

                HttpResponseMessage result = client.PostAsJsonAsync(uri, emp).Result;

                if (result.StatusCode == HttpStatusCode.Created)
                {
                    response = result.Content.ReadAsStringAsync().Result;
                }
            }
            return response;
        }

        static string InvokePut()
        {
            Uri uri = new Uri("https://localhost:44358/api/employees/updateemployee");

            Employee objEmp = new Employee { Id = 1, Name = "David", Country = "United States" };

            var response = UpdateResource(uri, objEmp);
            return response;
        }

        static string UpdateResource(Uri uri, Employee emp)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "basic YWRtaW46YWRtaW4=");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                // client.DefaultRequestHeaders.Add("Content-type", "application/json");

                HttpResponseMessage result = client.PutAsJsonAsync(uri, emp).Result;

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    response = result.Content.ReadAsStringAsync().Result;
                }
            }
            return response;
        }

        static string InvokeDelete()
        {
            Uri uri = new Uri("https://localhost:44358/api/employees/deleteemployee?id=5");

            var response = DeleteResource(uri);
            return response;
        }

        static string DeleteResource(Uri uri)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "basic YWRtaW46YWRtaW4=");
                HttpResponseMessage result = client.DeleteAsync(uri).Result;
                response = " Status Code: " + result.StatusCode.ToString();
            }
            return response;
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}

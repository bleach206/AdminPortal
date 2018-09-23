using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Model;
using Model.Interface;
using Repository.Interface;

using Newtonsoft.Json.Linq;

namespace Repository
{
    public class EmployeeRepositorySolr : IEmployeeRepositorySolr<IEmployee>
    {
        #region Fields

        private readonly string _url;
        #endregion

        #region Constructor

        public EmployeeRepositorySolr(string url) => _url = url;        
        #endregion

        #region Mothods

        public async Task<IEnumerable<IEmployee>> GetEmployee()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(_url + "select?q=*:*");
                response.EnsureSuccessStatusCode();

                var employees = await response.Content.ReadAsStringAsync();
                return await Task.Run(() => JObject.Parse(employees)["response"]["docs"].ToObject<Employee[]>().ToList());           
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<IEmployee> GetEmployeeByName(string name)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(_url + "select?q=FullName:" + name);
                request.ContentType = "application/json; charset=utf-8";
                var json = string.Empty;
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    json = sr.ReadToEnd();
                }

                if (!string.IsNullOrWhiteSpace(json))
                {
                    var employeObject = JObject.Parse(json);
                    var employeList = employeObject["response"]["docs"].ToObject<Employee[]>().ToList();
                    return employeList;
                }

                return null;
            }
            catch (WebException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

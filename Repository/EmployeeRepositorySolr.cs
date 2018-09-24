using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<IEmployee>> GetEmployee() => await Get(_url + "select?q=*:*");


        public async Task<IEnumerable<IEmployee>> GetEmployeeByName(string name) => await Get(_url + "select?q=FullName:" + name);        
        #endregion

        #region Private Methods

        private async Task<IEnumerable<IEmployee>> Get(string url)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(url);
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
        #endregion
    }
}

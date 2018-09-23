using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

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

        public IEnumerable<IEmployee> GetEmployee()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(_url + "select?q=*:*");
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

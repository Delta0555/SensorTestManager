using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using SensorData.ServiceClasses;
using System;
using System.Collections.Generic;

namespace SensorData.ServiceProxy {
    public class SensorDataServiceProxy {
        private readonly string __sConnectionBase;
        private RestClient __pProxy;
        private const string __sTestParameterRoute = "TestParameter";
        private const string __sSensorRoute = "Sensor";
        private const string __sTestRunRoute = "TestRun";
        private const string __sFullTestRunRoute = "TestRun" + "/Full";
        private const string __sFullSensorRoute = __sSensorRoute + "/Full";
        
        public SensorDataServiceProxy(string sAddress, string sUser, string sPW) {
            __sConnectionBase = sAddress;
            __pProxy = new RestClient(sAddress);
            __pProxy.Authenticator = new HttpBasicAuthenticator(sUser, sPW);            
        }

        private T ExecuteServiceCall<T>(string sRoute, RestSharp.Method pMethod, object pPayload = null) {
            T pReturnValue;

            RestRequest pRequest = new RestRequest(sRoute, pMethod);
            if (pPayload != null) {
                pRequest.AddJsonBody(JsonConvert.SerializeObject(pPayload));
            }

            IRestResponse pResponse = __pProxy.Execute(pRequest, pMethod);

            pReturnValue = JsonConvert.DeserializeObject<T>(pResponse.Content);

            return pReturnValue;
        }

        public TestParameter GetTestParameterFor(long nID) {
            TestParameter pReturnValue = null;
            try {
                string sRequest = string.Format("{0}/{1}", __sTestParameterRoute, nID);
                RestRequest pRequest = new RestRequest(sRequest, Method.GET);

                IRestResponse pResponse = __pProxy.Execute(pRequest, Method.GET);

                pReturnValue = JsonConvert.DeserializeObject<TestParameter>(pResponse.Content);
            }
            catch (Exception e) {
                //todo: Add logging
            }

            return pReturnValue;
        }

        public List<TestParameter> GetAllTestParameters() {
            //todo: Convert this to make use of paging with lastid and pagesize

            List<TestParameter> pReturnValue = null;

            try {
                string sRequest = __sTestParameterRoute;
                RestRequest pRequest = new RestRequest(sRequest, Method.GET);

                IRestResponse pResponse = __pProxy.Execute(pRequest, Method.GET);

                pReturnValue = JsonConvert.DeserializeObject<List<TestParameter>>(pResponse.Content);
            }
            catch (Exception e) {
                //todo: Add logging
            }

            return pReturnValue ?? new List<TestParameter>(0);
        }

        public bool UpdateTestParameter(TestParameter pParam) {
            bool bSuccess = false;

            try {
                string sRequest = __sTestParameterRoute;
                RestRequest pRequest = new RestRequest(sRequest, Method.PUT);
                pRequest.AddJsonBody(JsonConvert.SerializeObject(pParam));

                IRestResponse pResponse = __pProxy.Execute(pRequest, Method.PUT);

                bSuccess = JsonConvert.DeserializeObject<Boolean>(pResponse.Content);
            }
            catch (Exception e) {
                //todo: add Logging
                bSuccess = false;
            }

            return bSuccess;
        }

        public long AddTestParameter(TestParameter pParam) {
            long nNewID = long.MinValue;

            try {
                string sRequest = __sTestParameterRoute;
                RestRequest pRequest = new RestRequest(sRequest, Method.POST);
                pRequest.AddJsonBody(JsonConvert.SerializeObject(pParam));
                

                IRestResponse pResponse = __pProxy.Execute(pRequest, Method.POST);

                nNewID = JsonConvert.DeserializeObject<Int64>(pResponse.Content);
            }
            catch (Exception e) {
                //todo: Add logging
            }

            return nNewID;
        }       

        public bool AddSensor(Sensor pSensor) {
            bool bSuccess = false;

            try {
                string sRequest = __sSensorRoute;
                RestRequest pRequest = new RestRequest(sRequest, Method.POST);
                pRequest.AddJsonBody(JsonConvert.SerializeObject(pSensor));

                IRestResponse pResponse = __pProxy.Execute(pRequest, Method.POST);                
                bSuccess = JsonConvert.DeserializeObject<Boolean>(pResponse.Content);
            }
            catch (Exception e) {
                //todo: Add logging
                bSuccess = false;
            }

            return bSuccess;
        }




        

        public bool UpdateSensor(Sensor pSensor) {
            bool bSuccess = false;

            try {
                bSuccess = ExecuteServiceCall<bool>(__sSensorRoute, Method.PUT, pSensor);
            }
            catch (Exception e) {
                //todo: Add logging
                bSuccess = false;
            }

            return bSuccess;
        }


        public List<FullSensor> GetAllFullSensors() {
            List<FullSensor> pSensors = null;

            try {
                string sRequest = __sFullSensorRoute;
                RestRequest pRequest = new RestRequest(sRequest, Method.GET);
                IRestResponse pResponse = __pProxy.Execute(pRequest, Method.GET);

                pSensors = JsonConvert.DeserializeObject<List<FullSensor>>(pResponse.Content);
            }
            catch(Exception e) {

            }

            return pSensors ?? new List<FullSensor>(0);
        }

        public List<FullTestRun> GetAllFullTestRuns() {
            List<FullTestRun> pReturnValue = null;

            try {
                pReturnValue = ExecuteServiceCall<List<FullTestRun>>(__sFullTestRunRoute, Method.GET);
            }
            catch(Exception e) {
               
            }

            return pReturnValue ?? new List<FullTestRun>(0);
        }
    }
}

using System.Net.Http;
using System.Net.Http.Headers;

namespace Demo.DDD.SystemTests
{
    public static class AuthenticationHelper
    {
        public static void Authenticate(HttpClient httpClient)
        {
            var token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IlpqUXhNREUwTUdGbU1USTJORFl5WWpRNE56TmxZVE0xWkdZellXUXdZV0UxT0dRMFpUUmlPUSIsIng1dCI6IlpqUXhNREUwTUdGbU1USTJORFl5WWpRNE56TmxZVE0xWkdZellXUXdZV0UxT0dRMFpUUmlPUSJ9.eyJuYmYiOjE2MTg0Njc1OTUsImV4cCI6MTYxODQ2NzcxNSwiaWF0IjoxNjE4NDY3NTk1LCJpc3MiOiJodHRwczovL2lhbS10ZXN0LmRhbnNrZW5ldC5uZXQvIiwic3ViIjoiTlQwMDAxXFxEMDAzMjY0Iiwic2NvcGUiOlsiZlBEZW55TG9nb25Mb2NhbGx5Il0sImF1ZCI6WyJodHRwczovL2lhbS10ZXN0LmRhbnNrZW5ldC5uZXQvaHR0cHM6Ly93d3cuZGFuc2tlYmFuay5jb20vIl0sInJ1bnRpbWVDaGFubmVsIjoiYnVzaW5lc3NvbmxpbmUifQ.PxxAdmPUrzMOelIQ1GeDEYWpER7JeGPxoCDHu-8WlsVow8Ke78JFIvlqT0BUfVwCJQMJQK4_qKHLjlrm2zV_4vF9QiqPDJM5ghEBSbBg7mNZaTM64PR4qiQngA0Hv0bCyzccKhjY1ggljhSBnWyV-Vjf2TF3XVinNQd03RnqdP4vAdj20zin4ScSuhZaRVhVjD7e5OVmdP28yjbA6w7dtaK5s9-oOMIfJtCkdycIt6sCdWIKWhJ5i7gTbgICVZhig8gkicFL18YXGTXTQLQU026OKBATJMaRlhSSjU1YG7Qsfhvi23B_9krzdjru22iFc4E1dO1absdklsaRx8WnMw";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}

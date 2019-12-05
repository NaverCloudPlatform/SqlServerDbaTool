using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CsLib;

namespace lazylog
{
    class Certification
    {

        public bool Bind(string path, string pfx, string pfxpassword, string cert, string port)
        {
            try
            {
                string pfxFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path, pfx);
                string certFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path, cert);

                DeleteCertification(pfxFileName
                    , StoreName.My
                    , StoreLocation.LocalMachine
                    , pfxpassword
                    , X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);

                DeleteCertification(certFileName
                    , StoreName.Root
                    , StoreLocation.LocalMachine);

                InstallCertification(pfxFileName
                    , StoreName.My
                    , StoreLocation.LocalMachine
                    , pfxpassword
                    , X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);

                InstallCertification(certFileName
                    , StoreName.Root
                    , StoreLocation.LocalMachine);

                CertificationNetBind(certFileName, port);
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        private void InstallCertification(string certFileName, StoreName storeName, StoreLocation storeLocaion, string password, X509KeyStorageFlags x509KeyStorageFlags)
        {
            X509Store store = new X509Store(storeName, storeLocaion);
            try
            {
                bool Exists = false;
                X509Certificate2 cert = new X509Certificate2(certFileName, TranString.convertToSecureString(password), x509KeyStorageFlags);
                store.Open(OpenFlags.ReadWrite);
                foreach (X509Certificate2 storeCert in store.Certificates)
                {
                    if (storeCert.Thumbprint.Equals(cert.Thumbprint, StringComparison.OrdinalIgnoreCase))
                    {
                        Exists = true;
                        break;
                    }
                }
                if (!Exists)
                {
                    store.Add(cert);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                store.Close();
            }
        }

        private void InstallCertification(string certFileName, StoreName storeName, StoreLocation storeLocaion)
        {
            X509Store store = new X509Store(storeName, storeLocaion);
            try
            {
                bool Exists = false;
                X509Certificate2 cert = new X509Certificate2(certFileName);
                store.Open(OpenFlags.ReadWrite);
                foreach (X509Certificate2 storeCert in store.Certificates)
                {
                    if (storeCert.Thumbprint.Equals(cert.Thumbprint, StringComparison.OrdinalIgnoreCase))
                    {
                        Exists = true;
                        break;
                    }
                }
                if (!Exists)
                {
                    store.Add(cert);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                store.Close();
            }
        }

        private void DeleteCertification(string certFileName, StoreName storeName, StoreLocation storeLocaion, string password, X509KeyStorageFlags x509KeyStorageFlags)
        {
            X509Store store = new X509Store(storeName, storeLocaion);
            try
            {
                X509Certificate2 cert = new X509Certificate2(certFileName, TranString.convertToSecureString(password), x509KeyStorageFlags);
                store.Open(OpenFlags.ReadWrite);
                foreach (X509Certificate2 storeCert in store.Certificates)
                {
                    if (storeCert.Thumbprint.Equals(cert.Thumbprint, StringComparison.OrdinalIgnoreCase))
                    {
                        store.Remove(cert);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                store.Close();
            }
        }

        private void DeleteCertification(string certFile, StoreName storeName, StoreLocation storeLocaion)
        {
            X509Store store = new X509Store(storeName, storeLocaion);
            try
            {
                X509Certificate2 cert = new X509Certificate2(certFile);
                store.Open(OpenFlags.ReadWrite);
                foreach (X509Certificate2 storeCert in store.Certificates)
                {
                    if (storeCert.Thumbprint.Equals(cert.Thumbprint, StringComparison.OrdinalIgnoreCase))
                    {
                        store.Remove(cert);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                store.Close();
            }
        }



        private string GetCertHash(string certFileName)
        {
            using (X509Certificate2 cert = new X509Certificate2(certFileName))
                return cert.Thumbprint.ToLower();
        }

        private string GetAssemblyGuid()
        {
            var assembly = typeof(Program).Assembly;
            var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            var id = attribute.Value;
            return id.ToLower();
        }

        private bool CertificationNetBind(string certFileName, string port)
        {
            StringBuilder sbPsiResults = new StringBuilder();
            ProcessStartInfo psi = new ProcessStartInfo() { CreateNoWindow = true, UseShellExecute = false, RedirectStandardOutput = true };
            bool isBounded = false;

            // SHOW
            psi.FileName = "netsh";
            psi.Arguments = $"http show sslcert ipport=0.0.0.0:{port}";
            sbPsiResults.Clear();
            Process procHttpShow = Process.Start(psi);
            while (procHttpShow != null && !procHttpShow.StandardOutput.EndOfStream)
                sbPsiResults.Append(procHttpShow.StandardOutput.ReadLine());
            procHttpShow?.WaitForExit(2000);

            if (sbPsiResults.ToString().ToLower().Contains("ip:port") && sbPsiResults.ToString().ToLower().Contains(":9090"))
            {
                // DELETE
                psi.FileName = "netsh";
                psi.Arguments = $"http delete sslcert ipport=0.0.0.0:{port}";
                sbPsiResults.Clear();
                Process procHttpDelete = Process.Start(psi);
                while (procHttpDelete != null && !procHttpDelete.StandardOutput.EndOfStream)
                    sbPsiResults.Append(procHttpDelete.StandardOutput.ReadLine());
                procHttpDelete?.WaitForExit(2000);

                if (sbPsiResults.ToString().ToLower().Contains("success"))
                    isBounded = false;
                else
                    throw new Exception($"http delete sslcert error : {sbPsiResults.ToString()}");
            }
            else
                isBounded = false;               

            if (!isBounded)
            {
                // ADD
                psi.FileName = "netsh";
                psi.Arguments = $"http add sslcert ipport=0.0.0.0:{port} certhash={GetCertHash(certFileName).ToLower()} appid={{{GetAssemblyGuid()}}}";
                sbPsiResults.Clear();
                Process procHttpAdd = Process.Start(psi);
                while (procHttpAdd != null && !procHttpAdd.StandardOutput.EndOfStream)
                    sbPsiResults.Append(procHttpAdd.StandardOutput.ReadLine());
                procHttpAdd?.WaitForExit(2000);
                if (sbPsiResults.ToString().ToLower().Contains("error"))
                {
                    isBounded = false;
                    throw new Exception($"http add sslcert error : {sbPsiResults.ToString()}");
                }
                else
                    isBounded = true;
            }
            return isBounded;
        }
    }
}

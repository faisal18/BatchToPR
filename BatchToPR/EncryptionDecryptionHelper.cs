﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BatchToPR
{
    class EncryptionDecryptionHelper
    {
        public string EncryptData(X509Certificate2 cert, byte[] data)
        {
            // GetRSAPublicKey returns an object with an independent lifetime, so it should be
            // handled via a using statement.
            string finalEnc = "";
            try
            {

                RSACryptoServiceProvider rsa = cert.PublicKey.Key as RSACryptoServiceProvider;


                // OAEP allows for multiple hashing algorithms, what was formermly just "OAEP" is
                // now OAEP-SHA1.

                byte[] cryptedData = rsa.Encrypt(data, true);
                finalEnc = Convert.ToBase64String(cryptedData);

            }
            catch (Exception ex)
            {
                Console.Write("error=" + ex);
            }
            return (finalEnc);
        }
        public string EncryptMain(string mydata, string certname)
        {
            certname = "DHCRINT.DHCA.AE";

            X509Certificate2 encCert = null;

            byte[] byteData = Encoding.ASCII.GetBytes(mydata);
            FetchCertificateLocalMachine(ref encCert, certname);
            if (encCert != null)
            {

                string encData = EncryptData(encCert, byteData);
                return (encData);
                //this.textBoxResult.Text = encData;
            }
            return ("");
        }
        public string DecryptData(X509Certificate2 cert, string data)
        {
            // GetRSAPrivateKey returns an object with an independent lifetime, so it should be
            // handled via a using statement.

            string finalDec = "";
            try
            {

                RSACryptoServiceProvider rsa = cert.PrivateKey as RSACryptoServiceProvider;


                // OAEP allows for multiple hashing algorithms, what was formermly just "OAEP" is
                // now OAEP-SHA1.
                byte[] byteEncoded = Convert.FromBase64String(data);
                byte[] decryptedData = rsa.Decrypt(byteEncoded, true);
                finalDec = System.Text.Encoding.ASCII.GetString(decryptedData);

            }
            catch (Exception ex)
            {
                Console.Write("error=" + ex);
            }
            return (finalDec);
        }
        private void FetchCertificateLocalMachine(ref System.Security.Cryptography.X509Certificates.X509Certificate2 cert, string certificteName)
        {
            try
            {
                X509Store store = new X509Store(StoreLocation.LocalMachine);

                store.Open(OpenFlags.ReadOnly);
                foreach (X509Certificate2 certificate in store.Certificates)
                {
                    string FullCertificateName = certificate.GetName();
                    int cc = FullCertificateName.IndexOf("CN=") + 3;
                    int len = FullCertificateName.Length - cc;

                    string FetchedCertificateName = FullCertificateName.Substring(cc, len);

                    if (certificteName.CompareTo(FetchedCertificateName) == 0)
                    {
                        cert = certificate;
                        break;
                    }
                }
                store.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        public string DecryptMain(string mydata, string certname)
        {
            certname = "DHCRINT.DHCA.AE";

            X509Certificate2 encCert = null;

            FetchCertificateLocalMachine(ref encCert, certname);
            if (encCert != null)
            {

                string decData = DecryptData(encCert, mydata);
                return (decData);
                //this.textBoxResult.Text = decData;
            }
            return ("");
        }
    }
}

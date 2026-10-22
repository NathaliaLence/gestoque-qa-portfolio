using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Gestoque.Helper
{
    public static class CriptoHelper
    {
        //nao trabalha com string
        public static string HashMD5(string val)
        {
            var bytes = Encoding.ASCII.GetBytes(val); //converte string em bytes
            var md5 = MD5.Create(); //cria o algoritmo
            var hash = md5.ComputeHash(bytes); //obter a criptografia

            var ret = string.Empty;
            for (int i = 0; i < hash.Length; i++)
            {
                ret += hash[i].ToString("x2");
            }

            return ret;
        }
    }
}
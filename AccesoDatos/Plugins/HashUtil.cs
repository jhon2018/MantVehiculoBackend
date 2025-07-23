﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Plugins
{
    public class HashUtil
    {
        // Averiguar MD5 //





        public static string ObtenerMD5(string valor)

        {

            using MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.UTF8.GetBytes(valor);

            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            foreach (byte b in hashBytes)

                sb.Append(b.ToString("x2"));

            return sb.ToString();

        }

    }
}

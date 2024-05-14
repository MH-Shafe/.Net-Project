﻿using DAL.EF;
using DAL.EF.Entites;
using DAL.EF.Entites.Admin;
using DAL.EF.Entites;
using DAL.Interfaces;
using DAL.Repos;
using DAL.Repos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF.Entities.User;

namespace DAL
{
    public class DataFactory
    {
        private static UberContext context = new UberContext();
        public static IRepo<Login, int> LoginData()
        {
            return new LoginRepo();
        }
        
        public static IRepo<SignUp, int> SignUpData()
        {
            return new SignUpRepo(context);
        }
        public static IRepo<Payment_u, int> Payment_uData()
        {
            return new PaymentRepo(context);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TutHub.Models;
using TutHub.Models.enums;

namespace TutHub.DataHandlers
{
    public class UserHandler
    {
        private IConfiguration _config;
        public UserHandler(IConfiguration config)
        {
            _config = config;
        }


        public async Task<User> LogIn(string username, string password)
        {
            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            using(MySqlConnection conn = new MySqlConnection(connString))
            {
                
                await conn.OpenAsync();
                
                string sqlStr = String.Format("select * from Users where usr_id='{0}' and u_password= '{1}'", username, password);
                using (MySqlCommand cmd = new MySqlCommand(sqlStr, conn))
                {
                    
                    using(var sqlDataReader = await cmd.ExecuteReaderAsync())
                    {
                        
                        while(await sqlDataReader.ReadAsync())
                        { 
                            var data = (string) sqlDataReader.GetValue(0);

                            if (data == username)
                            {
                                User user = new User();
                                user.Usr_id = username;
                                user.F_Name = (string)sqlDataReader.GetValue(1);
                                user.L_Name = (string)sqlDataReader.GetValue(2);
                                user.U_Gender = (Gender)sqlDataReader.GetValue(3);
                                return user;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public async Task<User> SignUp(User user)
        {
            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sqlStr = String.Format("insert into Users (usr_id, f_name, l_name, gender, u_password, is_student, is_teacher, photo_url, language_pref) " +
                    "values('{0}','{1}','{2}',{3},'{4}',{5},{6},'{7}',{8});", user.ToArray());
                
                using (MySqlCommand cmd = new MySqlCommand(sqlStr, conn))
                {
                    if (await cmd.ExecuteNonQueryAsync() == 1)
                    {
                        return user;
                    }
                }
            }
            return null;
        }


        //TODO
        public async Task<User> Update(User user)
        {
            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sqlStr = String.Format("update Users set usr_id='{0}', f_name, l_name, gender, u_password, is_student, is_teacher, photo_url, language_pref) " +
                    "values('{0}','{1}','{2}',{3},'{4}',{5},{6},'{7}',{8});", user.ToArray());
                
                string sqlSt0r = String.Format("insert into Users (usr_id, f_name, l_name, gender, u_password, is_student, is_teacher, photo_url, language_pref) " +
                    "values('{0}','{1}','{2}',{3},'{4}',{5},{6},'{7}',{8});", user.ToArray());

                using (MySqlCommand cmd = new MySqlCommand(sqlStr, conn))
                {
                    if (await cmd.ExecuteNonQueryAsync() == 1)
                    {
                        return user;
                    }
                }
            }
            return null;
        }


        public async Task<bool> Delete(string id)
        {
            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sqlStr = String.Format("delete from Users where usr_id='{0}'", id);

                using (MySqlCommand cmd = new MySqlCommand(sqlStr, conn))
                {
                    if (await cmd.ExecuteNonQueryAsync() == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

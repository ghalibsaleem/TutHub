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
                        
                        if(await sqlDataReader.ReadAsync())
                        {
                            List<object> lst = new List<object>();
                            for (int i = 0; i < sqlDataReader.FieldCount; i++)
                            {
                                lst.Add(sqlDataReader[i]);
                            }

                            User user = new User();
                            user.FromArray(lst);
                            return user;
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

        public async Task<bool> UpdatePassword(string username, string oldpassword, string newPassword)
        {
            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                bool flag = false;

                await conn.OpenAsync();

                string sqlStr = String.Format("select * from Users where usr_id='{0}' and u_password= '{1}'", username, oldpassword);
                using (MySqlCommand cmd = new MySqlCommand(sqlStr, conn))
                {

                    using (var sqlDataReader = await cmd.ExecuteReaderAsync())
                    {

                        while (await sqlDataReader.ReadAsync())
                        {
                            flag = true;
                        }
                    }
                }

                if (flag)
                {
                    sqlStr = String.Format("update Users " +
                    "set u_password = '{2}' " +
                    "where usr_id='{0}' and u_password= '{1}'", username, oldpassword, newPassword);
                    using (MySqlCommand cmd = new MySqlCommand(sqlStr, conn))
                    {

                        if (await cmd.ExecuteNonQueryAsync() == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public async Task<User> Update(User user)
        {
            string connString = _config.GetValue<string>("ConnectionStrings:TutHub_DB");

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                await conn.OpenAsync();
                string sqlStr = String.Format("update Users " +
                    "set f_name = '{1}', l_name='{2}', gender={3}, is_student= {5}, is_teacher = {6}, photo_url = '{6}', language_pref = '{6}'  " +
                    "where usr_id={0}", user.ToArray());

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

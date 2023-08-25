using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IPLProject.Models;

namespace IPLProject.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;

            // 고정된 사용자 이름과 비밀번호 설정
            string fixedAdminUsername = "admin";
            string fixedAdminPassword = "1234";

            string fixedUserAUsername = "userA";
            string fixedUserAPassword = "1234";

            string fixedUserBUsername = "userB";
            string fixedUserBPassword = "1234";

            if (credential.UserName == fixedAdminUsername && credential.Password == fixedAdminPassword
                || credential.UserName == fixedUserAUsername && credential.Password == fixedUserAPassword
                || credential.UserName == fixedUserBUsername && credential.Password == fixedUserBPassword)
            {
                validUser = true;
            }
            else
            {
                validUser = false;
            }

            return validUser;
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }
        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }
        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                //데이터 베이스 사용하지 않으므로 주석 처리
                //connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where username=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                //using (var reader = command.ExecuteReader())
                //{
                //    if (reader.Read())
                //    {
                //        user = new UserModel()
                //        {
                //            Id = reader[0].ToString(),
                //            Username = reader[1].ToString(),
                //            Password = string.Empty,
                //            Name = reader[3].ToString(),
                //            LastName = reader[4].ToString(),
                //            Email = reader[5].ToString(),
                //        };
                //    }
                //}
            }
            return user;
        }
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
